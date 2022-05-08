using Org.BouncyCastle.X509;
using RPKIdecoder.CrlClass;
using RPKIdecoder.ExtractEveloped;
using RPKIdecoder.MftClass;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace RPKIdecoder
{
    class DirectoryDecoder
    {
        public static List<MFT> decode(String directoryPath)
        {
            
            List<MFT> decodedMftsList = new List<MFT>();
          

            foreach (string mftName in Directory.GetFiles(directoryPath, "*.mft", SearchOption.AllDirectories))
            {
                byte[] fileMft = File.ReadAllBytes(mftName);
                ExtractEnvelopedData.ExtractStartDateTime(fileMft);
                byte[] extracted = ExtractEnvelopedData.ExtractContent(fileMft);

                MFT decodedMft = DecoderData.DecodeMFT(fileMft, extracted);
                Console.WriteLine(" ************************ DECODING MFT ************************* ");
                Console.WriteLine(decodedMft);
                


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
                            decodedRoa.setCommonName(fileToDecode);
                            if (decodedRoa.getIssuerNumber() != decodedMft.getIssuerNumber())
                                throw new DifferentIssuerNumbersException();
                            Console.WriteLine(decodedRoa);
                            decodedMft.getRoaList().Add(decodedRoa);
                            

                        }
                    }
                    if (Path.GetExtension(fileToDecode) == ".crl")
                    {
                        foreach (string roaToOpen in Directory.GetFiles(directoryPath, "*" + fileToDecode, SearchOption.AllDirectories))
                        {
                            byte[] fileCrl = File.ReadAllBytes(roaToOpen);
                            CRL decodedCrl = DecoderData.DecodeCRL(fileCrl);
                            Console.WriteLine("Common name : " + fileToDecode);
                            if (decodedCrl.getIssuerNumber() != decodedMft.getIssuerNumber())
                                throw new DifferentIssuerNumbersException();
                            Console.WriteLine(decodedCrl);
                            decodedMft.getCrlList().Add(decodedCrl);
                            
                        }
                    }

                }
                decodedMftsList.Add(decodedMft);
            }
            return decodedMftsList;
        }
    }
}
