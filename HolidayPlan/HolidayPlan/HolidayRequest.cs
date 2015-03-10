using System;

namespace HolidayPlan
{
    public class HolidayRequest
    {
        public string EmployeeName;
        public string EmployeeEmail;
        public string ManagerEmail;

        // holiday period
        public DateTime From;
        public DateTime To;

        public RequestStatus Status;
    }
}
