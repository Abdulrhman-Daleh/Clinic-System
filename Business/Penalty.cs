using AppEnums.cs;
using Data_Access.DTOs;
using System;

namespace Business
{
    public class Penalty
    {
        public int PenaltyID { get; set; }
        public EnPenaltyStatuses PenaltyStatus { get; set; }
        public int? AppointmentID { get; set; }
        public Appointment Appointment { get; set; }
        public decimal PenaltyAmount { get; set; }
        public DateTime PenaltyWrittenDate { get; set; }
        public int PolicyID { get; set; }
        public Policy Policy { get; set; }
        public DateTime PenaltyDueDate { get; set; }
        public DateTime? PenaltyClosedDate { get; set; }
        public Penalty()
        {
            PenaltyID = -1;
            PenaltyStatus = 0;
            AppointmentID = null;
            PenaltyClosedDate = null;
        }
        public static PenaltyDto ToDTO(Penalty penalty)
        {
            if (penalty == null)
                return new PenaltyDto();

            return new PenaltyDto()
            {
                PenaltyID = penalty.PenaltyID,
                PenaltyStatus = penalty.PenaltyStatus,
                AppointmentID = penalty.AppointmentID,
                PenaltyAmount = penalty.PenaltyAmount,
                PenaltyWrittenDate = penalty.PenaltyWrittenDate,
                PolicyID = penalty.PolicyID,
                PenaltyClosedDate = penalty.PenaltyClosedDate,
                PenaltyDueDate = penalty.PenaltyDueDate
            };
        }
        public Penalty CreatePenalty(Policy policy, Appointment appointment, EnAppointmentStatuses currentStatus)
        {
            if (appointment.IsAppointmentOutDated())
            {
                if (currentStatus == EnAppointmentStatuses.InRoom)
                {
                    return null;
                }
            }

            Penalty penalty = new Penalty();
            penalty.PenaltyID = policy.PolicyID;
            penalty.AppointmentID = appointment.AppointmentID;
            penalty.Appointment = appointment;

            if (penalty.Appointment.IsStillActive())
                return null;

            if (DateTime.Now < penalty.Appointment.AppointmentDate)
                return null;


            penalty.Appointment.AppointmentStatuses = EnAppointmentStatuses.NoShow;
            penalty.PenaltyAmount = policy.NoShowFee ?? 0;
            penalty.PenaltyWrittenDate = DateTime.Now;
            penalty.PenaltyDueDate = DateTime.Now.AddDays(7);
            penalty.PolicyID = policy.PolicyID;
            penalty.PenaltyStatus = EnPenaltyStatuses.Unpaid;

            return penalty;
        }
    }
}