using System.Threading.Tasks;
using Common;
using web.api.DataAccess;

namespace web.api.App.Common
{
    public abstract class BaseCrudService<T, T_SearchResult, T_SearchParams>
    {
        protected AppDbContext _context;

        protected BaseCrudService(AppDbContext context)
        {
            _context = context;
        }

        public abstract Task<T> Get(int id);
        public abstract Task<T> Create(T entity);
        public abstract Task<T> Update(T evt);
        public abstract Task Delete(int id);
        public abstract Task<PageView<T_SearchResult>> Search(T_SearchParams searchParams);
        public abstract Task CheckRights(int id, string username);
    }
}