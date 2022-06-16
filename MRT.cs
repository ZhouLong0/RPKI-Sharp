using MRTSharp;
using MRTSharp.Model.TableDump;
using RPKIdecoder.CrlClass;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace RPKIdecoder
{
    class MRT
    {
        public static void MRTWriteResult()
        {
            /* *****CHANGE PATH*****           Table output file*/
            TextWriter tsw = new StreamWriter(@"C:\Users\zhoul\Desktop\MRT20201225.txt");
            /* *****CHANGE PATH*****           Stats output file*/
            TextWriter statsFile = new StreamWriter(@"C:\Users\zhoul\Desktop\MRT20201225stats.txt");

            tsw.WriteLine("Prefix\tAsNumber\tExistRecord\tValid\tNCertificates\tNAuthAs\tAuthAs's");

            /* *****CHANGE PATH*****           MRT table file*/
            FileInfo file = new FileInfo(@"C:\Users\zhoul\Desktop\bview.20120601.0000");
            /* *****CHANGE PATH*****           Roa repository*/
            String directoryWithCertificates = @"C:\Users\zhoul\Desktop\repo20120601";

            /*Decoded certificates*/
            List<ROA> decodedRoas = DirectoryDecoder.decodeVerifiedRoas(directoryWithCertificates);
            List<CRL> decodedCrls = DirectoryDecoder.decodeVerifiedCrls(directoryWithCertificates);

            GlobalStats stats = new GlobalStats();

            MRTParser.Run<TableDumpRIBRecord>(file, (mrtEntry) =>
            {
                IPNetwork prefixOfEntry = new IPNetwork(mrtEntry.Prefix.GetNetworkAddress(), (byte)mrtEntry.Prefix.Cidr);

                foreach (TableDumpRIBEntry tdrEntry in mrtEntry.Entries)
                {
                    /*Do something only if it has peerAs = 3333*/
                    if (tdrEntry.Peer.PeerAS == 3333)
                    {
                        /*Do something only if it has peerAs = 3333*/
                        uint asNumber = (uint)tdrEntry.BGPUpdate.ASPath[0].ASNumbers.GetValue(tdrEntry.BGPUpdate.ASPath[0].ASNumbers.Length-1);

                        /*Write an output line*/
                        RibTableLine tableLine = ROA.createTableLine(decodedRoas, decodedCrls, prefixOfEntry, asNumber);
                        stats.lineUpdate(tableLine);

                        /*Write it in the file*/
                        tsw.WriteLine(tableLine);
                     

                    }
                }



            });
            /* Create global stats */
            stats.globalUpdate();

            statsFile.WriteLine(stats);


            tsw.Close();
            statsFile.Close();

        }

        static void Main(string[] args)
        {

            MRTWriteResult();


        }
    }
}
