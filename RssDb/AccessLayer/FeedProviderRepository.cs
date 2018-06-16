using RssDb.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RssDb.AccessLayer
{
    public class FeedProviderRepository : IRepo<FeedProvider>
    {
        private FeedDb db;

        public FeedProviderRepository(FeedDb dbContext)
        {
            db = dbContext;
        }

        public void Create(FeedProvider item)
        {
            db.FeedProviders.Add(item);
        }

        public void Delete(int id)
        {
            var item = db.FeedProviders.Find(id);

            if (item == null) return;

            db.FeedProviders.Remove(item);
        }

        public IEnumerable<FeedProvider> Find(Func<FeedProvider, bool> predicate)
        {
            return db.FeedProviders.Where(predicate).ToList();
        }

        public FeedProvider Get(int id)
        {
            return db.FeedProviders.Find(id);
        }

        public IEnumerable<FeedProvider> GetAll()
        {
            return db.FeedProviders;
        }

        public void Update(FeedProvider item)
        {
            db.Entry(item).State = EntityState.Modified;
        }

        public async Task<IEnumerable<FeedProvider>> GetAllAsync()
        {
            return await db.FeedProviders.ToListAsync();
        }

        public async Task<FeedProvider> GetAsync(int id)
        {
            return await db.FeedProviders.FindAsync(id);
        }
    }
}
