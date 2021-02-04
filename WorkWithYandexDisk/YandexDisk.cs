using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using WorkWithYandexDisk.YandexDiskResponse;

namespace WorkWithYandexDisk
{
    class YandexDisk
    {
        private readonly string oAuthToken = "AgAAAABQWsjJAADLW0VGaust6UwNu5ku8ec3xEw";
        private readonly string urlDiskApi = $"https://cloud-api.yandex.net/v1/disk";
        WebClient wclient;
        private static readonly HttpClient client = new HttpClient();

        public YandexDisk()
        {
            wclient = new WebClient();
        }

        public void ShowSetting()
        {
            Console.WriteLine("oAuthToken = " + oAuthToken);
            Console.WriteLine("urlDiskApi = " + urlDiskApi);
        }

        public async Task GetResources(string pathFolder)
        {
            //string s = wclient.DownloadString(urlDiskApi + "/resources?path=disk:/newMetFolder");
            //Console.WriteLine(s);

            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Add("Authorization", " OAuth " + oAuthToken);

            var stringTask = client.GetStringAsync(urlDiskApi + "/resources?path=disk:/newMetFolder");

            var msg = await stringTask;
            JsonConvert.DeserializeObject<GetResources>(msg);
            
            Console.Write(msg);
        }
        
    }
}
