using Microsoft.EntityFrameworkCore;

namespace Application.General.IPersistence
{
    public interface IApplicationDBContext
    {
        DbSet<TEntity> Set<TEntity>() where TEntity : class;
        Task<int> SaveChangesAsync();

        /// ============================================= Entity DbSets ===========================================
        /// 
    }
}
