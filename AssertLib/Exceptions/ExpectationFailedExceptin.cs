using System;

namespace AssertLib
{
    public class ExpectationFailedExceptin : Exception
    {
        public string Actuall { get; set; }
        public string Expected { get; set; }
        
        

        public ExpectationFailedExceptin(string expected, string actuall,string message) : base(message)
        {
            Expected = expected;
            Actuall = actuall;

        }

        public ExpectationFailedExceptin(string expected, string actuall)
        {
            Expected = expected;
            Actuall = actuall;
        }

        public ExpectationFailedExceptin(string message) : base(message)
        {

        }

    }


}