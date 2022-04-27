using System;
using System.Collections.Generic;
using System.Text;

namespace RPKIdecoder
{
    class NotExistingTypeOfPrefix : Exception
      {

        public NotExistingTypeOfPrefix()
        {
            Console.WriteLine("Error: type of prefix is not valid");
        }
    }
    
}
