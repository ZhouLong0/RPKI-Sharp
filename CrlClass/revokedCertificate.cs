using System;
using System.Numerics;

namespace RPKIdecoder.CrlClass
{
    class revokedCertificate
    {
        private DateTime revocationTime;
        private BigInteger serialNumber;

        public void setRevocationTime(DateTime dateTime)
        {
            this.revocationTime = dateTime;
        }

        public void setSerialNumber(BigInteger serialNumber)
        {
            this.serialNumber = serialNumber;
        }

        public DateTime getRevocationTime()
        {
            return this.revocationTime;
        }

        public BigInteger getSerialNumber()
        {
            return this.serialNumber;
        }

        public string ToString()
        {
            return "Serial Number : " + this.serialNumber + "\n" + "Revocation Time : " + this.revocationTime + "\n";
        }
    }
}