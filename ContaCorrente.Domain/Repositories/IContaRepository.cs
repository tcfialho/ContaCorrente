using ContaCorrente.Domain.Entities;
using System.Threading.Tasks;

namespace ContaCorrente.Domain.Repositories
{
    public interface IContaRepository
    {
        Task<Conta> ObterPorNumero(int numero);
    }
}
