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

            List<ROA> decodedRoas = new List<ROA>();
            List<MFT> decodedMftsList = new List<MFT>();

          
            string directoryPath = @"C:\Users\zhoul\Desktop\2022\02\02\out\rta\validated\rrdp.krill.cloud";

            //int a = Directory.GetFiles(directoryPath, "*.mft", SearchOption.AllDirectories).Length;
            //Console.WriteLine(a + "  files mft");

            foreach (string mftName in Directory.GetFiles(directoryPath, "*.mft", SearchOption.AllDirectories))
            {
                byte[] fileMft = File.ReadAllBytes(mftName);
                ExtractEnvelopedData.ExtractStartDateTime(fileMft);
                byte[] extracted = ExtractEnvelopedData.ExtractContent(fileMft);

                MFT decodedMft = DecoderData.DecodeMFT(fileMft,extracted);
                Console.WriteLine(" ************************ DECODING MFT ************************* ");
                Console.WriteLine(decodedMft);
                decodedMftsList.Add(decodedMft);
                
                /********************************** START TO DECODE CERTIFICATES IN THE MANIFEST **************************************/
                foreach (FileAndHash fh in decodedMft.getFileAndHashes())
                {
                    string fileToDecode = fh.getFile();

                    if (Path.GetExtension(fileToDecode) == ".roa")
                    {
                        foreach (string roaToOpen in Directory.GetFiles(directoryPath, "*" + fileToDecode, SearchOption.AllDirectories))
                        {
                            byte[] fileRoa = File.ReadAllBytes(roaToOpen);
                            byte[] extractedFileRoa = ExtractEnvelopedData.ExtractContent(fileRoa);
                            ROA decodedRoa = DecoderData.DecodeROA(fileRoa, extractedFileRoa);
                            Console.WriteLine("Common name: " + fileToDecode);
                            if (decodedRoa.getIssuerNumber() != decodedMft.getIssuerNumber())
                                throw new DifferentIssuerNumbersException();
                            Console.WriteLine(decodedRoa);
                            
                        }
                    }
                    if (Path.GetExtension(fileToDecode) == ".crl")
                    {
                        foreach (string roaToOpen in Directory.GetFiles(directoryPath, "*" + fileToDecode, SearchOption.AllDirectories))
                        {
                            byte[] fileCrl = File.ReadAllBytes(roaToOpen);
                            X509Crl decodedCrl = DecoderData.DecodeCRL(fileCrl); 
                            Console.WriteLine("Common name : " + fileToDecode);
                            if (decodedCrl.IssuerDN.ToString() != decodedMft.getIssuerNumber())
                                throw new DifferentIssuerNumbersException();
                            Console.WriteLine(decodedCrl);
                        }
                    }

                }
            }


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



            //foreach (MFT decMFT in decodedMfts)
            //{
            //    Console.WriteLine(decMFT);
            //}
            //Console.WriteLine(decodedMfts.Count + "mft files decoded");


            /*********************************************************************************************************
             ************************************************** CRL **************************************************
             *********************************************************************************************************/

            //int crlNum = Directory.GetFiles(@"C:\Users\zhoul\Desktop\2022\02\02\out\rta\validated", "*.crl", SearchOption.AllDirectories).Length;
            //Console.WriteLine(crlNum + "  files crl");
            //foreach (string crlName in Directory.GetFiles(@"C:\Users\zhoul\Desktop\2022\02\02\out\rta\validated", "*.crl", SearchOption.AllDirectories))
            //{
            //    byte[] fileCrl = File.ReadAllBytes(crlName);
            //    X509Crl decodedCrlFile = DecoderData.DecodeCRL(fileCrl);
            //    Console.WriteLine("******************* START DECODING CRL *****************\n");
            //    Console.WriteLine(decodedCrlFile);
            //    Console.WriteLine("\n\n");
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
