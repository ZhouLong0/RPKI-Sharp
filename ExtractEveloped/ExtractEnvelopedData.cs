using System;
using System.Collections.Generic;
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
    }
}
