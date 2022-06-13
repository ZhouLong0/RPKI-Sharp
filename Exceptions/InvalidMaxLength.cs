using System;
using System.Collections.Generic;
using System.Text;

namespace RPKIdecoder
{
    class InvalidMaxLength : Exception
      {

        public InvalidMaxLength()
        {
            Console.WriteLine("If present, the maxLength MUST be an integer greater than " +
                "or equal to the length of the accompanying prefix, and less than or " +
                "equal to the length(in bits) of an IP address in the address family " +
                "(32 for IPv4 and 128 for IPv6) ");
        }
    }
    
}
