using ContaCorrente.Domain.Entities;
using ContaCorrente.Infrastructures;
using System;
using System.Collections.Generic;
using System.Text;

namespace ContaCorrente.Tests
{
    public static class Seed
    {
        public static void InitializeDbForTests(ContaCorrenteContext db)
        {
            db.Contas.AddRange(GetContas());
            db.SaveChanges();
        }

        public static IEnumerable<Conta> GetContas()
        {
            return new List<Conta>()
            {
                new Conta(1111),
                new Conta(2222),
            };
        }
    }
}
