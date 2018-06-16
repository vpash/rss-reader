using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RssConsoleApp
{
    public class FeedReaderItem
    {
        public string Title { get; set; }

        public string Url { get; set; }

        public string Body { get; set; }

        public DateTime PubDate { get; set; }

        public override string ToString()
        {
            return $"{PubDate}\tBody length: {Body.Length}\t{((Title.Length > 40) ? Title.Substring(0, 40) + "..." : Title)}";
        }
    }
}
