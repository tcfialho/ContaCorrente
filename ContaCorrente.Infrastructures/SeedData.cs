using ContaCorrente.Domain.Entities;
using ContaCorrente.Infrastructures;
using System.Linq;

namespace ContaCorrente
{
    public static class SeedData
    {
        public static void Initialize(ContaCorrenteContext context)
        {
            if (!context.Contas.Any())
            {
                context.Contas.Add(new Conta(1111));
                context.Contas.Add(new Conta(2222));

                context.SaveChanges();
            }
        }
    }
}
