using Microsoft.EntityFrameworkCore;
using EmployeeManagementSystem.Shared.Domain;
using System.Linq.Expressions;

namespace EmployeeManagementSystem.Shared.Repository
{
    public class BaseRepository<TId, TEntity>
        where TId : struct
        where TEntity : Entity<TId, TEntity>
    {
        private readonly DbSet<TEntity> _dbSet;
        private readonly DbContext _context;

        public BaseRepository(DbContext context)
        {
            _dbSet = context.Set<TEntity>();
            _context = context;
        }

        public async Task<TEntity> AddAsync(TEntity obj)
        {
            await _dbSet.AddAsync(obj);
            await _context.SaveChangesAsync();
            return obj;
        }

        public void Update(TEntity obj)
        {
            _dbSet.Update(obj);
        }

        public void Remove(TEntity obj)
        {
            _dbSet.Remove(obj);
        }

        public void RemoveRange(IEnumerable<TEntity> list)
        {
            _dbSet.RemoveRange(list);
        }

        public async Task<IEnumerable<TEntity>> ToListAsyncAsNoTracking() => await _dbSet.AsNoTracking().ToListAsync();

        public async Task<IEnumerable<TEntity>> GetAsync(Expression<Func<TEntity, bool>> predicate) => await _dbSet.Where(predicate).ToListAsync();

        public async Task<IEnumerable<TEntity>> GetAsyncAsNoTracking(Expression<Func<TEntity, bool>> predicate) => await _dbSet.AsNoTracking().Where(predicate).ToListAsync();

        public async Task<TEntity> GetByIdAsyncAsNoTracking(TId id) => await _dbSet.AsNoTracking().FirstOrDefaultAsync(e => e.Id.Equals(id));

        public async Task<IEnumerable<TEntity>> ToListAsync() => await _dbSet.ToListAsync();

        public async Task<TEntity> GetByIdAsync(TId id) => await _dbSet.FirstOrDefaultAsync(e => e.Id.Equals(id));

    }
}
