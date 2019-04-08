using ContaCorrente.Infrastructures.Persistances;
using Microsoft.EntityFrameworkCore;

namespace ContaCorrente.Infrastructures.Repositories
{

    public abstract class Repository<TEntity> where TEntity : class
    {
        private readonly IDatabaseContext _context;

        protected DbSet<TEntity> Set() => _context.Set<TEntity>();

        public Repository(IDatabaseContext context)
        {
            _context = context;
        }

        protected void IsTransient(TEntity item)
        {
            _context.IsTransient(item);
        }

        protected void MarkAsModified(TEntity item)
        {
            _context.MarkAsModified(item);
        }
    }
}