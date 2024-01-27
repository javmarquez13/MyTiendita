using MyTiendita.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyTiendita.Data.Repositories;

public interface IGenericRepository<T> where T : ModelBase
{
    Task<T> GetByIdAsync(int Id);
    IQueryable<T> GetAll();
    Task<T> AddAsync(T entity);
    Task AddRangeAsync(IEnumerable<T> entities);
    Task<bool> SoftDeleteAsync(T entity);
    Task<bool> HardDeleteAsync(int id);
    void Update(T entity);
    void UpdateRange(IEnumerable<T> entities);
}
