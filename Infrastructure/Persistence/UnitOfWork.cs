using Application.General.IPersistence;
using Domain.Entities.Admin;
using Domain.Entities.Location;
using Microsoft.EntityFrameworkCore.Storage;

namespace Infrastructure.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        IDbContextTransaction _dbContextTransaction;
        private readonly ApplicationDbContext _applicationDBContext;

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public UnitOfWork(ApplicationDbContext applicationDBContext) => _applicationDBContext = applicationDBContext;
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

        private bool _disposed = false;

        /// <summary>
        /// Save data in database in sync way
        /// </summary>
        /// <returns></returns>
        public int Save()
        {
            return _applicationDBContext.SaveChanges();
        }

        /// <summary>
        /// Save data in database in Async way
        /// </summary>
        /// <returns></returns>
        public async Task<int> SaveAsync()
        {
            return await _applicationDBContext.SaveChangesAsync();
        }

        /// <summary>
        /// 
        /// </summary>
        public void BeginTransaction()
        {
            _dbContextTransaction = _applicationDBContext.Database.BeginTransaction();
        }

        /// <summary>
        /// 
        /// </summary>
        public void CommitTransaction()
        {
            if (_dbContextTransaction != null)
            {
                _dbContextTransaction.Commit();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public void RollbackTransaction()
        {
            if (_dbContextTransaction != null)
            {
                _dbContextTransaction.Rollback();
            }
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed && disposing)
            {
                _applicationDBContext.Dispose();
            }
            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }


        IRepository<State> _stateRepository;
        public IRepository<State> StateRepository
        {
            get
            {
                if (_stateRepository == null)
                {
                    _stateRepository = new Repository<State>(_applicationDBContext);
                }
                return _stateRepository;
            }
        }

        IRepository<City> _cityRepository;
        public IRepository<City> CityRepository
        {
            get
            {
                if (_cityRepository == null)
                {
                    _cityRepository = new Repository<City>(_applicationDBContext);
                }
                return _cityRepository;
            }
        }
        IRepository<Category> _categoryRepository;
        public IRepository<Category> CategoryRepository
        {
            get
            {
                if (_categoryRepository == null)
                {
                    _categoryRepository = new Repository<Category>(_applicationDBContext);
                }
                return _categoryRepository;
            }
        }
        IRepository<Product> _productRepository;
        public IRepository<Product> ProductRepository
        {
            get
            {
                if (_productRepository == null)
                {
                    _productRepository = new Repository<Product>(_applicationDBContext);
                }
                return _productRepository;
            }
        }
        IRepository<Banner> _bannerRepository;
        public IRepository<Banner> BannerRepository
        {
            get
            {
                if (_bannerRepository == null)
                {
                    _bannerRepository = new Repository<Banner>(_applicationDBContext);
                }
                return _bannerRepository;
            }
        }
        IRepository<Permission> _permissionRepository;
        public IRepository<Permission> PermissionRepository
        {
            get
            {
                if (_permissionRepository == null)
                {
                    _permissionRepository = new Repository<Permission>(_applicationDBContext);
                }
                return _permissionRepository;
            }
        }
        IRepository<Pages> _pageRepository;
        public IRepository<Pages> PageRepository
        {
            get
            {
                if (_pageRepository == null)
                {
                    _pageRepository = new Repository<Pages>(_applicationDBContext);
                }
                return _pageRepository;
            }
        }
    }
}
