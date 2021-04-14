using System;
using System.Threading.Tasks;

namespace WorkWithYandexDisk
{
    class Program
    {
        static async Task Main(string[] args)
        {
            YandexDisk yd = new YandexDisk();
            yd.ShowSetting();

            if (args.Length < 2)
            {
                Console.WriteLine("Нет входных данных");
                return;
            }

            //string pathDir =  @"C:\Test_Desktop";
            //string urlLink = "disk:/SecretFolder/TopSecret";

            await yd.ParallellUploadFiles(args[0], args[1], true);
            Console.ReadKey();
        }
    }
}


    