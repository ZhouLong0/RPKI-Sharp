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

        override
        public string ToString()
        {
            StringBuilder stringa = new StringBuilder();
            stringa.Append("File name: " +this.file + "\n" + "Hash: ");
            foreach(int i in hash)
            {
                stringa.Append(i);
            }
            stringa.Append("\n");

            return stringa.ToString();
        }
    }
}
