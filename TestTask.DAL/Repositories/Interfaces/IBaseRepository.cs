using System.Threading.Tasks;

namespace TestTask.DAL.Repositories
{
    public interface IBaseRepository<TEntity> where TEntity: class, new () 
    {
        Task<TEntity> AddAsync(TEntity entity);
    }
}
