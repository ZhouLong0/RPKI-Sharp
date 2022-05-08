using System;
using System.IO;
using System.Security.Cryptography.Pkcs;
using System.Formats.Asn1;
using System.Numerics;
using System.Collections.Generic;
using RPKIdecoder.ExtractEveloped;
using RPKIdecoder.MftClass;
using System.Security.Cryptography.X509Certificates;
using Org.BouncyCastle.X509;
using RPKIdecoder.CrlClass;

namespace RPKIdecoder
{
    class RPKIdecoder
    {
       

        static void Main(string[] args)
        {
            /***************************************** open ALL files from directory path *****************************************/


            string directoryPath = @"C:\Users\zhoul\Desktop\Nostromo2122";
            string directoryPathYearBefore = @"C:\Users\zhoul\Desktop\2021\02\02\out\rta\validated\cb.rg.net";

            List<MFT> decodedData = DirectoryDecoder.decode(directoryPath);


            //check if all files of the directory have the same IssuerNumber
            if (decodedData[0] != null)
            {
                bool sameIssuerNumber = true;
                string issuerNumber = decodedData[0].getIssuerNumber();

                foreach (MFT mft in decodedData)
                {
                    if (mft.getIssuerNumber() != issuerNumber)
                    {
                        sameIssuerNumber = false;
                        break;
                    }
                }

                if (sameIssuerNumber)
                    Console.WriteLine("ALL certificates of the directory have the same ISSUER NUMBER : " + issuerNumber + "\n\n\n");
                else
                    Console.WriteLine("NOT ALL certificates of the directory have the same ISSUER NUMBER \n\n\n");

            }


            //Console.WriteLine("\nNEXT YEAR\n");

            //List<MFT> decodedData2 = DirectoryDecoder.decode(directoryPath2);



            /******************************************************************************************************************/
            /************************************* TAKE A CRL AND FIND REVOKED FILES ******************************************/
            /******************************************************************************************************************/
            //string crlPath = @"C:\Users\zhoul\Desktop\Nostromo2122\nostromo.heficed.net\repo\635153\0\7D99D2758F25260E783E9FADCBA2F104EE8FA5EB.crl";
            //string directoryToSearch = @"C:\Users\zhoul\Desktop\Nostromo2122";

            //CRL decodedCrl = DecoderData.DecodeCRL(File.ReadAllBytes(crlPath));
            //Console.WriteLine(decodedCrl);

            //foreach (string roaToOpen in Directory.GetFiles(directoryToSearch, "*.roa", SearchOption.AllDirectories))
            //{
            //    byte[] fileRoa = File.ReadAllBytes(roaToOpen);
            //    byte[] extractedFileRoa = ExtractEnvelopedData.ExtractContent(fileRoa);
            //    ROA decodedRoa = DecoderData.DecodeROA(fileRoa, extractedFileRoa);
            //    decodedRoa.setCommonName(roaToOpen);



            //    Console.WriteLine(decodedRoa);
            //}











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
