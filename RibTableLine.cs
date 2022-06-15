using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace RPKIdecoder
{
    class RibTableLine
    {
        private IPNetwork prefix;

        private uint asNumber;

        private bool existRecord;

        private bool valid;

        private int numberOfRecords;

        private int numberOfDifferentAuthorizedAs;
        
        List<int> authorizedAs;

        public RibTableLine()
        {
            this.authorizedAs = new List<int>();
        }

        public IPNetwork Prefix { get => prefix; set => prefix = value; }
        public uint AsNumber { get => asNumber; set => asNumber = value; }
        public bool ExistRecord { get => existRecord; set => existRecord = value; }
        public bool Valid { get => valid; set => valid = value; }
        public int NumberOfRecords { get => numberOfRecords; set => numberOfRecords = value; }
        public int NumberOfDifferentAuthorizedAs { get => numberOfDifferentAuthorizedAs; set => numberOfDifferentAuthorizedAs = value; }
        public List<int> AuthorizedAs { get => authorizedAs; set => authorizedAs = value; }

        override
        public String ToString()
        {
            StringBuilder authas = new StringBuilder();
            foreach(int asN in authorizedAs)
            {
                authas.Append(asN + ",");
            }
            return prefix +"\t"+ asNumber +"\t"+ existRecord +"\t"+ valid + "\t"+ numberOfRecords + 
                "\t" + numberOfDifferentAuthorizedAs + "\t" + authas;
        }
    }
}
