using System;
using System.Text;

namespace RPKIdecoder
{
    class GlobalStats
    {
        uint nOfLines;

        uint nOfRecordedPrefix;

        uint nOfValidAnn;

        float pOfRecordedPrefix;

        float pOfValidAnn;

        float pOfInvalidAnn;

        public GlobalStats()
        {
            ;
        }

        public uint NOfLines { get => nOfLines; set => nOfLines = value; }
        public uint NOfRecordedPrefix { get => nOfRecordedPrefix; set => nOfRecordedPrefix = value; }
        public uint NOfValidAnn { get => nOfValidAnn; set => nOfValidAnn = value; }
        public float POfRecordedPrefix { get => pOfRecordedPrefix; set => pOfRecordedPrefix = value; }
        public float POfValidAnn { get => pOfValidAnn; set => pOfValidAnn = value; }
        public float POfInvalidAnn { get => pOfInvalidAnn; set => pOfInvalidAnn = value; }

        public void lineUpdate(RibTableLine ribTableLine)
        {
            nOfLines++;
            if (ribTableLine.ExistRecord)
            {
                nOfRecordedPrefix++;

                if(ribTableLine.Valid)
                {
                    nOfValidAnn++;
                }
            }
        }

        public void globalUpdate()
        {
            POfRecordedPrefix = NOfRecordedPrefix / (float)nOfLines;
            POfValidAnn = NOfValidAnn / (float)nOfLines;

        }


       override
       public String ToString()
        {
            StringBuilder stringa = new StringBuilder();
            stringa.AppendLine("Number of total lines: " + NOfLines);
            stringa.AppendLine("Number of recorded prefixes: " + NOfRecordedPrefix);
            stringa.AppendLine("Number of valid announcements: " + NOfValidAnn);
            stringa.AppendLine("Percentage of recorded prefixes: " + POfRecordedPrefix * 100 + "%");
            stringa.AppendLine("Percentage of valid announcement: " + POfValidAnn * 100 + "%");
            return stringa.ToString();
        }
    }
}
