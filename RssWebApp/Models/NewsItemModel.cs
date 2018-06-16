using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RssWebApp.Models
{
    public class NewsItemModel
    {
        public string Provider { get; set; }

        public string Title { get; set; }

        public string Body { get; set; }

        public DateTime PubDate { get; set; }
    }
}