using Org.BouncyCastle.X509;
using RPKIdecoder.CrlClass;
using RPKIdecoder.ExtractEveloped;
using RPKIdecoder.MftClass;
using System;
using System.Formats.Asn1;
using System.IO;
using System.Net;
using System.Numerics;
using System.Security.Cryptography.Pkcs;

namespace RPKIdecoder
{
    class DecoderData


    {
        /* Method that takes an array of bytes of a ROA file and returns the decoded content in a class */
        public static ROA DecodeROA(byte[] fileRoa, byte[] roaData)
        {
            ROA decodedRoa = new ROA();

            int totalByte = roaData.Length;
            int decodedByte, length, consumed;                            //  for the offset calculation 
            int undecodedBit;                          
            int offset = 0;

            decodedRoa.setStartDateTime(ExtractEnvelopedData.ExtractStartDateTime(fileRoa));
            decodedRoa.setEndDateTime(ExtractEnvelopedData.ExtractEndDateTime(fileRoa));
            decodedRoa.setSerialNumber(ExtractEnvelopedData.ExtractSerialNumber(fileRoa));
            decodedRoa.setIssuerNumber(ExtractEnvelopedData.ExtractIssuerNumber(fileRoa));
            /***********************************************************************************/
            /******************* DECODING SEQUENCE: ROUTE ORIGIN ADDRESS ***********************/
            /***********************************************************************************/
            AsnDecoder.ReadSequence(roaData[offset..], AsnEncodingRules.DER, out decodedByte, out length, out consumed, Asn1Tag.Sequence);
            offset += decodedByte;

            /********************** Decoding As number block **********************************/
            BigInteger asNumber = AsnDecoder.ReadInteger(roaData[offset..], AsnEncodingRules.DER, out decodedByte, Asn1Tag.Integer);
            offset += decodedByte;
            decodedRoa.setAsNumber(asNumber);

            /***********************************************************************************/
            /********************* DECODING SEQUENCE: IPADDRESS BLOCK **************************/
            AsnDecoder.ReadSequence(roaData[offset..], AsnEncodingRules.DER, out decodedByte, out length, out consumed, Asn1Tag.Sequence);
            offset += decodedByte;

            while (offset < totalByte)
            {
                IpAddressBlock decodedIpAddressBlock = new IpAddressBlock();

                /***************************** Decoding sequence block *****************************/
                AsnDecoder.ReadSequence(roaData[offset..], AsnEncodingRules.DER, out decodedByte, out length, out consumed, Asn1Tag.Sequence);
                offset += decodedByte;

                /***************************** Decoding Ip format block **************************/
                byte[] format = AsnDecoder.ReadOctetString(roaData[offset..], AsnEncodingRules.DER, out decodedByte, Asn1Tag.PrimitiveOctetString);
                offset += decodedByte;
                if((format[0]!= 0) || ((format[1] != 1) && (format[1] != 2))) throw new NotExistingTypeOfPrefix();
                if (format[1] == 1)
                {
                    //Console.WriteLine("                                IPV4 ");
                    decodedIpAddressBlock.setAddressFamily("IPV4");
                }

                if (format[1] == 2) {
                    //Console.WriteLine("                                IPV6 ");
                    decodedIpAddressBlock.setAddressFamily("IPV6");
                }
                /***********************************************************************************/
                /************************** DECODING SEQUENCE: ADDRESSES ***************************/
                AsnDecoder.ReadSequence(roaData[offset..], AsnEncodingRules.DER, out decodedByte, out length, out consumed, Asn1Tag.Sequence);
                offset += decodedByte;
                //Console.Write("Address: ");



                /***************************** Decoding ALL Address sequence block ************************/
                Boolean cycle = true;
                while (cycle)
                {
                    Address decodedAddress = new Address();
                    

                    /***************************** Decoding sequence block *****************************/
                    AsnDecoder.ReadSequence(roaData[offset..], AsnEncodingRules.DER, out decodedByte, out length, out consumed, Asn1Tag.Sequence);
                    offset += decodedByte;

                    /***************************** Decoding addresses block *********************************/
                    byte[] address = AsnDecoder.ReadBitString(roaData[offset..], AsnEncodingRules.DER, out undecodedBit, out decodedByte, Asn1Tag.PrimitiveBitString);
                    offset += decodedByte;
                    int netMask = (address.Length * 8) - undecodedBit;

                    byte[] finalAddress;
                    if (format[1] == 1) 
                    { 
                        finalAddress = new byte[4];
                    }
                    else
                    {
                        finalAddress = new byte[16];
                    }

                    for (int i = 0; i<address.Length; i++)
                    {
                        finalAddress[i] = address[i];
                    }

                    IPAddress ip = new IPAddress(finalAddress);
                   
                    decodedAddress.setIpAddress(ip);
                    decodedAddress.setNetmask(netMask);
                    decodedAddress.setPrefix(new IPNetwork(ip, (byte) netMask));
                    

                    decodedIpAddressBlock.getAddresses().Add(decodedAddress);
                   
                    /* Try to see if there is maxLength*/
                    Boolean decodifica = true;
                    try
                    {
                        AsnDecoder.ReadInteger(roaData[offset..], AsnEncodingRules.DER, out decodedByte, Asn1Tag.Integer);
                    }
                    catch (AsnContentException c)
                    {
                        decodifica = false;
                    }

                    if (decodifica)
                    {
                        BigInteger maxLength = AsnDecoder.ReadInteger(roaData[offset..], AsnEncodingRules.DER, out decodedByte, Asn1Tag.Integer);
                        offset += decodedByte;
                  
                        decodedAddress.setMaxLength((int)maxLength);

                        if (format[1] == 1)
                            if (maxLength > 32) throw new InvalidMaxLength();
                        if (format[1] == 2)
                            if (maxLength > 128) throw new InvalidMaxLength();

                    }



                    /* exit the cycle? */
                    try
                    {
                        AsnDecoder.ReadSequence(roaData[offset..], AsnEncodingRules.DER, out decodedByte, out length, out consumed, Asn1Tag.Sequence);
                        int offsetTemp = offset + decodedByte;
                        AsnDecoder.ReadBitString(roaData[offsetTemp..], AsnEncodingRules.DER, out undecodedBit, out decodedByte, Asn1Tag.PrimitiveBitString);
                    }
                    catch (AsnContentException c)
                    {
                        cycle = false;
                    }

                }
                decodedRoa.getIpAddressBlock().Add(decodedIpAddressBlock);
            }

            return decodedRoa;
        }



        public static MFT DecodeMFT(byte[] fileMft,byte[] roaData)
        {
            int offset, length, consumed, decodedByte, numberOfTotalByte, undecodedByte;
            numberOfTotalByte = roaData.Length;
            offset = 0;

            MFT decodedMFT = new MFT();
            decodedMFT.setSerialNumber(ExtractEnvelopedData.ExtractSerialNumber(fileMft));
            decodedMFT.setIssuerNumber(ExtractEnvelopedData.ExtractIssuerNumber(fileMft));

            /***********************************************************************************/
            /********************** DECODING SEQUENCE OF MANIFEST BLOCK ************************/
            /***********************************************************************************/
            AsnDecoder.ReadSequence(roaData[offset..], AsnEncodingRules.DER, out decodedByte, out length, out consumed, Asn1Tag.Sequence);
            offset += decodedByte;


            /********************** Decoding manifest number block ************************/
            BigInteger manifestNumber = AsnDecoder.ReadInteger(roaData[offset..], AsnEncodingRules.DER, out decodedByte, Asn1Tag.Integer);
            offset += decodedByte;
            decodedMFT.setManifestNumber(manifestNumber);

            /********************** Decoding time block ************************/
            DateTimeOffset thisUpdate = AsnDecoder.ReadGeneralizedTime(roaData[offset..], AsnEncodingRules.DER, out decodedByte, Asn1Tag.GeneralizedTime);
            offset += decodedByte;
            decodedMFT.setThisUpdate(thisUpdate);


            DateTimeOffset nextUpdate = AsnDecoder.ReadGeneralizedTime(roaData[offset..], AsnEncodingRules.DER, out decodedByte, Asn1Tag.GeneralizedTime);
            offset += decodedByte;
            decodedMFT.setNextUpdate(nextUpdate);


            /********************** Decoding file hash algorithm block ************************/
            String fileHashAlg = AsnDecoder.ReadObjectIdentifier(roaData[offset..], AsnEncodingRules.DER, out decodedByte, Asn1Tag.ObjectIdentifier);
            offset += decodedByte;
            decodedMFT.setFileHashAlg(fileHashAlg);
           

            /***********************************************************************************/
            /*********************** DECODING SEQUENCE : FILE LIST *****************************/
            AsnDecoder.ReadSequence(roaData[offset..], AsnEncodingRules.DER, out decodedByte, out length, out consumed, Asn1Tag.Sequence);
            offset += decodedByte;


            /********************** Decoding N sequence of file list block ***********************/
            while (offset < numberOfTotalByte)
            {

                FileAndHash fileAndHash = new FileAndHash();

                AsnDecoder.ReadSequence(roaData[offset..], AsnEncodingRules.DER, out decodedByte, out length, out consumed, Asn1Tag.Sequence);
                offset += decodedByte;

                /******************************* Decoding file block *******************************/
                String fileName = AsnDecoder.ReadCharacterString(roaData[offset..], AsnEncodingRules.DER, UniversalTagNumber.IA5String, out decodedByte, null);
                offset += decodedByte;
                fileAndHash.setFile(fileName);

                /************************** Decoding Ip address block ******************************/
                byte[] hash = AsnDecoder.ReadBitString(roaData[offset..], AsnEncodingRules.DER, out undecodedByte, out decodedByte, Asn1Tag.PrimitiveBitString);
                offset += decodedByte;
                fileAndHash.setHash(hash);
                decodedMFT.getFileAndHashes().Add(fileAndHash);
            }
            return decodedMFT;
        }

        public static CRL DecodeCRL(byte[] roaData)
        {
            X509CrlParser crlParser = new X509CrlParser();
            X509Crl decodedX509Crl = crlParser.ReadCrl(roaData);

            CRL decodedCrl = new CRL();
            decodedCrl.setIssuerName(decodedX509Crl.IssuerDN.ToString());
            decodedCrl.setThisUpdate(decodedX509Crl.ThisUpdate);
            decodedCrl.setNextUpdate(decodedX509Crl.NextUpdate.Value);

            if (decodedX509Crl.GetRevokedCertificates() != null)
            {
                foreach (X509CrlEntry rc in decodedX509Crl.GetRevokedCertificates())
                {
                    revokedCertificate revokedCertificate = new revokedCertificate();
                    revokedCertificate.setSerialNumber(BigInteger.Parse(rc.SerialNumber.ToString()));
                    revokedCertificate.setRevocationTime(rc.RevocationDate);
                    decodedCrl.getRevokedCertificates().Add(revokedCertificate);
                }
            }
            return decodedCrl;
        }

    }
}
