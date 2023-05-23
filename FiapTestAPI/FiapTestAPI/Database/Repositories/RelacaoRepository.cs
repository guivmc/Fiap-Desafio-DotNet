using Dapper;
using FiapTestAPI.Database.Repositories.Interfaces;
using FiapTestAPI.Models;
using Microsoft.Data.Sqlite;

namespace FiapTestAPI.Database.Repositories
{
    public class RelacaoRepository : IRelacaoRepository
    {
        private readonly DatabaseConfig _config;

        public RelacaoRepository(DatabaseConfig config)
        {
            this._config = config;
        }

        public async Task Create(RelacaoModel relacao)
        {
            SqliteConnection connection = new SqliteConnection(this._config.ConnectionConfig);

            await connection.ExecuteAsync("INSERT INTO Aluno_Turma (AlunoID, TurmaID)" +
                "VALUES (@AlunoID, @TurmaID);", relacao);
        }

        public async Task Delete(int alunoID)
        {
            SqliteConnection connection = new SqliteConnection(this._config.ConnectionConfig);

            await connection.ExecuteAsync("DELETE FROM Aluno_Turma WHERE AlunoID = @ID;", new { ID = alunoID });
        }

        public async Task Update(RelacaoModel relacao)
        {
            SqliteConnection connection = new SqliteConnection(this._config.ConnectionConfig);

            await connection.ExecuteAsync("UPDATE Aluno_Turma SET TurmaID = @TurmaID, AlunoID = @AlunoID WHERE AlunoID = @AlunoID;", relacao);
        }
    }
}
