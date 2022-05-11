using RPKIdecoder.CrlClass;
using RPKIdecoder.ExtractEveloped;
using System;
using System.Collections.Generic;
using System.IO;
using System.Numerics;
using System.Text;

namespace RPKIdecoder
{
    static class Find
    {

        public static void searchAllRoasWithIp(String directoryToSearch, String IpToFind)
        {
            System.Net.IPAddress ipaddress = System.Net.IPAddress.Parse(IpToFind);
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
            TextWriter tsw = new StreamWriter(@"C:\Users\zhoul\Desktop\" + IpToFind +".txt");
            foreach (ROA r in decodedROAS)
            {
                foreach (IpAddressBlock ip in r.getIpAddressBlock())
                {
                    foreach (Address ad in ip.getAddresses())
                    {
                        if (ad.getIpAddress().Equals(ipaddress))
                            tsw.WriteLine(r.ToString());
                    }
                }
                //if(r.getAsNumber() == 61317)
                //{
                //    Console.WriteLine(r);
                //    tsw.WriteLine(r);
                //}
            }
            tsw.Close();
        }

        public static void searchAllRoasWithAs(String directoryToSearch, BigInteger AsToFind )
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

        public static void searchRevocation(String directoryToSearch, BigInteger serialNumber)
        {
            List<CRL> decodedCrls = new List<CRL>();
            TextWriter tsw = new StreamWriter(@"C:\Users\zhoul\Desktop\" + serialNumber + ".txt");
            foreach (string crlToOpen in Directory.GetFiles(directoryToSearch, "*.crl", SearchOption.AllDirectories))
            {
                byte[] fileCrl = File.ReadAllBytes(crlToOpen);
                CRL decodedCrl = DecoderData.DecodeCRL(fileCrl);
                decodedCrls.Add(decodedCrl);
                foreach (revokedCertificate r in decodedCrl.getRevokedCertificates())
                {
                    if (r.getSerialNumber() == serialNumber)
                        tsw.WriteLine(decodedCrl);
                }
            }
            tsw.Close();
        }

    }
}
