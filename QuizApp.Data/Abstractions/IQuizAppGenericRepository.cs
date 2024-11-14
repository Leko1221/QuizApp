using System.Linq.Expressions;
using System.Reflection;

namespace QuizApp.Data.Abstractions
{
    public interface IQuizAppGenericRepository<TEntity>
    {
        Task<TEntity> AddAsync(TEntity entity);
        Task<int> CountAsync();
        Task DeleteAsync(object id);
        Task ExecuteInTransactionAsync(Func<Task> action);
        Task<bool> ExistsAsync(object id);
        IQueryable<TEntity> Find(IQueryable<TEntity> dbSet, Expression<Func<TEntity, bool>> filter = null);
        IQueryable<TEntity> FindWithPagination(IQueryable<TEntity> dbSet, int resultsPerPage, int pageNumber, out int totalPages, out int totalCount, out int resultCount, Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null);
        IQueryable<TEntity> GetAll();
        Task<TEntity> GetByIdAsync(object id);
        List<PropertyInfo> GetNavigationProperties();
        Task<TEntity> UpdateAsync(TEntity entity);
        Task UpdateManyAsync(IQueryable<TEntity> entities);
    }
}