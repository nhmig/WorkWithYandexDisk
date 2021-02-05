using System;
using System.Threading.Tasks;

namespace WorkWithYandexDisk
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            YandexDisk yd = new YandexDisk();
            yd.ShowSetting();
            //await yd.GetResources("disk:/SecretFolder");

            //await yd.PutResources("disk:/SecretFolder/nFolder");

            //await yd.GetResourcesDownload("disk:/Test/Горы.jpg");

            //await yd.GetResourcesUpload(@"C:\Test_Desktop\Asp.netCore3.1.pdf", "disk:/SecretFolder/Asp.netCore3.1.pdf&overwrite=true");

            await yd.ParallellUploadFiles(@"C:\Test_Desktop", "disk:/SecretFolder/TopSecret");
        }
    }
}


    