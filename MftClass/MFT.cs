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

        private int numberOfByte;

        private BigInteger manifestNumber;

        private DateTimeOffset thisUpdate;

        private DateTimeOffset nextUpdate;

        private string fileHashAlg;

        private List<FileAndHash> filesAndHashes;


        public void setNumberOfByte(int n)
        {
            this.numberOfByte = n;
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

        override
        public string ToString()
        {
            StringBuilder stringa = new StringBuilder();
            stringa.Append("Number total of byte: " + this.numberOfByte + "\n");
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
