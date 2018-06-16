using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RssWebApp.Models
{
    public class IndexModel
    {
        public List<NewsItemModel> Items { get; set; }

        public List<ProviderModel> Providers { get; set; }

        public int CurrentPage { get; set; }

        public int? ProviderId { get; set; }

        public int TotalPages { get; set; }

        public int PageSize { get; set; }

        public NewsItemOrder Order { get; set; }

        public Dictionary<RssWebApp.Models.NewsItemOrder, string> OrderDict { get; set; } = new Dictionary<RssWebApp.Models.NewsItemOrder, string>
        {
            { RssWebApp.Models.NewsItemOrder.DateDesc, "Date DESC" },
            { RssWebApp.Models.NewsItemOrder.DateAsc, "Date ASC" },
            { RssWebApp.Models.NewsItemOrder.ProviderDesc, "Provider DESC" },
            { RssWebApp.Models.NewsItemOrder.ProviderAsc, "Provider ASC" },
            { RssWebApp.Models.NewsItemOrder.TitleDesc, "Title DESC" },
            { RssWebApp.Models.NewsItemOrder.TitleAsc, "Title ASC" },
        };
    }
}