using ICSharpCode.SharpZipLib.GZip;
using ICSharpCode.SharpZipLib.Tar;
using System;
using System.IO;
using System.Net;

namespace RPKIdecoder
{
    /*
     * Class with method to download and unzip files
     */
    class FileManager
    {

        /*
         * Download a the tar.gz file from ftp.ripe matching the passed argument "date"
         */
        public static string DownloadFile(DateTime date, String destFolder)
        {
            int yearTmp = date.Year;
            int monthTmp = date.Month;
            int dayTmp = date.Day;
            string year = yearTmp.ToString();
            string month;
            string day;

            if (monthTmp < 10)
                month = "0" + monthTmp.ToString();
            else
                month = monthTmp.ToString();


            if (dayTmp < 10)
                day = "0" + dayTmp.ToString();
            else
                day = dayTmp.ToString();

            Console.WriteLine("Wait. I'm downloading the certificate!");
            WebClient mywebClient = new WebClient();
            mywebClient.DownloadFile("https://ftp.ripe.net/rpki/ripencc.tal/" + year + "/" + month + "/" + day + "/repo.tar.gz",
                destFolder + year + "-" + month + "-" + day + ".tar.gz");
            Console.WriteLine("Download of the certificate compleated!");
            return destFolder + year + "-" + month + "-" + day + ".tar.gz";
        }

        public static void ExtractTGZ(String gzArchiveName, String destFolder)
        {
            Stream inStream = File.OpenRead(gzArchiveName);
            Stream gzipStream = new GZipInputStream(inStream);
            Console.WriteLine("Wait. Extracting the certificate!");
            TarArchive tarArchive = TarArchive.CreateInputTarArchive(gzipStream);
            tarArchive.ExtractContents(destFolder);
            tarArchive.Close();

            gzipStream.Close();
            inStream.Close();
            Console.WriteLine("Extraction of the certificate completed!");
        }



    }
}
