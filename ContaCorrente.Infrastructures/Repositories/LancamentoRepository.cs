using ContaCorrente.Domain.Entities;
using ContaCorrente.Domain.Repositories;
using ContaCorrente.Infrastructures.Persistances;

namespace ContaCorrente.Infrastructures.Repositories
{
    public class LancamentoRepository : Repository<Lancamento>, ILancamentoRepository
    {
        public LancamentoRepository(IDatabaseContext context) : base(context)
        {
        }

        public void Incluir(Lancamento lancamento)
        {
            Set().AddAsync(lancamento);
        }
    }
}
