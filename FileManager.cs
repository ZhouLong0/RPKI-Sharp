using System;
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
        public static string DownloadFile(DateTime date)
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


            WebClient mywebClient = new WebClient();
            mywebClient.DownloadFile("https://ftp.ripe.net/rpki/ripencc.tal/" + year + "/" + month + "/" + day + "/repo.tar.gz",
                @"C:\Users\zhoul\Desktop\AutoDownloadFolder\" + year + "-" + month + "-" + day + ".tar.gz");

            return @"C:\Users\zhoul\Desktop\AutoDownloadFolder\" + year + "-" + month + "-" + day + ".tar.gz";
        }


        
    }
}
