using System;

namespace TimeRegisterAPI.Domain
{
    public class TimeRecord
    {
        public int Id { get; set; }
        public int ActivityNumber { get; set; }
        public decimal UsedTime { get; set; }
        public string Comments { get; set; }
        public DateTime DateRecord { get; set; }
    }
}
