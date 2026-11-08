using AppEnums.cs;
using Data_Access;
using Data_Access.DTOs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;

namespace Business
{
    public class Prescription
    {
        private EnMode _mode;
        public int PrescriptionID { get; set; }
        public int AppointmentID { get; set; }
        public Appointment Appointment { get; set; }
        public decimal TotalPrice { get; set; }
        public bool DoctorDidSign { get; set; }
        public DateTime WriteDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public List<Business.PrescriptionItem> PrescriptionItems { get; set; }
        public bool IsAddMode()
        {
            return _mode == EnMode.Add;
        }

        public Prescription()
        {
            PrescriptionID = -1;
            AppointmentID = -1;
            TotalPrice = 0;
            DoctorDidSign = false;
            WriteDate = DateTime.Now;
            _mode = EnMode.Add;
            PrescriptionItems = new List<PrescriptionItem>();
            Appointment = new Appointment();
        }

        public Prescription(int prescriptionID, int appointmentId)
        {
            PrescriptionID = prescriptionID;
            Appointment = Appointment.FindAppointmentByID(appointmentId);
            _mode = EnMode.Update;
        }

        public static Prescription FromDto(PrescriptionDto prescriptionDto)
        {
            if (prescriptionDto == null)
                return null;

            return new Prescription(prescriptionDto.PrescriptionID, prescriptionDto.AppointmentID)
            {
                PrescriptionID = prescriptionDto.PrescriptionID,
                AppointmentID = prescriptionDto.AppointmentID,
                TotalPrice = prescriptionDto.TotalPrice,
                DoctorDidSign = prescriptionDto.DoctorDidSign,
                WriteDate = prescriptionDto.WriteDate,
                ExpirationDate = prescriptionDto.ExpirationDate
            };
        }

        public static PrescriptionDto ToDto(Prescription prescription)
        {
            if (prescription == null)
                return null;

            return new PrescriptionDto()
            {
                PrescriptionID = prescription.PrescriptionID,
                AppointmentID = prescription.AppointmentID,
                TotalPrice = prescription.TotalPrice,
                DoctorDidSign = prescription.DoctorDidSign,
                WriteDate = prescription.WriteDate,
                ExpirationDate = prescription.ExpirationDate
            };
        }

        private bool AddNewPrescription()
        {
            List<PrescriptionItemDto> list = new List<PrescriptionItemDto>();

            foreach (var item in PrescriptionItems)
                list.Add(PrescriptionItem.ToDto(item));

            PrescriptionID = PrescriptionData.AddNewPrescription(ToDto(this), list);

            return PrescriptionID > 0;
        }

        private bool UpdatePrescription()
        {
            return PrescriptionData.UpdatePrescription(ToDto(this));
        }

        public decimal CalculateTotalPrice()
        {
            decimal totalPrice = 0;

            foreach (var p in PrescriptionItems)
                totalPrice += (p.Medicine.UnitPrice * p.Quantity);

            return totalPrice;
        }
        public OperationResult Save()
        {
            OperationResult result = IsPrescriptionValidToSave();

            if (!result.IsSuccess)
                return result;

            string performedBy = Global.CurrentLoggedInStaff == null
                ? "Initial setup"
                : $"StaffID {Global.CurrentLoggedInStaff.StaffId}";

            switch (_mode)
            {
                case EnMode.Add:

                    if (AddNewPrescription())
                    {
                        _mode = EnMode.Update;

                        Auditor.Log(
                            "Prescription Added",
                            $"Prescription for AppointmentID {AppointmentID} (Total: {TotalPrice}, Signed: {DoctorDidSign}) was created by {performedBy}.",
                            $"PrescriptionID {PrescriptionID}");

                        return new OperationResult(
                            "Prescription added successfully.",
                            "Added",
                            true);
                    }

                    return new OperationResult(
                        "Failed to add prescription.",
                        "Failed",
                        false);

                case EnMode.Update:

                    if (UpdatePrescription())
                    {
                        Auditor.Log(
                            "Prescription Updated",
                            $"Prescription for AppointmentID {AppointmentID} (Total: {TotalPrice}, Signed: {DoctorDidSign}, Expiration: {ExpirationDate:d}) was updated by {performedBy}.",
                            $"PrescriptionID {PrescriptionID}");

                        return new OperationResult(
                            "Prescription updated successfully.",
                            "Updated",
                            true);
                    }

                    return new OperationResult(
                        "Failed to update prescription.",
                        "Failed",
                        false);
            }

            return new OperationResult(
                "Invalid prescription operation mode.",
                "Failed",
                false);
        }

        public static Prescription FindPrescriptionByID(int prescriptionID)
        {
            return FromDto(PrescriptionData.FindPrescriptionByID(prescriptionID));
        }

        public static Prescription FindPrescriptionByAppointmentID(int appointmentID)
        {
            return FromDto(PrescriptionData.FindPrescriptionByAppointmentID(appointmentID));
        }

        public static async Task<BindingList<Prescription>> GetPrescriptions()
        {
            BindingList<Prescription> prescriptions = new BindingList<Prescription>();

            foreach (PrescriptionDto prescriptionDto in await PrescriptionData.GetPrescriptions())
            {
                prescriptions.Add(FromDto(prescriptionDto));
            }

            return prescriptions;
        }

        private OperationResult IsPrescriptionValidToSave()
        {
            if (PrescriptionItems.Count == 0)
                return new OperationResult(
               "Prescription requires at least one prescription item.",
               "Invalid",
               false);

            if (Util.IsValidId(AppointmentID) &&
                TotalPrice >= 0 &&
                ExpirationDate > WriteDate)
            {
                return new OperationResult(
                    "Prescription information is valid to save.",
                    "Valid",
                    true);
            }

            return new OperationResult(
                "Prescription information is invalid.",
                "Invalid",
                false);
        }

        public string SignedByName() => DoctorDidSign ? Appointment.Staff.GetFullName() : "Not Signed";

        public string GetValidUntilDate() => DateTime.Now >= ExpirationDate ? "Expired" : $"{ExpirationDate.ToShortDateString()}   ( +{(ExpirationDate - DateTime.Now).TotalDays} days)";

    }
}
