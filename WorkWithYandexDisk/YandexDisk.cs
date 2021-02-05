using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using WorkWithYandexDisk.YandexDiskResponse;


namespace WorkWithYandexDisk
{
    class YandexDisk
    {
        private readonly string oAuthToken = "AgAAAABQWsjJAADLW0VGaust6UwNu5ku8ec3xEw";
        private readonly string oAuthTokenWrong = "AgAAAABQWsjJAADLW0VGaust6UwNu5ku8ec3xEwbv";
        private readonly string urlDiskApi = $"https://cloud-api.yandex.net/v1/disk";
        
        private static readonly HttpClient client = new HttpClient();

        public YandexDisk()
        {
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Add("Authorization", " OAuth " + oAuthToken);
        }

        public void ShowSetting()
        {
            Console.WriteLine("oAuthToken = " + oAuthToken);
            Console.WriteLine("urlDiskApi = " + urlDiskApi);
        }

        public async Task GetResources(string pathFolder)
        {
            try
            {
                var stringTask = client.GetStringAsync(urlDiskApi + "/resources?path=" + pathFolder);
                GetResources answer = JsonConvert.DeserializeObject<GetResources>(await stringTask);

                Console.WriteLine(Environment.NewLine + "Содержимое: " + pathFolder);
                foreach (var item in answer._embedded.items)
                {
                    Console.WriteLine((item.type == "dir"?"папка: ":"файл: ") + item.name);
                }
            }
            catch (HttpRequestException ex) 
            {
                Console.WriteLine("ex.Message: " + ex.Message);
            }
        }

        public async Task PutResources(string pathFolder)
        {
            try
            {
                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Put, urlDiskApi + "/resources?path=" + pathFolder);
                var response = await client.SendAsync(request);
                var msg = response.StatusCode;
                Console.WriteLine(Environment.NewLine + "Ответ на создание папки: " + response.StatusCode);

            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine("ex.Message: " + ex.Message);
            }
        }


        public async Task GetResourcesDownload(string pathFile)
        {
            try
            {
                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, urlDiskApi + "/resources/download?path=" + pathFile);
                var response = await client.SendAsync(request);
                DownloadLink answer = JsonConvert.DeserializeObject<DownloadLink>(await response.Content.ReadAsStringAsync());
                int position = answer.href.IndexOf("?");
                string downloadlink = answer.href.Substring(0, position);
                string[] subs = answer.href[(position + 1)..].Split('&');
                SortedDictionary<string, string> tagHref = new SortedDictionary<string, string>();
                foreach (var item in subs)
                {
                    position = item.IndexOf("=");
                    tagHref.Add(item.Substring(0, position), item[(position + 1)..]);
                }

                WebClient wclient = new WebClient();
                wclient.DownloadFile(new Uri(answer.href), @"C:\Test_Desktop\" + HttpUtility.UrlDecode(tagHref["filename"]));

                Console.WriteLine(Environment.NewLine + "Ответ на получении ссылки на скачивание: " + response.StatusCode);
                Console.WriteLine($"File " + HttpUtility.UrlDecode(tagHref["filename"]) + " downloaded to C:\\Test_Desktop");
                //Console.WriteLine("Ссылка на скачивание: " + answer.href);

            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine("ex.Message: " + ex.Message);
            }
        }


        public async Task GetResourcesUpload(string pathFile, string urlLink)
        {
            try
            {
                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, urlDiskApi + "/resources/upload?path=" + urlLink);
                var response = await client.SendAsync(request);
                UploadLink answer = JsonConvert.DeserializeObject<UploadLink>(await response.Content.ReadAsStringAsync());
                try
                {
                    WebClient wclient = new WebClient();
                    wclient.Credentials = CredentialCache.DefaultCredentials;
                    wclient.UploadFileAsync(new Uri(answer.href), "PUT", pathFile);
                    //wclient.Dispose();

                    Console.Write(Environment.NewLine + "Downloading: " + pathFile + " Status: ");
                    var left1 = Console.CursorLeft;
                    var top1 = Console.CursorTop;
                    Console.CursorVisible = false;

                    while (true)
                    {
                        request = new HttpRequestMessage(HttpMethod.Get, urlDiskApi + "/operations/" + answer.operation_id);
                        response = await client.SendAsync(request);
                        GetOperations statusOperationId = JsonConvert.DeserializeObject<GetOperations>(await response.Content.ReadAsStringAsync());
                        
                        Console.SetCursorPosition(left1, top1);
                        Console.WriteLine(statusOperationId.status + "    ");
                        if (statusOperationId.status != "in-progress") break;

                        Thread.Sleep(1000);
                    }
                    
                }
                catch (Exception ex)
                {
                    Console.WriteLine("GetResourcesUpload ex.Message: " + ex.Message);
                }

                //Console.WriteLine(Environment.NewLine + "Ответ на получении ссылки на закачивание на сервер: " + response.StatusCode);
                //Console.WriteLine($"File " + pathFile + " downloaded to " + urlLink);
                //Console.WriteLine("Ссылка на скачивание: " + answer.href);

            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine("GetResourcesUpload ex.Message: " + ex.Message);
            }
        }



        public async Task ParallellUploadFiles(string pathDir, string urlLink)
        {
            if (!Directory.Exists(pathDir)) return;
            string[] filesFullname = Directory.GetFiles(pathDir);
            string[] filesName = new DirectoryInfo(pathDir).GetFiles().Select(o => o.Name).ToArray();

            //существует ли папка/urlLink // её создание

            for (int i = 0; i < filesName.Length; i++)
            {
                Console.WriteLine("ParallellUploadFiles begin ");
                await GetResourcesUpload(filesFullname[i], urlLink + "/" + filesName[i]);
                Console.WriteLine("ParallellUploadFiles end");
            }

            Console.WriteLine("ParallellUploadFiles");

        }


    }
}
