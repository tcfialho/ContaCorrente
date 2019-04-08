using ContaCorrente.Domain.Entities;
using ContaCorrente.Domain.Repositories;
using ContaCorrente.Infrastructures.Persistances;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace ContaCorrente.Infrastructures.Repositories
{
    public class ContaRepository : Repository<Conta>, IContaRepository
    {
        public ContaRepository(IDatabaseContext context) : base(context)
        {

        }

        public async Task<Conta> ObterPorNumero(int numero)
        {
            return await Set().FirstOrDefaultAsync(x => x.Numero == numero);
        }
    }
}
