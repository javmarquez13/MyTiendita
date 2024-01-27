using Microsoft.EntityFrameworkCore;
using MyTiendita.Data.Repositories;
using MyTiendita.Domain;
using MyTiendita.Persistence.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyTiendita.Data.RepositoryImplementation;

public abstract class GenericRepository<T> : IGenericRepository<T> where T : ModelBase
{
    protected DbSet<T> Entitites => _context.Set<T>();
    protected readonly ApplicationDbContext _context;

    protected GenericRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<T> AddAsync(T entity)
    {
        await Entitites.AddAsync(entity);
        return entity;
    }
    public async Task<T> GetByIdAsync(int Id)
        => await Entitites.FindAsync(Id);

    public void Update(T entity)
    {
        entity.Updated = DateTime.Now;
        Entitites.Update(entity);
    }

    public void UpdateRange(IEnumerable<T> entities)
    {
        Entitites.UpdateRange(entities);
    }
    public async Task AddRangeAsync(IEnumerable<T> entities)
    {
        await Entitites.AddRangeAsync(entities);
    }

    public IQueryable<T> GetAll()
        => Entitites;

    public async Task<bool> SoftDeleteAsync(T entity)
    {
        Entitites.Attach(entity);
        entity.IsActive = false;
        var entry = Entitites.Entry(entity);
        entry.State = EntityState.Modified;
        return true;
    }
    public async Task<bool> HardDeleteAsync(int id)
    {
        T entity = await GetByIdAsync(id);

        if (entity == null)
            return false;

        Entitites.Remove(entity);
        return true;
    }
}
