using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RssWebApp.Models
{
    public enum NewsItemOrder
    {
        None,
        TitleDesc, TitleAsc,
        BodyDesc, BodyAsc,
        DateDesc, DateAsc,
        ProviderDesc, ProviderAsc
    }
}
