using RssDb.Entities;

namespace RssDb
{
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class FeedDb : DbContext
    {
        public FeedDb() : base("name=FeedDb") { }

        public virtual DbSet<FeedProvider> FeedProviders { get; set; }

        public virtual DbSet<FeedItem> FeedItems { get; set; }
    }
}