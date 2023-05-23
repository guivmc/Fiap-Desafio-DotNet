using FiapTestAPI.Models;

namespace FiapTestAPI.Database.Repositories.Interfaces
{
    public interface IAlunoRepository
    {
        Task Create(AlunoModel aluno);
        Task Update(AlunoModel aluno);
        Task Delete(int id);
    }
}
