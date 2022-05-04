using System;
using System.Collections.Generic;
using System.Text;

namespace RPKIdecoder
{
    class DifferentIssuerNumbersException : Exception
    {
        public DifferentIssuerNumbersException()
        {
            Console.WriteLine("Error: Issuer numbers are different");
        }
    }
}
