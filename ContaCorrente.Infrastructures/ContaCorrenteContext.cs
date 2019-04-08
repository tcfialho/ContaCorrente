using ContaCorrente.Domain.Entities;
using ContaCorrente.Infrastructures.Persistances;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace ContaCorrente.Infrastructures
{
    public class ContaCorrenteContext : DbContext, IDatabaseContext
    {
        public DbSet<Lancamento> Usuarios { get; set; }
        public DbSet<Conta> Contas { get; set; }

        public ContaCorrenteContext(DbContextOptions<ContaCorrenteContext> options) : base(options)
        {
        }

        public void IsTransient<TEntity>(TEntity item) where TEntity : class
        {
            Entry(item).State = EntityState.Detached;
        }

        public void MarkAsModified<TEntity>(TEntity item) where TEntity : class
        {
            Entry(item).State = EntityState.Modified;
        }

        async Task IDatabaseContext.SaveChanges()
        {
            await SaveChangesAsync();
        }
    }
}
