using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace RPKIdecoder
{
    class ROA
    {
        private int serialNumber;

        private string issuerNumber;

        private int numberOfByte;

        private BigInteger asNumber;

        private List<IpAddressBlock> ipAddressBlocks;

        private DateTimeOffset startDateTime;

        private DateTimeOffset endDateTime;

        public ROA()
        {
            ipAddressBlocks = new List<IpAddressBlock>();
        }

        public void setSerialNumber(int serialNumber)
        {
            this.serialNumber = serialNumber;
        }

        public void setIssuerNumber(string issuerNumber)
        {
            this.issuerNumber = issuerNumber;
        }

        public void setNumberOfByte(int numberOfByte)
        {
            this.numberOfByte = numberOfByte;
        }

        public BigInteger getAsNumber()
        {
            return this.numberOfByte;
        }

        public void setAsNumber(BigInteger asNumber)
        {
            this.asNumber = asNumber;
        }

        public List<IpAddressBlock> getIpAddressBlock()
        {
            return this.ipAddressBlocks;   
        }

        public void setStartDateTime(DateTimeOffset startDate)
        {
            this.startDateTime = startDate;
        }

        public void setEndDateTime(DateTimeOffset endDate)
        {
            this.endDateTime = endDate;
        }

        override
        public string ToString()
        {
            StringBuilder stringa = new StringBuilder();
            stringa.Append("Certificate serial number : " + this.serialNumber + "\n");
            stringa.Append("Issuer Number : " + this.issuerNumber + "\n");
            stringa.Append("Number of byte : " + this.numberOfByte +"\n");
            stringa.Append("As number : " + this.asNumber + "\n");
            stringa.Append("Not Before : " + this.startDateTime + "\n");
            stringa.Append("Not After : " + this.endDateTime + "\n");
            foreach (IpAddressBlock i in ipAddressBlocks)
            {
                stringa.Append(i.ToString());
            }
            stringa.Append("\n\n\n");
            return stringa.ToString();

        }

    }
}
