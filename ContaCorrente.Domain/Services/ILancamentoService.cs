using System.Threading.Tasks;

namespace ContaCorrente.Domain.Services
{
    public interface ILancamentoService
    {
        Task Registrar(int numeroContaOrigem, int numeroContaDestino, decimal valor);
    }
}