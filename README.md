# RPKI Sharp

An RPKI Parser written in C#

## How to use

Parse all ROAs and CRLs of a tal directory: 

```
string DirectoryWithCertificates = @"C:\...";

List<ROA> DecodedROAs = DirectoryDecoder.DecodeRoas(DirectoryWithCertificates);

List<CRL> DecodedCRLs = DirectoryDecoder.DecodeCrls(DirectoryWithCertificates);
```
