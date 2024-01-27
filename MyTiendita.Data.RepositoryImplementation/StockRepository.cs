using MyTiendita.Data.Repositories;
using MyTiendita.Domain;
using MyTiendita.Persistence.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyTiendita.Data.RepositoryImplementation;

public class StockRepository : GenericRepository<Stock>, IStockRepository
{
    public StockRepository(ApplicationDbContext context) : base(context)
    {

    }
}
