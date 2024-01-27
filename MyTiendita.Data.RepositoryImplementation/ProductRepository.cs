using MyTiendita.Data.Repositories;
using MyTiendita.Domain;
using MyTiendita.Persistence.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyTiendita.Data.RepositoryImplementation;

public class ProductRepository : GenericRepository<Product>, IProductRepository
{
    public ProductRepository(ApplicationDbContext context) : base(context)
    {

    }
}
