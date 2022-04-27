using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace RPKIdecoder
{
    class ROA
    {
        private int numberOfByte;

        private BigInteger asNumber;

        private List<IpAddressBlock> ipAddressBlocks;

        public ROA()
        {
            ipAddressBlocks = new List<IpAddressBlock>();
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
        override
        public string ToString()
        {
            StringBuilder stringa = new StringBuilder("");
            stringa.Append("\nNumber of byte : " + numberOfByte);
            stringa.Append("\nAs number : " + asNumber);

            foreach (IpAddressBlock i in ipAddressBlocks)
            {
                stringa.Append(i.ToString());
            }
            return stringa.ToString();

        }

    }
}
