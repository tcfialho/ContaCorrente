namespace ContaCorrente.Domain.Entities
{
    public class Conta
    {
        public int Id { get; set; }
        public int Numero { get; set; }

        internal Conta()
        {
        }

        public Conta(int numero) : this()
        {
            Numero = numero;
        }
    }
}