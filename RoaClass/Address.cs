using System.Net;

namespace RPKIdecoder
{
    public class Address
    {
        IPAddress ipAddress;

        int netmask;

        int maxLength;


        public IPAddress getIpAddress()
        {
            return this.ipAddress;
        }

        public void setIpAddress(IPAddress ipAddress)
        {
            this.ipAddress = ipAddress;
        }

        public int getNetmask()
        {
            return this.netmask;
        }

        public void setNetmask(int netmask)
        {
            this.netmask = netmask;
        }

        public int getMaxLength()
        {
            return this.maxLength;
        }

        public void setMaxLength(int maxLength)
        {
            this.maxLength = maxLength;
        }

        override
        public string ToString()
        {
            if (this.maxLength == 0)
            {
                return "IP address : " + this.ipAddress + "/" + this.netmask + "\n" + "Max length : not present \n" ;
            }
            return "IP address : " + this.ipAddress + "/" + this.netmask + "\n" + "Max length : " + this.maxLength + "\n";
        }
    }
}