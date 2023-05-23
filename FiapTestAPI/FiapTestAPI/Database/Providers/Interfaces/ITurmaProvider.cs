using FiapTestAPI.Models;

namespace FiapTestAPI.Database.Providers.Interfaces
{
    public interface ITurmaProvider
    {
        Task<IEnumerable<TurmaModel>> Get();
        Task<TurmaModel> Get(string turma);
        Task<TurmaModel> Get(int id);
    }
}
