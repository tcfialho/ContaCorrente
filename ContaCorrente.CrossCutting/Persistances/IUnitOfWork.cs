using System.Threading.Tasks;

namespace ContaCorrente.Infrastructures.Persistances
{
    public interface IUnitOfWork
    {
        Task Commit();
    }
}