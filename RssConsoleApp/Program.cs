using RssDb;
using RssDb.AccessLayer;
using RssDb.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Syndication;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace RssConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var startDate = DateTime.Now;
            try
            {
                UpdateNews();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            Console.WriteLine($"\nExecuted: {DateTime.Now - startDate}");
            Console.ReadKey();
        }

        static void UpdateNews()
        {
            var defaultColor = Console.ForegroundColor;

            using (FeedAccessLayer layer = new FeedAccessLayer())
            {
                foreach (var p in layer.FeedProviders.GetAll())
                {
                    var list = FeadReader.ParseItems(p.Url);

                    Console.WriteLine($"           Источник: {p.Title}");
                    Console.WriteLine($"                URL: {p.Url}");
                    Console.WriteLine($" Загружено новостей: {list.Count}");
                    Console.WriteLine($"");

                    var saveCounter = 0;

                    foreach (var n in list)
                    {
                        if (!layer.IsItemExist(n.Title, n.PubDate))
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                            layer.FeedItems.Create(new FeedItem { Title = n.Title, Body = n.Body, FeedProvider = p, PublishDate = n.PubDate, Url = n.Url });
                            saveCounter++;
                        }

                        Console.WriteLine(n);

                        Console.ForegroundColor = defaultColor;
                    }

                    Console.WriteLine($"");
                    Console.WriteLine($"   Сохранено в БД: {saveCounter}");

                    Console.WriteLine($"");
                }

                layer.Save();
            }
        }
    }
}