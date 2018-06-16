using RssDb.Entities;

namespace RssDb.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<RssDb.FeedDb>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
            Database.SetInitializer(new DropCreateDatabaseAlways<FeedDb>());
        }

        protected override void Seed(FeedDb context)
        {
            context.FeedProviders.AddOrUpdate(x => x.Title, new FeedProvider { Title = "Интерфакс", Url = "http://www.interfax.by/news/feed" });
            context.FeedProviders.AddOrUpdate(x => x.Title, new FeedProvider { Title = "Хабрахабр", Url = "http://habrahabr.ru/rss/" });
        }
    }
}
