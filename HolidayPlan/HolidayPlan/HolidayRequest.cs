using System;

namespace HolidayPlan
{
    public class HolidayRequest
    {
        public IEmployee Employee { get; set; }
        public IEmployee Manager { get; set; }

        // holiday period
        public DateTime From;
        public DateTime To;

        public RequestStatus Status;
    }
}
