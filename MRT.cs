using MRTSharp;
using MRTSharp.Model.TableDump;
using RPKIdecoder.CrlClass;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;

namespace RPKIdecoder
{
    class MRT
    {
        public static void MRTWriteResult()
        {
                   
            ///*setup*/
            //TextWriter tsw = new StreamWriter(@"C:\Users\zhoul\Desktop\MRT_Results\2020\ResultTable2.txt");
            //tsw.WriteLine("Prefix\tAsNumber\tExistRecord\tValid\tNCertificates\tNAuthAs\tAuthAs's");
            //tsw.Close();

            /* *****CHANGE PATH*****           MRT table file*/
            FileInfo file = new FileInfo(@"C:\Users\zhoul\Desktop\ReposData\2020\bview.20200601.0000");
            /* *****CHANGE PATH*****           Roa repository*/
            String directoryWithCertificates = @"C:\Users\zhoul\Desktop\ReposData\2020";

            /*Decoded certificates*/
            List<ROA> decodedRoas = DirectoryDecoder.decodeVerifiedRoas(directoryWithCertificates);
            List<CRL> decodedCrls = DirectoryDecoder.decodeVerifiedCrls(directoryWithCertificates);

            Console.WriteLine(decodedRoas.Count + " Roas");
            Console.WriteLine(decodedCrls.Count + " Crls");

            for (int i = 0; i < decodedRoas.Count; i++)
            {
                ROA roa = decodedRoas[i];
                if (CRL.containsRoa(decodedCrls, roa))
                    decodedRoas.Remove(roa);
            }

            GlobalStats stats = new GlobalStats();

            Console.WriteLine("ALL certificates have just been decoded");
            Console.WriteLine(decodedRoas.Count + " Valid Roas");

            //if is required to start from an ip
            //bool start = false;

            MRTParser.Run<TableDumpRIBRecord>(file, (mrtEntry) =>
            {
                IPNetwork prefixOfEntry = new IPNetwork(mrtEntry.Prefix.GetNetworkAddress(), (byte)mrtEntry.Prefix.Cidr);

                //if is required to start from an ip
                //if (prefixOfEntry == IPNetwork.Parse("117.187.8.0/23"))
                //    start = true;

                //if (start == true)
                {
                    foreach (TableDumpRIBEntry tdrEntry in mrtEntry.Entries)
                    {
                        /*Do something only if it has peerAs = 3333*/
                        if (tdrEntry.Peer.PeerAS == 3333)
                        {
                            /*Do something only if it has peerAs = 3333*/
                            uint asNumber = (uint)tdrEntry.BGPUpdate.ASPath[0].ASNumbers.GetValue(tdrEntry.BGPUpdate.ASPath[0].ASNumbers.Length - 1);

                            /*Write an output line*/
                            RibTableLine tableLine = ROA.createTableLine(decodedRoas, decodedCrls, prefixOfEntry, asNumber);
                            
                            stats.lineUpdate(tableLine);
                            TextWriter tsw1 = new StreamWriter(@"C:\Users\zhoul\Desktop\MRT_Results\2020\ResultTable5.txt", true);
                            tsw1.WriteLine(tableLine);

                            TextWriter statsFile = new StreamWriter(@"C:\Users\zhoul\Desktop\MRT_Results\2020\Stats5.txt");
                            stats.globalUpdate();
                            statsFile.WriteLine(stats);

                            statsFile.Close();
                            tsw1.Close();
                        }
                    }
                }

                

            });
           
            

        }

        static void Main(string[] args)
        {

            MRTWriteResult();


        }
    }
}
