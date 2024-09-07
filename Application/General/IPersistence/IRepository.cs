using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Application.General.IPersistence
{
    public interface IRepository<T> where T : class
    {

        /// <summary>
        /// For add data in 'T' table
        /// </summary>
        /// <param name="entity">T class object</param>
        /// <returns></returns>
        Task AddAsync(T entity);

        /// <summary>
        /// For add multiple data in T class object table
        /// </summary>
        /// <param name="entities">List of T type of data</param>
        /// <returns></returns>
        Task AddRangeAsync(IEnumerable<T>? entities);

        /// <summary>
        /// For update data in 'T' table
        /// </summary>
        /// <param name="entity">T class object</param>
        /// <returns></returns>

        Task UpdateAsync(T entity);

        /// <summary>
        /// delete data from table by using primary id
        /// </summary>
        /// <param name="id">T class object primary id</param>
        /// <returns></returns>

        Task<bool> SoftDeleteAsync(T entity);

        /// <summary>
        /// delete data from table by using primary id
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        Task<bool> SoftDeleteAsync(Guid Id);

        /// <summary>
        /// delete data from table by using primary id
        /// </summary>
        /// <param name="id">T class object primary id</param>
        /// <returns></returns>

        Task<bool> DeleteAsync(object id);

        /// <summary>
        /// delete data from table by specific 'T' object
        /// </summary>
        /// <param name="entity">delete T object class</param>
        /// <returns></returns>
        Task<bool> DeleteAsync(T entity);
        Task<bool> DeleteAsync(params T[] entities);
        Task<bool> DeleteAsync(IEnumerable<T> entities);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="where">delete T object data by various condition</param>
        /// <returns></returns>
        Task<bool> DeleteAsync(Expression<Func<T, bool>> where);

        /// <summary>
        /// get a data from table by using primary id
        /// </summary>
        /// <param name="id">primary id of 'T' class object</param>
        /// <returns></returns>
        Task<T> GetAsync(object id);

        /// <summary>
        /// get a data from 'T' table by specifc query
        /// </summary>
        /// <param name="where">condition</param>
        /// <returns></returns>
        Task<T> GetAsync(Expression<Func<T, bool>> where);

        /// <summary>
        /// get all data from 'T' table without any condition
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<T>> GetAllAsync();

        /// <summary>
        /// count the number of data
        /// </summary>
        /// <returns></returns>
        Task<int> CountAsync();

        /// <summary>
        /// count the number of data by using specific conditions
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        Task<int> CountAsync(Expression<Func<T, bool>> where);

        /// <summary>
        /// find the data by using specific conditions
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        Task<T?> FirstOrDefaultAsync(Expression<Func<T, bool>> where);

        /// <summary>
        /// data is exist or not
        /// </summary>
        /// <param name="where">condition</param>
        /// <returns></returns>
        Task<bool> Any(Expression<Func<T, bool>> where);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        IQueryable<T> Get(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="include"></param>
        /// <returns></returns>
        IQueryable<T> GetAll(Expression<Func<T, bool>>? predicate = null, Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        Boolean RemoveRange(Expression<Func<T, Boolean>>? predicate = null);

        #region properties
        /// <summary>
        /// Gets a table
        /// </summary>
        IQueryable<T> Table { get; }

        /// <summary>
        /// Gets a table with "no tracking" enabled (EF features)
        /// use it only when load record
        /// </summary>
        IQueryable<T> TableNoTracking { get; }
        #endregion

    }
}
