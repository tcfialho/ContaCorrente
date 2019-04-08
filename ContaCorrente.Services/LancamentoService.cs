using ContaCorrente.Domain.Entities;
using ContaCorrente.Domain.Repositories;
using ContaCorrente.Domain.Services;
using ContaCorrente.Infrastructures.Persistances;
using System.Threading.Tasks;

namespace ContaCorrente.Services
{
    public class LancamentoService : ILancamentoService
    {
        private readonly IUnitOfWork _unityOfWork;
        private readonly IContaRepository _contaRepository;
        private readonly ILancamentoRepository _lancamentoRepository;

        public LancamentoService(IUnitOfWork unityOfWork, IContaRepository contaRepository, ILancamentoRepository lancamentoRepository)
        {
            _unityOfWork = unityOfWork;
            _contaRepository = contaRepository;
            _lancamentoRepository = lancamentoRepository;
        }

        public async Task Registrar(int numeroContaOrigem, int numeroContaDestino, decimal valor)
        {
            var contaOrigem = await _contaRepository.ObterPorNumero(numeroContaOrigem);
            var contaDestino = await _contaRepository.ObterPorNumero(numeroContaDestino);

            var lancamento = new Lancamento(contaOrigem, contaDestino, valor);

            if (lancamento.IsValid())
            {
                _lancamentoRepository.Incluir(lancamento);
                await _unityOfWork.Commit();
            }
        }
    }
}
