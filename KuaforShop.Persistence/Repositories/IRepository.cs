using KuaforShop.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace KuaforShop.Persistence.Repositories
{
    public interface IRepository<T> where T : BaseEntity
    {
        DbSet<T> Table { get; }

        //Read methods
        IQueryable<T> GetAll(bool tracking = true);
        IQueryable<T> GetWhere(Expression<Func<T, bool>> method, bool tracking = true);
        Task<T> GetSingleAsync(Expression<Func<T, bool>> method, bool tracking = true);
        Task<T> GetByIdAsync(Guid id, bool tracking = true);


        //Write methods
        Task<bool> AddAsync(T model);
        Task<bool> AddRangeAsync(List<T> datas);
        bool Remove(T model);
        bool RemoveRange(List<T> datas);
        Task<bool> RemoveAsync(Guid id);
        bool Update(T model);
        Task<int> SaveAsync();
    }
}

