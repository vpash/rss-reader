using RssWebApp.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace RssWebApp.EF
{
    public class DbOperations : IDisposable
    {
        private RssFeedEntities db;

        public DbOperations()
        {
            db = new RssFeedEntities();
        }

        public async Task<IndexModel> GetIndexModel(int pageSize, int page, int? providerId, NewsItemOrder order)
        {
            return new IndexModel
            {
                Items = await GetNewsItems(providerId, (page - 1) * pageSize, pageSize, order),
                Providers = await GetProviders(),
                Order = order,
                CurrentPage = page,
                PageSize = pageSize,
                TotalPages = await NewsCount(providerId) / pageSize,
                ProviderId = providerId,
            };
        }

        public async Task<List<ProviderModel>> GetProviders()
        {
            return await db.FeedProviders.Select(x => new ProviderModel { Id = x.Id, Title = x.Title, Url = x.Url }).ToListAsync();
        }

        public async Task<int> NewsCount(int? providerId)
        {
            return (providerId > 0) 
                ? await db.FeedItems.CountAsync(x => x.FeedProvider_Id == providerId) 
                : await db.FeedItems.CountAsync();
        }

        public async Task<List<NewsItemModel>> GetNewsItems(int? providerId, int skip, int take, NewsItemOrder order)
        {
            var query = db.FeedItems.AsQueryable();

            switch (order)
            {
                case NewsItemOrder.TitleDesc:
                    query = query.OrderByDescending(x => x.Title);
                    break;

                case NewsItemOrder.TitleAsc:
                    query = query.OrderBy(x => x.Title);
                    break;

                case NewsItemOrder.BodyDesc:
                    query = query.OrderByDescending(x => x.Body);
                    break;

                case NewsItemOrder.BodyAsc:
                    query = query.OrderBy(x => x.Body);
                    break;

                case NewsItemOrder.DateDesc:
                    query = query.OrderByDescending(x => x.PublishDate);
                    break;

                case NewsItemOrder.DateAsc:
                    query = query.OrderBy(x => x.PublishDate);
                    break;

                case NewsItemOrder.ProviderDesc:
                    query = query.OrderByDescending(x => x.FeedProvider.Title);
                    break;

                case NewsItemOrder.ProviderAsc:
                    query = query.OrderBy(x => x.FeedProvider.Title);
                    break;

                default:
                    query = query.OrderBy(x => x.Title);
                    break;
            }

            if (providerId > 0) query = query.Where(x => x.FeedProvider_Id == providerId);
            if (skip > 0) query = query.Skip(skip);
            if (take > 0) query = query.Take(take);

            return await query.Select(x => new NewsItemModel
            {
                Title = x.Title,
                Body = x.Body,
                PubDate = x.PublishDate,
                Provider = x.FeedProvider.Title,
            }).ToListAsync();
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