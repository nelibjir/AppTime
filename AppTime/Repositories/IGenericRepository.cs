using System;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace AppTime.Repositories
{
    public interface IGenericRepository<T>
    {
        void SaveAll();
        Task<int> SaveAllAsync(CancellationToken cancellationToken);
        Task<T[]> FindManyAsync(Expression<Func<T, bool>> condition, CancellationToken cancellationToken);
        Task<T[]> FindAllAsync(CancellationToken cancellationToken);
    }
}
