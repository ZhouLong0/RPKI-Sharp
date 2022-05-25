using System.Net;

namespace RPKIdecoder
{
    public class Address
    {
        IPAddress ipAddress;

        int netmask;

        IPNetwork prefix;

        int maxLength;

        public Address()
        {
            this.prefix = new IPNetwork();
        }

        public IPNetwork getPrefix()
        {
            return this.prefix;
        }
        public void setPrefix(IPNetwork prefix)
        {
            this.prefix = prefix;
        }

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
                return "IP address : " + this.getPrefix().ToString() + "\n" + "Max length : not present \n" ;
            }
            return "IP address : " + this.getPrefix().ToString() + "\n" + "Max length : " + this.maxLength + "\n";
        }
    }
}