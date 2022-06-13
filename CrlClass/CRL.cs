using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace RPKIdecoder.CrlClass
{
    class CRL
    {
        public CRL()
        {
            this.revokedCertificates = new List<revokedCertificate>();
        }

        private string IssuerName;
        private DateTime thisUpdate;
        private DateTime nextUpdate;
        List<revokedCertificate> revokedCertificates;

        public void setIssuerName(string issuerName)
        {
            this.IssuerName = issuerName;
        }

        public void setThisUpdate(DateTime t)
        {
            this.thisUpdate = t;
        }

        public void setNextUpdate(DateTime t)
        {
            this.nextUpdate = t;
        }

        public string getIssuerNumber()
        {
            return this.IssuerName;
        }

        public List<revokedCertificate> getRevokedCertificates()
        {
            return this.revokedCertificates;
        }

        override
        public string ToString()
        {
            StringBuilder stringa = new StringBuilder();
            stringa.Append("Issuer Name : " + this.IssuerName + "\n");
            stringa.Append("This Update : " + this.thisUpdate + "\n");
            stringa.Append("Next Update : " + this.nextUpdate+ "\n");
            stringa.Append("Revoked Certificate List : " + "\n");

            foreach (revokedCertificate rc in revokedCertificates)
            {
                stringa.Append(rc.ToString());
            }
            stringa.Append("\n\n\n");
            return stringa.ToString();
        }

    }
}
