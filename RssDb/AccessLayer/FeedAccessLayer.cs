using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RssDb.Entities;

namespace RssDb.AccessLayer
{
    public class FeedAccessLayer : IDisposable
    {
        private FeedDb db;
        private FeedItemRepository feedItemRepository;
        private FeedProviderRepository feedProviderRepository;

        public FeedAccessLayer()
        {
            db = new FeedDb();
        }

        public FeedDb Db => db;

        public IRepo<FeedItem> FeedItems
        {
            get
            {
                if (feedItemRepository == null)
                    feedItemRepository = new FeedItemRepository(db);
                return feedItemRepository;
            }
        }

        public IRepo<FeedProvider> FeedProviders
        {
            get
            {
                if (feedProviderRepository == null)
                    feedProviderRepository = new FeedProviderRepository(db);
                return feedProviderRepository;
            }
        }

        public bool IsItemExist(string title, DateTime publishDate)
        {
            return db.FeedItems.Any(x => x.Title == title && x.PublishDate == publishDate);
        }

        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    db.Dispose();
                }
                this.disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void Save()
        {
            db.SaveChanges();
        }
    }
}