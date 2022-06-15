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
            TextWriter tsw = new StreamWriter(@"C:\Users\zhoul\Desktop\MRT20120601.txt");
            tsw.WriteLine("Prefix\tAsNumber\tExistRecord\tValid\tNCertificates\tNAuthAs\tAuthAs's");

            FileInfo file = new FileInfo(@"C:\Users\zhoul\Desktop\bview.20120601.0000");
            String directoryWithCertificates = @"C:\Users\zhoul\Desktop\repo20120601";

            List<ROA> decodedRoas = DirectoryDecoder.decodeVerifiedRoas(directoryWithCertificates);
            List<CRL> decodedCrls = DirectoryDecoder.decodeVerifiedCrls(directoryWithCertificates);

            MRTParser.Run<TableDumpRIBRecord>(file, (mrtEntry) =>
            {
                IPNetwork prefixOfEntry = new IPNetwork(mrtEntry.Prefix.GetNetworkAddress(), (byte)mrtEntry.Prefix.Cidr);

                foreach (TableDumpRIBEntry tdrEntry in mrtEntry.Entries)
                {
                    if (tdrEntry.Peer.PeerAS == 3333)
                    {
                        /*Do something only if it has peerAs = 3333*/
                        uint asNumber = (uint)tdrEntry.BGPUpdate.ASPath[0].ASNumbers.GetValue(tdrEntry.BGPUpdate.ASPath[0].ASNumbers.Length-1);

                        RibTableLine tableLine = ROA.createTableLine(decodedRoas, decodedCrls, prefixOfEntry, asNumber);
                        tsw.WriteLine(tableLine);
                     

                    }
                }

            });
            tsw.Close();


        }

        static void Main(string[] args)
        {

            MRTWriteResult();

            //FileInfo file = new FileInfo(@"C:\Users\zhoul\Desktop\bview.20120601.0000");
            //MRTParser.Run<TableDumpRIBRecord>(file, (mrtEntry) =>
            //{

                
            //    Console.WriteLine(mrtEntry);

            //});

        }
    }
}
