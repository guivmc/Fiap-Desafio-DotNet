using FiapTestAPI.Models;

namespace FiapTestAPI.Database.Repositories.Interfaces
{
    public interface IRelacaoRepository
    {
        Task Create(RelacaoModel relacao);
        Task Update(RelacaoModel relacao);
        Task Delete(int alunoID);
    }
}
