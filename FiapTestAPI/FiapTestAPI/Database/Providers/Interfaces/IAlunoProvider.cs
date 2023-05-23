using FiapTestAPI.Models;

namespace FiapTestAPI.Database.Providers.Interfaces
{
    public interface IAlunoProvider
    {

        Task<IEnumerable<AlunoModel>> Get();
        Task<AlunoModel> Get(int id);
        Task<AlunoModel> Get(string usuario);
    }
}
