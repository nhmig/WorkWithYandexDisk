using System;
using System.Collections.Generic;
using System.Text;

namespace WorkWithYandexDisk.YandexDiskResponse
{
    class GetResources
    {
        public _Embedded _embedded { get; set; }
        public string name { get; set; }
        public Exif1 exif { get; set; }
        public string resource_id { get; set; }
        public DateTime created { get; set; }
        public DateTime modified { get; set; }
        public string path { get; set; }
        public Comment_Ids1 comment_ids { get; set; }
        public string type { get; set; }
        public long revision { get; set; }
    }

    public class _Embedded
    {
        public string sort { get; set; }
        public Item[] items { get; set; }
        public int limit { get; set; }
        public int offset { get; set; }
        public string path { get; set; }
        public int total { get; set; }
    }

    public class Item
    {
        public string name { get; set; }
        public Exif exif { get; set; }
        public DateTime created { get; set; }
        public string resource_id { get; set; }
        public DateTime modified { get; set; }
        public string path { get; set; }
        public Comment_Ids comment_ids { get; set; }
        public string type { get; set; }
        public long revision { get; set; }
        public string antivirus_status { get; set; }
        public int size { get; set; }
        public string mime_type { get; set; }
        public string file { get; set; }
        public string media_type { get; set; }
        public string preview { get; set; }
        public string sha256 { get; set; }
        public string md5 { get; set; }
    }

    public class Exif
    {
        public DateTime date_time { get; set; }
    }

    public class Comment_Ids
    {
        public string private_resource { get; set; }
        public string public_resource { get; set; }
    }

    public class Exif1
    {
    }

    public class Comment_Ids1
    {
        public string private_resource { get; set; }
        public string public_resource { get; set; }
    }

}
