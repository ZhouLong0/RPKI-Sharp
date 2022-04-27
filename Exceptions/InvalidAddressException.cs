using System;
using System.Collections.Generic;
using System.Text;

namespace RPKIdecoder
{
    class InvalidAddressException : Exception
      {

        public InvalidAddressException()
        {
            Console.WriteLine("Error: address is not valid");
        }


    }
    
}
