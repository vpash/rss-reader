using RssDb.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RssDb.AccessLayer
{
    public class FeedItemRepository : IRepo<FeedItem>
    {
        private FeedDb db;

        public FeedItemRepository(FeedDb dbContext)
        {
            db = dbContext;
        }

        public void Create(FeedItem item)
        {
            db.FeedItems.Add(item);
        }

        public void Delete(int id)
        {
            var item = db.FeedItems.Find(id);

            if (item == null) return;

            db.FeedItems.Remove(item);
        }

        public IEnumerable<FeedItem> Find(Func<FeedItem, bool> predicate)
        {
            return db.FeedItems.Where(predicate).ToList();
        }

        public FeedItem Get(int id)
        {
           return db.FeedItems.Find(id);
        }

        public IEnumerable<FeedItem> GetAll()
        {
            return db.FeedItems;
        }

        public void Update(FeedItem item)
        {
            db.Entry(item).State = EntityState.Modified;
        }

        public async Task<IEnumerable<FeedItem>> GetAllAsync()
        {
            return await db.FeedItems.ToListAsync();
        }

        public async Task<FeedItem> GetAsync(int id)
        {
            return await db.FeedItems.FindAsync(id);
        }
    }
}
