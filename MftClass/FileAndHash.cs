using System;
using System.Collections.Generic;
using System.Text;

namespace RPKIdecoder.MftClass
{
    class FileAndHash
    {
        private String file;

        private byte[] hash;

        public void setFile(String file)
        {
            this.file = file;
        }

        public void setHash(byte[] hash)
        {
            this.hash = hash;
        }

        public String getFile()
        {
            return this.file;
        }

        override
        public string ToString()
        {
            StringBuilder stringa = new StringBuilder();
            stringa.Append("File common name: " +this.file + "\n" + "Hash: ");
            foreach(int i in hash)
            {
                stringa.Append(i);
            }
            stringa.Append("\n");

            return stringa.ToString();
        }
    }
}
