using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
