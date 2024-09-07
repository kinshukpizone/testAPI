using Application.General.IPersistence;
using Domain.General.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Infrastructure.Persistence
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private readonly ApplicationDbContext _context;

        protected DbSet<TEntity> _entities;

        public Repository(ApplicationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _entities = _context.Set<TEntity>();
        }
        protected virtual DbSet<TEntity> Entities
        {
            get
            {
                if (_entities == null)
                {
                    _entities = _context.Set<TEntity>();
                }
                return _entities;
            }
        }

        public IQueryable<TEntity> Table => Entities;

        public IQueryable<TEntity> TableNoTracking => Entities.AsNoTracking();

        public async Task AddAsync(TEntity entity)
        {
            try
            {
                IsEntityNullOrEmpty(entity);
                await Entities.AddAsync(entity);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException exception)
            {
                throw new Exception(await GetFullErrorTextAndRollbackEntityChanges(exception));
            }
        }

        public async Task AddRangeAsync(IEnumerable<TEntity>? entities)
        {
            try
            {
                IsEntityNullOrEmpty(entities!);
                await Entities.AddRangeAsync(entities!);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException exception)
            {
                throw new Exception(exception.Message, exception.InnerException);
            }
        }

        public async Task<bool> Any(Expression<Func<TEntity, bool>> where)
        {
            try
            {
                return await Entities.AsNoTracking().AnyAsync(where);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<int> CountAsync()
        {
            try
            {
                return await Entities.CountAsync();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<int> CountAsync(Expression<Func<TEntity, bool>> where)
        {
            try
            {
                return await Entities.CountAsync(where);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<bool> DeleteAsync(object id)
        {
            try
            {
                var entity = await Entities.FindAsync(id);
                if (entity != null)
                {
                    Entities.Remove(entity);
                    return true;
                }
                return false;
            }
            catch (DbUpdateException exception)
            {
                throw new Exception(await GetFullErrorTextAndRollbackEntityChanges(exception));
            }
        }
        public async Task<bool> DeleteAsync(params TEntity[] entities)
        {
            if (entities != null)
            {
                Entities.RemoveRange(entities);
                return true;
            }
            return false;           
        }
        public async Task<bool> DeleteAsync(IEnumerable<TEntity> entities)
        {
            if (entities != null)
            {
                Entities.RemoveRange(entities);
                return true;
            }
            return false;
        }
        

        public async Task<bool> DeleteAsync(TEntity entity)
        {
            try
            {
                IsEntityNullOrEmpty(entity);
                Entities.Remove(entity);
                return true;
            }
            catch (DbUpdateException exception)
            {
                throw new Exception(await GetFullErrorTextAndRollbackEntityChanges(exception));
            }
        }

        public async Task<bool> DeleteAsync(Expression<Func<TEntity, bool>> where)
        {
            try
            {
                var entities = Entities.Where(where);
                Entities.RemoveRange(entities);
                return true;
            }
            catch (DbUpdateException exception)
            {
                throw new Exception(await GetFullErrorTextAndRollbackEntityChanges(exception));
            }
        }

        public async Task<TEntity?> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> where)
        {
            try
            {
                return await Entities.AsNoTracking().FirstOrDefaultAsync(where);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IQueryable<TEntity> Get(Expression<Func<TEntity, bool>> predicate)
        {
            return Entities.Where(predicate).AsNoTracking();
        }

        public IQueryable<TEntity> GetAll(Expression<Func<TEntity, bool>>? predicate = null, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null)
        {
            IQueryable<TEntity> query = Entities;

            if (include != null)
            {
                query = include(query);
            }

            if (predicate != null)
            {
                query = query.Where(predicate).AsNoTracking();
            }
            return query.AsQueryable();
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            try
            {
                return await Entities.ToListAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<TEntity> GetAsync(object id)
        {
            try
            {
#pragma warning disable CS8603 // Possible null reference return.
                return await Entities.FindAsync(id);
#pragma warning restore CS8603 // Possible null reference return.
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> where)
        {
            try
            {
#pragma warning disable CS8603 // Possible null reference return.
                return await Entities.AsNoTracking().FirstOrDefaultAsync(where);
#pragma warning restore CS8603 // Possible null reference return.
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool RemoveRange(Expression<Func<TEntity, bool>>? predicate = null)
        {
            var records = Entities.Where(predicate!).ToList();
            if (records.Count > 0)
            {
                Entities.RemoveRange(records);
                _context.SaveChanges();
                return true;
            }
            return false;
        }

        public async Task<bool> SoftDeleteAsync(TEntity entity)
        {
            try
            {
                if (entity != null)
                {
                    IsEntityNullOrEmpty(entity);
                    dynamic obj = (dynamic)entity;
                    if (obj!.ActivityStatus != ActivityStatus.SOFT_DELETE.ToString())
                    {
                        obj!.ActivityStatus = ActivityStatus.SOFT_DELETE.ToString();
                        entity = (TEntity)obj;
                        Entities.Update(entity);
                        await _context.SaveChangesAsync();
                        return true;
                    }
                }
                return false;
            }
            catch (DbUpdateException exception)
            {
                throw new Exception(await GetFullErrorTextAndRollbackEntityChanges(exception));
            }
        }

        public async Task<bool> SoftDeleteAsync(Guid Id)
        {
            try
            {
                var entity = await GetAsync(Id);
                if (entity != null)
                {
                    IsEntityNullOrEmpty(entity);
                    dynamic obj = (dynamic)entity;
                    if (obj!.ActivityStatus != ActivityStatus.SOFT_DELETE.ToString())
                    {
                        obj!.ActivityStatus = ActivityStatus.SOFT_DELETE.ToString();
                        entity = (TEntity)obj;
                        Entities.Update(entity);
                        await _context.SaveChangesAsync();
                        return true;
                    }
                }
                return false;
            }
            catch (DbUpdateException exception)
            {
                throw new Exception(await GetFullErrorTextAndRollbackEntityChanges(exception));
            }
        }

        public async Task UpdateAsync(TEntity entity)
        {
            try
            {
                IsEntityNullOrEmpty(entity);
                Entities.Update(entity);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException exception)
            {
                throw new Exception(await GetFullErrorTextAndRollbackEntityChanges(exception));
            }
        }

        protected void IsEntityNullOrEmpty(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException($"Null value not acceptable", $"Entity ${nameof(entity)}");
            }
        }

        protected void IsEntityNullOrEmpty(IEnumerable<TEntity> entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException($"Null value not acceptable", $"Entity ${nameof(entity)}");
            }
        }

        protected async Task<string> GetFullErrorTextAndRollbackEntityChanges(DbUpdateException exception)
        {
            try
            {
                if (_context is DbContext dbContext)
                {
                    var entries = dbContext.ChangeTracker.Entries()
                        .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified).ToList();

                    entries.ForEach(entry =>
                    {
                        try
                        {
                            entry.State = EntityState.Unchanged;
                        }
                        catch (InvalidOperationException)
                        {
                            // ignore
                        }
                    });

                    await _context.SaveChangesAsync();
                    return exception.ToString();

                }
            }
            catch (Exception ex)
            {
                // if after the rollback of changes the context still not saving,
                // then return the full text of the exception that occurred when saving
                return ex.ToString();
            }
            return exception.ToString();
        }

    }
}
