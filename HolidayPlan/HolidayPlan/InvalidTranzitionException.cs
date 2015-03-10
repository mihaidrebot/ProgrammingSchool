using System;

namespace HolidayPlan
{
    public class InvalidTranzitionException:Exception
    {
        readonly Tranzition args;

        public InvalidTranzitionException(Tranzition args)
        {
            this.args = args;
        }
    }
}
