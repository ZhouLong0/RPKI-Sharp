using RPKIdecoder.CrlClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Numerics;
using System.Text;

namespace RPKIdecoder
{
    class ROA
    {
        private string commonName;

        private BigInteger serialNumber;

        private string issuerNumber;

        private BigInteger asNumber;

        private List<IpAddressBlock> ipAddressBlocks;

        private DateTimeOffset startDateTime;

        private DateTimeOffset endDateTime;

        public ROA()
        {
            ipAddressBlocks = new List<IpAddressBlock>();
        }

        public void setSerialNumber(BigInteger serialNumber)
        {
            this.serialNumber = serialNumber;
        }

        public void setIssuerNumber(string issuerNumber)
        {
            this.issuerNumber = issuerNumber;
        }

        public string getIssuerNumber()
        {
            return this.issuerNumber;
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

        public void setCommonName(String commonName)
        {
            this.commonName = commonName;
        }

        public string getCommonName()
        {
            return this.commonName;
        }

        public BigInteger getSerialNumber()
        {
            return this.serialNumber;
        }

        public BigInteger getAsNumber()
        {
            return this.asNumber;
        }


        override
        public string ToString()
        {
            StringBuilder stringa = new StringBuilder();
            stringa.Append("Common Name : " + this.commonName + "\n");
            stringa.Append("Serial number : " + this.serialNumber + "\n");
            stringa.Append("Issuer Number : " + this.issuerNumber + "\n");
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


        public bool ContainsIPAddressRecord(IPNetwork ipAddress)
        {
            foreach (IpAddressBlock ip in this.getIpAddressBlock())
            {
                foreach (Address ad in ip.getAddresses())
                {
                    if (ad.getPrefix().Contains(ipAddress) && ipAddress.Cidr <= ad.getMaxLength())
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public static RibTableLine createTableLine(List<ROA> roas,List<CRL> cRLs, IPNetwork ipAddress, uint asNumber)
        {
            
            RibTableLine ribTableLine = new RibTableLine();
            ribTableLine.Prefix = ipAddress;
            ribTableLine.AsNumber = asNumber;

            foreach (ROA roa in roas)
            {
                if (roa.ContainsIPAddressRecord(ipAddress))
                {
                    ribTableLine.ExistRecord = true;
                    ribTableLine.NumberOfRecords++;
                    if (!ribTableLine.AuthorizedAs.Contains((uint)roa.getAsNumber()))
                        ribTableLine.AuthorizedAs.Add((uint)roa.getAsNumber());
                    if ((uint)roa.getAsNumber() == asNumber)
                        ribTableLine.Valid = true;
                }
            }

            ribTableLine.NumberOfDifferentAuthorizedAs = ribTableLine.AuthorizedAs.Count;
            return ribTableLine;
        }

   








    }
}
