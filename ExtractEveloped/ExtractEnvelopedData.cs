using System;
using System.Collections.Generic;
using System.Numerics;
using System.Security.Cryptography.Pkcs;
using System.Text;

namespace RPKIdecoder.ExtractEveloped
{
    class ExtractEnvelopedData
    {
        public static DateTimeOffset ExtractStartDateTime(byte[] signature)
        {
            if (signature == null)
                throw new ArgumentNullException("signature");

            // decode the signature
            SignedCms cms = new SignedCms();
            cms.Decode(signature);
            if (cms.Detached)
                throw new InvalidOperationException("Cannot extract enveloped content from a detached signature.");

            return cms.Certificates[0].NotBefore;
        }

        public static DateTimeOffset ExtractEndDateTime(byte[] signature)
        {
            if (signature == null)
                throw new ArgumentNullException("signature");

            // decode the signature
            SignedCms cms = new SignedCms();
            cms.Decode(signature);

            if (cms.Detached)
                throw new InvalidOperationException("Cannot extract enveloped content from a detached signature.");

            return cms.Certificates[0].NotAfter;
        }


        public static byte[] ExtractContent(byte[] signature)
        {
            if (signature == null)
                throw new ArgumentNullException("signature");

            // decode the signature
            SignedCms cms = new SignedCms();
            cms.Decode(signature);

            if (cms.Detached)
                throw new InvalidOperationException("Cannot extract enveloped content from a detached signature.");

            return cms.ContentInfo.Content;
        }

        public static byte[] ExtractCrl(byte[] signature)
        {
            if (signature == null)
                throw new ArgumentNullException("signature");

            // decode the signature
            SignedCms cms = new SignedCms();
            cms.Decode(signature);

            if (cms.Detached)
                throw new InvalidOperationException("Cannot extract enveloped content from a detached signature.");

            return cms.ContentInfo.Content;
        }

        public static BigInteger ExtractSerialNumber(byte[] signature)
        {
            if (signature == null)
                throw new ArgumentNullException("signature");

            // decode the signature
            SignedCms cms = new SignedCms();
            cms.Decode(signature);
            if (cms.Detached)
                throw new InvalidOperationException("Cannot extract enveloped content from a detached signature.");

            return new BigInteger(cms.Certificates[0].GetSerialNumber());
        }

        public static string ExtractIssuerNumber(byte[] signature)
        {
            if (signature == null)
                throw new ArgumentNullException("signature");

            // decode the signature
            SignedCms cms = new SignedCms();
            cms.Decode(signature);
            if (cms.Detached)
                throw new InvalidOperationException("Cannot extract enveloped content from a detached signature.");

            return cms.Certificates[0].GetIssuerName();
        }

       public static void ExtractSetUp(ROA roa, byte[] signature)
        {
            if (signature == null)
                throw new ArgumentNullException("signature");

            SignedCms cms = new SignedCms();
            cms.Decode(signature);
            if (cms.Detached)
                throw new InvalidOperationException("Cannot extract enveloped content from a detached signature.");

            roa.setStartDateTime(cms.Certificates[0].NotBefore);
            roa.setEndDateTime(cms.Certificates[0].NotAfter);
            roa.setIssuerNumber(cms.Certificates[0].GetIssuerName());
            roa.setSerialNumber(new BigInteger(cms.Certificates[0].GetSerialNumber()));
        }
    }
}
