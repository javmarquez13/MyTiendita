using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyTiendita.Data.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        IProductRepository ProductRepository { get; }
        IStockRepository StockRepository { get; }
        Task<int> SaveAsync();
        void CreateTransaction();
        void Commit();
        void Rollback();
    }
}
