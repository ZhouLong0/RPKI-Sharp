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


            /********************************user****************************/
            //Console.WriteLine("Insert IP: ");
            //IPAddress ipAddress = IPAddress.Parse(Convert.ToString(Console.ReadLine()));
            //Console.WriteLine("Insert cidr: ");
            //int cidr = Convert.ToInt32(Console.ReadLine());
            //Console.WriteLine("Insert date (dd/mm/year): ");
            //DateTime date = Convert.ToDateTime(Console.ReadLine());

            DateTime date = new DateTime(2015, 01, 01);
            int cidr = 18;
            IPAddress ipAddress = IPAddress.Parse("194.154.32.0");



            IPNetwork prefix = new IPNetwork(ipAddress, (Byte)cidr);
            String folder = @"C:\Users\zhoul\Desktop\AutoDownloadFolder\";
            UseCase.verifyIPAddress(prefix, cidr, date, folder);
            //Controller.verifyIPAddress(ipAddress, cidr, date, folder);

           











        }
    }
}
