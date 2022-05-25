using System;
using System.Collections.Generic;
using System.IO;
using System.Net;


namespace RPKIdecoder
{
    class Controller
    {
        //verify if the prefix is valid and find the best fit roa
        public static bool verifyIPAddress(IPNetwork prefix, int cidr, DateTime date, String folder)
        {
            String extractedFolder = folder + date.Year + "-" + date.Month + "-" + date.Day;
            Console.WriteLine("IP address entered :   " + prefix);
            Console.WriteLine("DateTime entered :   " + date);
            if (!Directory.Exists(extractedFolder))
            {
                Console.Write(DateTime.Now + "   ");
                String gzFile = FileManager.DownloadFile(date, folder);
                Console.Write(DateTime.Now + "   ");
                FileManager.ExtractTGZ(gzFile, extractedFolder);
            }
            else
            {
                Console.WriteLine("Certificates already in local");
            }

            Console.Write(DateTime.Now + "\n\n");

            List<ROA> usefullRoas = Find.searchAllRoasWithIp(extractedFolder, prefix);

            //for each certificate that refers to the ip, check if it's revocated
            Console.WriteLine("Number of roas :" + usefullRoas.Count);
            if (usefullRoas.Count != 0)
            {
                foreach (ROA r in usefullRoas)
                {
                    if (Find.searchRevocation(extractedFolder, r.getSerialNumber()))
                        usefullRoas.Remove(r);
                }

                if (usefullRoas.Count != 0)
                {
                    if (usefullRoas.Count > 1)
                    {
                        ROA tmp = usefullRoas[0];
                        int c = 0;
                        //best matching prefix
                        foreach (ROA r in usefullRoas)
                        {
                            foreach (IpAddressBlock ip in r.getIpAddressBlock())
                            {
                                foreach (Address ad in ip.getAddresses())
                                {
                                    if (ad.getPrefix().Contains(prefix))
                                    {
                                        if (ad.getNetmask() > c)
                                        {
                                            tmp = r;
                                            c = ad.getNetmask();
                                        }
                                        //tsw.WriteLine(r.ToString());
                                    }
                                }
                            }

                        }
                        Console.WriteLine("Valid! Roa with best prefix : \n\n" + tmp);
                    }
                    else
                        Console.WriteLine("Valid ! Only 1 certificate found");
                }

                else
                {
                    Console.WriteLine("Prefix Not Valid because the certificate has been revoked");
                }

            }
            else
            {
                Console.WriteLine("IP address not certificated");
            }
            Console.Write(DateTime.Now + "   ");

            return true;
        }

        public static bool verifyIPAddress(IPAddress prefix, int cidr, DateTime date, String folder)
        {
            String extractedFolder = folder + date.Year + "-" + date.Month + "-" + date.Day;
            Console.WriteLine("IP address entered :   " + prefix);
            Console.WriteLine("DateTime entered :   " + date);
            if (!Directory.Exists(extractedFolder))
            {
                Console.Write(DateTime.Now + "   ");
                String gzFile = FileManager.DownloadFile(date, folder);
                Console.Write(DateTime.Now + "   ");
                FileManager.ExtractTGZ(gzFile, extractedFolder);
            }
            else
            {
                Console.WriteLine("Certificates already in local");
            }

            Console.Write(DateTime.Now + "\n\n");

            List<ROA> usefullRoas = Find.searchAllRoasWithIp(extractedFolder, prefix);

            //for each certificate that refers to the ip, check if it's revocated
            Console.WriteLine("Number of roas :" + usefullRoas.Count);
            if (usefullRoas.Count != 0)
            {
                foreach (ROA r in usefullRoas)
                {
                    if (Find.searchRevocation(extractedFolder, r.getSerialNumber()))
                        usefullRoas.Remove(r);
                }

                if (usefullRoas.Count != 0)
                {
                    if (usefullRoas.Count > 1)
                    {
                        ROA tmp = usefullRoas[0];
                        int c = 0;
                        //best matching prefix
                        foreach (ROA r in usefullRoas)
                        {
                            foreach (IpAddressBlock ip in r.getIpAddressBlock())
                            {
                                foreach (Address ad in ip.getAddresses())
                                {
                                    if (ad.getPrefix().Contains(prefix))
                                    {
                                        if (ad.getNetmask() > c)
                                        {
                                            tmp = r;
                                            c = ad.getNetmask();
                                        }
                                        //tsw.WriteLine(r.ToString());
                                    }
                                }
                            }

                        }
                        Console.WriteLine("Valid! Roa with best prefix : \n" + tmp);
                    }
                    else
                        Console.WriteLine("Valid ! Only 1 certificate found");
                }

                else
                {
                    Console.WriteLine("Prefix Not Valid because the certificate has been revoked");
                }

            }
            else
            {
                Console.WriteLine("IP address not certificated");
            }
            Console.Write(DateTime.Now + "   ");

            return true;
        }



    }
}
