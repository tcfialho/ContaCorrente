using ContaCorrente.Domain.Entities;

namespace ContaCorrente.Domain.Repositories
{
    public interface ILancamentoRepository
    {
        void Incluir(Lancamento lancamento);
    }
}
