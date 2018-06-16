using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace RssConsoleApp
{
    public static class FeadReader
    {
        private const string ACCEPT_HEADER = "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8";
        private const string USER_AGENT = "Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:60.0) Gecko/20100101 Firefox/60.0";

        private static readonly HttpClient _httpClient = new HttpClient();

        public static List<FeedReaderItem> ParseItems(string url)
        {
            var list = new List<FeedReaderItem>();

            var content = LoadContent(url);

            content = content.Replace(((char)0x1C).ToString(), string.Empty);
            content = content.Replace(((char)65279).ToString(), string.Empty);
            content = (new Regex(@"[\n]{1}")).Replace(content, string.Empty);

            XDocument document = XDocument.Parse(content);

            foreach (var i in document.Descendants("item"))
            {
                list.Add(new FeedReaderItem
                {
                    Title = i.Element("title")?.Value ?? "empty",
                    Body = i.Element("description")?.Value ?? "empty",
                    Url = i.Element("link")?.Value ?? "empty",
                    PubDate = TryParseDate(i.Element("pubDate")?.Value),
                });
            }

            return list;
        }

        public static string LoadContent(string url)
        {
            return LoadContentAsync(url).Result;
        }

        public static async Task<string> LoadContentAsync(string url)
        {
            url = System.Net.WebUtility.UrlDecode(url);
            HttpResponseMessage response;

            using (var request = new HttpRequestMessage(HttpMethod.Get, url))
            {
                request.Headers.TryAddWithoutValidation("Accept", ACCEPT_HEADER);
                request.Headers.TryAddWithoutValidation("User-Agent", USER_AGENT);
                response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead);
            }

            if (!response.IsSuccessStatusCode)
            {
                if ((int)response.StatusCode == 308)
                {
                    url = response.Headers?.Location?.AbsoluteUri ?? url;
                }

                using (var request = new HttpRequestMessage(HttpMethod.Get, url))
                {
                    response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead);
                }
            }

            return Encoding.UTF8.GetString(await response.Content.ReadAsByteArrayAsync());
        }

        public static DateTime TryParseDate(string datetime)
        {
            if (string.IsNullOrWhiteSpace(datetime))
                return DateTime.MinValue;

            if (!DateTimeOffset.TryParse(datetime, DateTimeFormatInfo.CurrentInfo, DateTimeStyles.None, out var dt))
            {
                if (datetime.Contains(","))
                {
                    int pos = datetime.IndexOf(',') + 1;
                    string newdtstring = datetime.Substring(pos).Trim();

                    DateTimeOffset.TryParse(newdtstring, DateTimeFormatInfo.CurrentInfo, DateTimeStyles.None, out dt);
                }
            }

            if (dt == default(DateTimeOffset))
                return DateTime.MinValue;

            return dt.UtcDateTime;
        }
    }
}
