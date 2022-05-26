using RPKIdecoder.CrlClass;
using RPKIdecoder.ExtractEveloped;
using RPKIdecoder.MftClass;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Numerics;

namespace RPKIdecoder
{
    class RPKIdecoder
    {


        static void Main(string[] args)
        {
            /**********************************************************************************************************************/
            /***************************************** open ALL files from directory path *****************************************/
            /**********************************************************************************************************************/


            string directoryPath = @"C:\Users\zhoul\Desktop\Nostromo2122";

            //List<MFT> decodedData = DirectoryDecoder.decode(directoryPath);


            ////check if all files of the directory have the same IssuerNumber
            //if (decodedData[0] != null)
            //{
            //    bool sameIssuerNumber = true;
            //    string issuerNumber = decodedData[0].getIssuerNumber();

            //    foreach (MFT mft in decodedData)
            //    {
            //        if (mft.getIssuerNumber() != issuerNumber)
            //        {
            //            sameIssuerNumber = false;
            //            break;
            //        }
            //    }

            //    if (sameIssuerNumber)
            //        Console.WriteLine("ALL certificates of the directory have the same ISSUER NUMBER : " + issuerNumber + "\n\n\n");
            //    else
            //        Console.WriteLine("NOT ALL certificates of the directory have the same ISSUER NUMBER \n\n\n");

            //}





            /******************************************************************************************************************/
            /************************************* TAKE A CRL AND FIND REVOKED FILES ******************************************/
            /******************************************************************************************************************/
            string directoryToSearch = @"C:\Users\zhoul\Desktop\Nostromo2122";



            //Find.searchAllRoasWithAs(directoryToSearch, 61317);

            //61317 //"193.227.122.0"

            ///////////////////////*k*///////////////////////////////////////////
            //IPAddress ip = IPAddress.Parse("193.227.122.0");
            //IPNetwork prefix = new IPNetwork(ip, (Byte)24);
            //Find.searchAllRoasWithIp(directoryToSearch, prefix);

            //BigInteger  toFind = BigInteger.Parse("448263085674351701227297377749888883408767793851");
            //Find.searchRevocation(directoryToSearch, toFind);
            //61317 //"193.227.122.0"


            //195.114.24.0/23   and   2a00:1f00::/32

            IPAddress ipAddress = IPAddress.Parse("194.154.32.0");
            int cidr = 20;
            DateTime date = new DateTime(2013, 01, 01);


            IPNetwork prefix = new IPNetwork(ipAddress, (Byte)cidr);
            String folder = @"C:\Users\zhoul\Desktop\AutoDownloadFolder\";
            Controller.verifyIPAddress(prefix, cidr, date, folder);
            Controller.verifyIPAddress(ipAddress, cidr, date, folder);

            //IPAddress ipaddress = IPAddress.Parse("193.250.0.0");
            //int cidr = 24;

            //IPNetwork prefix = new IPNetwork(ipaddress, (byte)cidr);

            //IPNetwork prefix2 = new IPNetwork(ipaddress, (byte)cidr);
            //IPNetwork prefix3 = new IPNetwork(ipaddress, (byte)25);
            //IPNetwork prefix4 = new IPNetwork(ipaddress, (byte)23);
            //IPNetwork prefix5 = new IPNetwork(IPAddress.Parse("193.250.1.0"), (byte)24);
            //Console.WriteLine(prefix.Total + "number of total ip");
            //Console.WriteLine(prefix.Contains(IPAddress.Parse("193.250.0.120")) + "   should be true");
            //Console.WriteLine(prefix.Contains(IPAddress.Parse("193.250.0.0")) + "   should be true");
            //Console.WriteLine(prefix.Contains(IPAddress.Parse("193.250.0.255")) + "   should be true");
            //Console.WriteLine(prefix.Contains(IPAddress.Parse("193.250.1.0")) + "   should be false");
            //Console.WriteLine(prefix.Overlap(prefix2) + "   should be true");
            //Console.WriteLine(prefix.Overlap(prefix3) + "   should be true");
            //Console.WriteLine(prefix.Overlap(prefix4) + "   should be true");
            //Console.WriteLine(prefix.Overlap(prefix5) + "   should be false");












            /***************************************** open only ONE file from file path *****************************************/
            //string pathROA = @"C:\Users\zhoul\Desktop\repo.tar\repo\ripencc.tal\2022\02\02\out\rta\validated\0.sb\0.sb\repo\sb\0\36322e3130362e37352e302f32342d3234203d3e2038383838.roa";
            //string pathMFT = @"C:\Users\zhoul\Desktop\ripencc.tal\2022\02\02\out\rta\validated\rpki.multacom.com\rpki.multacom.com\repo\MCOMCA\1\F827056C07EA5582DB063B6AF62F8F3076237CD7.mft";
            //byte[] asn1Data = File.ReadAllBytes(pathROA);
            //byte[] mftData = File.ReadAllBytes(pathMFT);

            //byte[] extractedROA = DecoderData.ExtractEnvelopedData(asn1Data);
            //DecoderData.DecodeMFT(mftData);
            //Console.WriteLine("\n Extracted!");
            ////DecoderData.DecodeROA(extractedROA);
            //DecoderData.DecodeMFT(extractedMFT);

            //byte[] asn1Data = File.ReadAllBytes(@"C:\Users\zhoul\Desktop\6a\00adb0-890d-47ad-a18c-4ad0abec275e\1\GqGckulmd_X5b2jxygqtn6MR60U.crl");
            //byte[] extractedROA = ExtractEnvelopedData.ExtractCrl(asn1Data);
            //File.WriteAllBytes(@"C:\Users\zhoul\Desktop\signedata2.crl", extractedROA

            //File.WriteAllBytes(@"C:\Users\zhoul\Desktop\signedata2.crl", extractedROA);



        }
    }
}
