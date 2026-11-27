using Data_Access;
using Data_Access.ListingDTOs;
using System;
using System.ComponentModel;
using System.Threading.Tasks;

namespace Business.ListingClasses
{
    public class PenaltyList
    {
        public int PenaltyID { get; set; }
        public string Patient { get; set; }
        public decimal Price { get; set; }
        public DateTime PenaltyDueDate { get; set; }
        public DateTime? PenaltyClosedDate { get; set; }
        public string Status { get; set; }
        private static PenaltyList FromDto(PenaltyListDto penalty)
        {
            return new PenaltyList()
            {
                PenaltyID = penalty.PenaltyID,
                Patient = penalty.Patient,
                Price = penalty.Price,
                PenaltyDueDate = penalty.PenaltyDueDate,
                PenaltyClosedDate = penalty.PenaltyClosedDate,
                Status = penalty.Status
            };
        }
        public async static Task<BindingList<PenaltyList>> GetAllPenalties(int patientId)
        {
            BindingList<PenaltyList> penalties = new BindingList<PenaltyList>();

            foreach (PenaltyListDto penaltyDto in await PenaltyData.GetAllPenalties(patientId))
                penalties.Add(FromDto(penaltyDto));

            return penalties;
        }
    }
}
