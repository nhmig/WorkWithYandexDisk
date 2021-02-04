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
            await yd.GetResources("disk:/newMetFolder");
        }
    }
}
