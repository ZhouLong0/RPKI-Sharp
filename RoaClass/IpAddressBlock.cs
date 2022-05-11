using System.Collections.Generic;
using System.Text;

namespace RPKIdecoder
{
    public class IpAddressBlock
    {
        private string addressFamily;

        private List<Address> addresses;

        public IpAddressBlock()
        {
            this.addresses = new List<Address>();
        }

        public void setAddressFamily(string addressFamily)
        {
            this.addressFamily = addressFamily;
        }

        public List<Address> getAddresses()
        {
            return this.addresses;
        }

        override
        public string ToString()
        {
            StringBuilder stringa = new StringBuilder();
            stringa.Append("Address family : " + addressFamily + "\n");
            foreach(Address a in addresses)
            {
                stringa.Append(a.ToString());
            }
            return stringa.ToString();
        }
    }
}