using System.Threading.Tasks;

namespace ContaCorrente.Infrastructures.Persistances
{
    public class UnitOfWork : IUnitOfWork
    {
        public IDatabaseContext Context { get; set; }

        public UnitOfWork(IDatabaseContext context)
        {
            Context = context;
        }

        public async Task Commit()
        {
            await Context.SaveChanges();
        }
    }
}
