using System;

namespace WebScrapeApi
{
    public class RefuseColletionInfoModel
    {
        public string CollectionDate { get; set; }
        public string CollectionType { get; set; }

    }

    public class PostModel
    {
        public string HouseNumber { get; set; }
        public string PostCode { get; set; }
    }


    public enum CollectionType
    {
        Refuse,
        Recycling,
    }
}
