using Dapper;
using FiapTestAPI.Database.Providers.Interfaces;
using FiapTestAPI.Models;
using Microsoft.Data.Sqlite;

namespace FiapTestAPI.Database.Providers
{
    public class RelacaoProvider : IRelacaoProvider
    {
        private readonly DatabaseConfig _config;

        public RelacaoProvider(DatabaseConfig config)
        {
            this._config = config;
        }

        public async Task<IEnumerable<RelacaoModel>> Get()
        {
            SqliteConnection connection = new SqliteConnection(this._config.ConnectionConfig);

            return await connection.QueryAsync<RelacaoModel>("SELECT  AlunoID, A.Nome AS AlunoNome, TurmaID, T.Turma AS TurmaNome FROM Aluno_Turma " +
                "INNER JOIN Aluno A ON A.ID = AlunoID " +
                "INNER JOIN Turma T ON T.ID = TurmaID ");
        }

        public async Task<RelacaoModel> GetPorAluno(int alunoID)
        {
            SqliteConnection connection = new SqliteConnection(this._config.ConnectionConfig);

            return await connection.QueryFirstOrDefaultAsync<RelacaoModel>("SELECT  AlunoID, A.Nome, TurmaID, T.Turma FROM Aluno_Turma " +
                "INNER JOIN Aluno A ON A.ID = AlunoID " +
                "INNER JOIN Turma T ON T.ID = TurmaID " +
                "WHERE AlunoID = @ID;", new { ID = alunoID });
        }

        public async Task<RelacaoModel> GetPorAlunoETurma(int alunoID, int turmaID)
        {
            SqliteConnection connection = new SqliteConnection(this._config.ConnectionConfig);

            return await connection.QueryFirstOrDefaultAsync<RelacaoModel>("SELECT TA.AlunoID, A.Nome, TA.TurmaID, T.Turma FROM Aluno_Turma AS TA " +
                "INNER JOIN Aluno AS A ON A.ID = TA.AlunoID " +
                "INNER JOIN Turma AS T ON T.ID = TA.TurmaID " +
                "WHERE TA.AlunoID = @AlunoID AND TA.TurmaID = @TurmaID;", new { AlunoID = alunoID, TurmaID = turmaID});
        }

        public async Task<RelacaoModel> GetPorTurma(int turmaID)
        {
            SqliteConnection connection = new SqliteConnection(this._config.ConnectionConfig);

            return await connection.QueryFirstOrDefaultAsync<RelacaoModel>("SELECT AlunoID, A.Nome, TurmaID, T.Turma FROM Aluno_Turma " +
                "INNER JOIN Aluno A ON A.ID = AlunoID " +
                "INNER JOIN Turma T ON T.ID = TurmaID " +
                "WHERE TurmaID = @ID;", new { ID = turmaID });
        }
    }
}
