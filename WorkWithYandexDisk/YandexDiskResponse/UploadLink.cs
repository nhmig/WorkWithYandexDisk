using System;
using System.Collections.Generic;
using System.Text;

namespace WorkWithYandexDisk.YandexDiskResponse
{
    class UploadLink
    {
        public string operation_id { get; set; }
        public string href { get; set; }
        public string method { get; set; }
        public bool templated { get; set; }
    }

}
