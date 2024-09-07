using Domain.Entities.Admin;
using Domain.Entities.Location;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Application.General.IPersistence
{
    public interface IUnitOfWork
    {
        int Save();
        Task<int> SaveAsync();
        void BeginTransaction();
        void CommitTransaction();
        void RollbackTransaction();

        /// =========================================== Include Repository Patterns ===========================================
        /// 
        IRepository<State> StateRepository { get; }
        IRepository<City> CityRepository { get; }
        IRepository<Category> CategoryRepository { get; }
        IRepository<Product> ProductRepository { get; }
        IRepository<Banner> BannerRepository { get; }
        IRepository<Permission> PermissionRepository { get; }
        IRepository<Pages> PageRepository { get; }
    }
}
