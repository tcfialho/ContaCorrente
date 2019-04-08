using System.ComponentModel.DataAnnotations;

namespace ContaCorrente.Domain.Commands
{
    public class LancamentoCommand
    {
        [Required]
        public int ContaOrigem { get; set; }
        [Required]
        public int ContaDestino { get; set; }
        [Required]
        public decimal Valor { get; set; }
    }
}