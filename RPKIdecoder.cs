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
            //Find.searchAllRoasWithIp(directoryToSearch, "193.227.129.0");

            //BigInteger  toFind = BigInteger.Parse("448263085674351701227297377749888883408767793851");
            //Find.searchRevocation(directoryToSearch, toFind);
            //61317 //"193.227.122.0"

            String ipAddress = "193.227.129.0";
            DateTime date = new DateTime(2021, 01, 01);
            String folder = @"C:\Users\zhoul\Desktop\AutoDownloadFolder\";
            Console.Write(DateTime.Now + "   ");
            String gzFile = FileManager.DownloadFile(date, folder);
            String extractedFolder = folder + date.Year + "-" + date.Month + "-" + date.Day;
            Console.Write(DateTime.Now + "   ");
            FileManager.ExtractTGZ(gzFile, extractedFolder);
            Console.Write(DateTime.Now + "   ");
            if (Find.searchAllRoasWithIp(extractedFolder, ipAddress).Count != 0)
            {

                Console.WriteLine("Valid IpAddress");
            }
            else
            {
                Console.WriteLine("IP address not certificated");
            }
            Console.Write(DateTime.Now + "   ");












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
