using System;

namespace Data_Access.DTOs
{
    public class PolicyDto
    {
        public int PolicyID { get; set; }
        public int OwnerID { get; set; }
        public int PolicyStatusID { get; set; }
        public DateTime ActiveFrom { get; set; }
        public DateTime ActiveTo { get; set; }
        public short? NoShowFreezeDays { get; set; }
        public decimal? LateFeesPerDay { get; set; }
        public decimal? NoShowFee { get; set; }
    }
}
