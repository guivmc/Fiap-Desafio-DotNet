using FiapTestAPI.Models;

namespace FiapTestAPI.Database.Providers.Interfaces
{
    public interface IRelacaoProvider
    {
        Task<IEnumerable<RelacaoModel>> Get();
        Task<RelacaoModel> GetPorAluno(int alunoID);
        Task<RelacaoModel> GetPorTurma(int turmaID);
        Task<RelacaoModel> GetPorAlunoETurma(int alunoID, int turmaID);
    }
}
