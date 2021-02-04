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
        private readonly string oAuthTokenWrong = "AgAAAABQWsjJAADLW0VGaust6UwNu5ku8ec3xEwbv";
        private readonly string urlDiskApi = $"https://cloud-api.yandex.net/v1/disk";
        //WebClient wclient;
        private static readonly HttpClient client = new HttpClient();

        public YandexDisk()
        {
            //wclient = new WebClient();
        }

        public void ShowSetting()
        {
            Console.WriteLine("oAuthToken = " + oAuthToken);
            Console.WriteLine("urlDiskApi = " + urlDiskApi);
        }

        public async Task GetResources(string pathFolder)
        {
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Add("Authorization", " OAuth " + oAuthToken);
            try
            {
                var stringTask = client.GetStringAsync(urlDiskApi + "/resources?path=" + pathFolder);

                var msg = await stringTask;
                GetResources answer = JsonConvert.DeserializeObject<GetResources>(msg);

                Console.WriteLine("Содержимое: " + pathFolder);
                foreach (var item in answer._embedded.items)
                {
                    Console.WriteLine((item.type == "dir"?"папка: ":"файл: ") + item.name);
                }
            }
            catch (HttpRequestException ex) {
                Console.WriteLine("ex.Message: " + ex.Message);
                
            }
            //Console.Write(answer._embedded.items);
        }
        
    }
}
