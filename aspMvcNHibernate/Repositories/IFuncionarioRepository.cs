using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace aspMvcNHibernate.Repositories
{
    public interface IRepository<T> where T : class
    {
        Task Add(T item);

        Task Remove(long id);

        Task Updade(T item);

        Task FindById(long id);

        IEnumerable<T> FindAll();
    }
}
