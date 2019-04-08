using ContaCorrente.CrossCutting.Notifications;

namespace ContaCorrente.Domain.Entities
{
    public class Lancamento
    {
        private readonly Notifications notifications = Notifications.Instance;

        internal Lancamento()
        {
        }

        public Lancamento(Conta contaOrigem, Conta contaDestino, decimal valor) : this()
        {            
            ContaOrigem = contaOrigem;
            ContaDestino = contaDestino;
            Valor = valor;

            Validate();
        }

        private void Validate()
        {
            if (ContaOrigem == null)
                notifications.AddNotification("O campo conta origem é obrigatório.");

            if (ContaDestino == null)
                notifications.AddNotification("O campo conta destino é obrigatório.");

            if (Valor < 0)
                notifications.AddNotification("Valor deve ser superior a zero");

            if (ContaOrigem.Numero == ContaDestino.Numero)
                notifications.AddNotification("Não é possivel transferir entre contas iguais.");
        }

        public bool IsValid()
        {
            return !notifications.HasNotifications();
        }

        public int Id { get; protected set; }
        public virtual Conta ContaOrigem { get; protected set; }
        public virtual Conta ContaDestino { get; protected set; }
        public decimal Valor { get; protected set; }
    }
}