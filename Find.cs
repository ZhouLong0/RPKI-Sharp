using RPKIdecoder.CrlClass;
using RPKIdecoder.ExtractEveloped;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Numerics;

namespace RPKIdecoder
{
    static class Find
    {

        public static List<ROA> searchAllRoasWithIp(String directoryToSearch, IPNetwork prefixToFind)
        {
            Console.WriteLine("Checking all certificates\n");
            //System.Net.IPAddress ipaddress = System.Net.IPAddress.Parse(IpToFind);
            List<ROA> decodedROAS = new List<ROA>();

            foreach (string roaToOpen in Directory.GetFiles(directoryToSearch, "*.roa", SearchOption.AllDirectories))
            {
                byte[] fileRoa = File.ReadAllBytes(roaToOpen);
                byte[] extractedFileRoa = ExtractEnvelopedData.ExtractContent(fileRoa);
                ROA decodedRoa = DecoderData.DecodeROA(fileRoa, extractedFileRoa);
                decodedRoa.setCommonName(roaToOpen);
                decodedROAS.Add(decodedRoa);

                //Console.WriteLine(decodedRoa);
            }
            //61317 //"193.227.122.0"
            Console.WriteLine("Writing interesting ROAs");
            List<ROA> returnList = new List<ROA>();
            //TextWriter tsw = new StreamWriter(@"C:\Users\zhoul\Desktop\" + IpToFind +".txt");
            foreach (ROA r in decodedROAS)
            {
                foreach (IpAddressBlock ip in r.getIpAddressBlock())
                {
                    foreach (Address ad in ip.getAddresses())
                    {
                        if (ad.getPrefix().Contains(prefixToFind))
                        {
                            Console.WriteLine(r);
                            if (!returnList.Contains(r)) returnList.Add(r);
                            //tsw.WriteLine(r.ToString());
                        }
                    }
                }
                //if(r.getAsNumber() == 61317)
                //{
                //    Console.WriteLine(r);
                //    tsw.WriteLine(r);
                //}
            }
            //tsw.Close();
            return returnList;
        }

        public static List<ROA> searchAllRoasWithIp(String directoryToSearch, IPAddress prefixToFind)
        {
            Console.WriteLine("Checking all certificates\n");
            //System.Net.IPAddress ipaddress = System.Net.IPAddress.Parse(IpToFind);
            List<ROA> decodedROAS = new List<ROA>();

            foreach (string roaToOpen in Directory.GetFiles(directoryToSearch, "*.roa", SearchOption.AllDirectories))
            {
                byte[] fileRoa = File.ReadAllBytes(roaToOpen);
                byte[] extractedFileRoa = ExtractEnvelopedData.ExtractContent(fileRoa);
                ROA decodedRoa = DecoderData.DecodeROA(fileRoa, extractedFileRoa);
                decodedRoa.setCommonName(roaToOpen);
                decodedROAS.Add(decodedRoa);

                //Console.WriteLine(decodedRoa);
            }
            //61317 //"193.227.122.0"
            Console.WriteLine("Writing results");
            List<ROA> returnList = new List<ROA>();
            //TextWriter tsw = new StreamWriter(@"C:\Users\zhoul\Desktop\" + IpToFind +".txt");
            foreach (ROA r in decodedROAS)
            {
                foreach (IpAddressBlock ip in r.getIpAddressBlock())
                {
                    foreach (Address ad in ip.getAddresses())
                    {
                        if (ad.getPrefix().Contains(prefixToFind))
                        {
                            Console.WriteLine(r);
                            if (!returnList.Contains(r)) returnList.Add(r);
                            //tsw.WriteLine(r.ToString());
                        }
                    }
                }
            }
            //tsw.Close();
            return returnList;
        }

        public static void searchAllRoasWithAs(String directoryToSearch, BigInteger AsToFind)
        {
            List<ROA> decodedROAS = new List<ROA>();

            foreach (string roaToOpen in Directory.GetFiles(directoryToSearch, "*.roa", SearchOption.AllDirectories))
            {
                byte[] fileRoa = File.ReadAllBytes(roaToOpen);
                byte[] extractedFileRoa = ExtractEnvelopedData.ExtractContent(fileRoa);
                ROA decodedRoa = DecoderData.DecodeROA(fileRoa, extractedFileRoa);
                decodedRoa.setCommonName(roaToOpen);
                decodedROAS.Add(decodedRoa);

                //Console.WriteLine(decodedRoa);
            }
            //61317 //"193.227.122.0"
            TextWriter tsw = new StreamWriter(@"C:\Users\zhoul\Desktop\As:" + AsToFind + ".txt");
            foreach (ROA r in decodedROAS)
            {

                if (r.getAsNumber() == AsToFind)
                {
                    Console.WriteLine(r);
                    tsw.WriteLine(r);
                }
            }
            tsw.Close();

        }

        public static bool searchRevocation(String directoryToSearch, BigInteger serialNumber)
        {
            List<CRL> decodedCrls = new List<CRL>();

            foreach (string crlToOpen in Directory.GetFiles(directoryToSearch, "*.crl", SearchOption.AllDirectories))
            {
                byte[] fileCrl = File.ReadAllBytes(crlToOpen);
                CRL decodedCrl = DecoderData.DecodeCRL(fileCrl);
                decodedCrls.Add(decodedCrl);
                foreach (revokedCertificate r in decodedCrl.getRevokedCertificates())
                {
                    if (r.getSerialNumber() == serialNumber)
                    {
                        Console.WriteLine(decodedCrl);
                        return true;
                    }

                }
            }
            return false;

        }

    }
}
