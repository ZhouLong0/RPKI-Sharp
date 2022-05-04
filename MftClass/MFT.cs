using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace RPKIdecoder.MftClass
{
    class MFT
    {
        public MFT()
        {
            this.filesAndHashes = new List<FileAndHash>();
        }

        private BigInteger serialNumber;

        private string issuerNumber;

        private BigInteger manifestNumber;

        private DateTimeOffset thisUpdate;

        private DateTimeOffset nextUpdate;

        private string fileHashAlg;

        private List<FileAndHash> filesAndHashes;

        public void setSerialNumber(BigInteger serialNumber)
        {
            this.serialNumber = serialNumber;
        }

        public void setIssuerNumber(string issuerNumber)
        {
            this.issuerNumber = issuerNumber;
        }

        public void setManifestNumber(BigInteger mn)
        {
            this.manifestNumber = mn;
        }

        public void setThisUpdate(DateTimeOffset tu)
        {
            this.thisUpdate = tu;
        }

        public void setNextUpdate(DateTimeOffset nu)
        {
            this.nextUpdate = nu;
        }
        public void setFileHashAlg(string fha)
        {
            this.fileHashAlg = fha;
        }

        public List<FileAndHash> getFileAndHashes()
        {
            return this.filesAndHashes;
        }

        public string getIssuerNumber()
        {
            return this.issuerNumber;
        }

        override
        public string ToString()
        {
            StringBuilder stringa = new StringBuilder();
            stringa.Append("Certificate serial number : " + this.serialNumber + "\n");
            stringa.Append("Issuer number : " + this.issuerNumber + "\n");
            stringa.Append("Manifest number : " + this.manifestNumber + "\n");
            stringa.Append("This update: " + this.thisUpdate + "\n" + "Next update: " + this.nextUpdate + "\n");
            stringa.Append("FileHashAlg: " + this.fileHashAlg + "\n");
            stringa.Append("FileList: "+"\n");
            foreach (FileAndHash fh in filesAndHashes)
            {
                stringa.Append(fh.ToString());
            }
            stringa.Append("\n\n");

            return stringa.ToString();
        }

    }

}
