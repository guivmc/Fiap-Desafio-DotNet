using FiapTestAPI.Models;

namespace FiapTestAPI.Database.Repositories.Interfaces
{
    public interface ITurmaRepository
    {
        Task Create(TurmaModel turma);
        Task Update(TurmaModel turma);
        Task Delete(int id);
    }
}
