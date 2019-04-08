using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace ContaCorrente.Infrastructures.Persistances
{
    public interface IDatabaseContext
    {
        DbSet<TEntity> Set<TEntity>() where TEntity : class;
        void IsTransient<TEntity>(TEntity item) where TEntity : class;
        void MarkAsModified<TEntity>(TEntity item) where TEntity : class;
        Task SaveChanges();
    }
}