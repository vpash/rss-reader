using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RssDb.AccessLayer
{
    public interface IRepo<T> where T : class
    {
        IEnumerable<T> GetAll();

        Task<IEnumerable<T>> GetAllAsync();

        T Get(int id);

        Task<T> GetAsync(int id);

        IEnumerable<T> Find(Func<T, Boolean> predicate);

        void Create(T item);

        void Update(T item);

        void Delete(int id);
    }
}
