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

namespace RPKIdecoder
{
    class Program
    {
       

        static void Main(string[] args)
        {
            /***************************************** open ALL files from directory path *****************************************/

            //List<ROA> decodedRoas = new List<ROA>();
            //List<MFT> decodedMfts = new List<MFT>();

            //int aa = Directory.GetFiles(@"C:\Users\zhoul\Desktop\2022\02\02\out\rta\validated", "*.roa", SearchOption.AllDirectories).Length;
            //Console.WriteLine(aa + "  files roa");

            //foreach (string txtName in Directory.GetFiles(@"C:\Users\zhoul\Desktop\2022\02\02\out\rta\validated", "*.roa", SearchOption.AllDirectories))
            //{
            //    byte[] fileRoa = File.ReadAllBytes(txtName);
            //    byte[] extracted = ExtractEnvelopedData.ExtractContent(fileRoa);
            //    ROA decodedRoaFile = DecoderData.DecodeROA(extracted);
            //    decodedRoaFile.setStartDateTime(ExtractEnvelopedData.ExtractStartDateTime(fileRoa));
            //    decodedRoaFile.setEndDateTime(ExtractEnvelopedData.ExtractEndDateTime(fileRoa));
            //    decodedRoas.Add(decodedRoaFile);
            //    Console.WriteLine(decodedRoaFile);
            //}

            ////foreach (ROA decROA in decodedRoas)
            ////{
            ////    Console.WriteLine(decROA);
            ////}

            //int a = Directory.GetFiles(@"C:\Users\zhoul\Desktop\2022\02\02\out\rta\validated", "*.mft", SearchOption.AllDirectories).Length;
            //Console.WriteLine(a + "  files mft");
            //foreach (string mftName in Directory.GetFiles(@"C:\Users\zhoul\Desktop\2022\02\02\out\rta\validated", "*.mft", SearchOption.AllDirectories))
            //{
            //    byte[] fileMft = File.ReadAllBytes(mftName);
            //    byte[] extracted = ExtractEnvelopedData.ExtractContent(fileMft);
            //    decodedMfts.Add(DecoderData.DecodeMFT(extracted));
            //}

            //foreach (MFT decMFT in decodedMfts)
            //{
            //    Console.WriteLine(decMFT);
            //}
            //Console.WriteLine(decodedMfts.Count +"mft files decoded");


            /*********************************************************************************************************
             ************************************************** CRL **************************************************
             *********************************************************************************************************/

            int crlNum = Directory.GetFiles(@"C:\Users\zhoul\Desktop\2022\02\02\out\rta\validated", "*.crl", SearchOption.AllDirectories).Length;
            Console.WriteLine(crlNum + "  files crl");
            foreach (string crlName in Directory.GetFiles(@"C:\Users\zhoul\Desktop\2022\02\02\out\rta\validated", "*.crl", SearchOption.AllDirectories))
            {
                byte[] fileCrl = File.ReadAllBytes(crlName);
                X509Crl decodedCrlFile = DecoderData.DecodeCRL(fileCrl);
                Console.WriteLine("******************* START DECODING CRL *****************\n");
                Console.WriteLine(decodedCrlFile);
                Console.WriteLine("\n\n");
            }







            /***************************************** open only ONE file from file path *****************************************/
            //string pathROA = @"C:\Users\zhoul\Desktop\repo.tar\repo\ripencc.tal\2022\02\02\out\rta\validated\0.sb\0.sb\repo\sb\0\36322e3130362e37352e302f32342d3234203d3e2038383838.roa";
            //string pathMFT = @"C:\Users\zhoul\Desktop\ripencc.tal\2022\02\02\out\rta\validated\rpki.multacom.com\rpki.multacom.com\repo\MCOMCA\1\F827056C07EA5582DB063B6AF62F8F3076237CD7.mft";
            //byte[] asn1Data = File.ReadAllBytes(pathROA);
            //byte[] mftData = File.ReadAllBytes(pathMFT);

            //byte[] extractedROA = DecoderData.ExtractEnvelopedData(asn1Data);
            //byte[] extractedMFT = DecoderData.ExtractEnvelopedData(mftData);      
            //Console.WriteLine("\n Extracted!");
            //DecoderData.DecodeROA(extractedROA);
            //DecoderData.DecodeMFT(extractedMFT);

            //byte[] asn1Data = File.ReadAllBytes(@"C:\Users\zhoul\Desktop\6a\00adb0-890d-47ad-a18c-4ad0abec275e\1\GqGckulmd_X5b2jxygqtn6MR60U.crl");
            //byte[] extractedROA = ExtractEnvelopedData.ExtractCrl(asn1Data);
            //File.WriteAllBytes(@"C:\Users\zhoul\Desktop\signedata2.crl", extractedROA

            //File.WriteAllBytes(@"C:\Users\zhoul\Desktop\signedata2.crl", extractedROA);



        }
    }
}
