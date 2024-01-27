using Microsoft.EntityFrameworkCore.Storage;
using MyTiendita.Data.Repositories;
using MyTiendita.Persistence.Database;

namespace MyTiendita.Data.RepositoryImplementation
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        private IDbContextTransaction _objTran;

        public IProductRepository ProductRepository { get; }
        public IStockRepository StockRepository { get; }

        public UnitOfWork(ApplicationDbContext context, IProductRepository productRepository, IStockRepository stockRepository)
        {
            this._context = context;
            this.ProductRepository = productRepository;
            this.StockRepository = stockRepository;
        }

        public async Task<int> SaveAsync()
            => await _context.SaveChangesAsync();

        public async void CreateTransaction()
        {
            _objTran = await _context.Database.BeginTransactionAsync();
        }

        public async void Commit()
        {
            _objTran.Commit();
        }

        public async void Rollback()
        {
            await _objTran.RollbackAsync();
            await _objTran.DisposeAsync();
        }


        public void Dispose()
        {
            _context.Dispose();
        }
    }
}