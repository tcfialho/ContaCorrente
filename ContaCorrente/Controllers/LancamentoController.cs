using System.Net;
using System.Threading.Tasks;
using ContaCorrente.Domain.Commands;
using ContaCorrente.Domain.Services;
using Microsoft.AspNetCore.Mvc;

namespace ContaCorrente.Controllers
{
    [Route("api/[controller]")]
    public class LancamentoController : ControllerBase
    {
        private readonly ILancamentoService _lancamentoService;

        public LancamentoController(ILancamentoService lancamentoService)
        {
            _lancamentoService = lancamentoService;
        }

        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> Post([FromBody] LancamentoCommand command)
            => await ApiResponse(_lancamentoService.Registrar(command.ContaOrigem, command.ContaDestino, command.Valor));
    }
}
