using Dapper;
using FiapTestAPI.Database.Repositories.Interfaces;
using FiapTestAPI.Models;
using Microsoft.Data.Sqlite;

namespace FiapTestAPI.Database.Repositories
{
    public class TurmaRepository : ITurmaRepository
    {
        private readonly DatabaseConfig _config;

        public TurmaRepository(DatabaseConfig config)
        {
            this._config = config;
        }

        public async Task Create(TurmaModel aluno)
        {
            SqliteConnection connection = new SqliteConnection(this._config.ConnectionConfig);

            await connection.ExecuteAsync("INSERT INTO Turma (CursoID, Turma, Ano)" +
                "VALUES (@CursoID, @Turma, @Ano);", aluno);
        }

        public async Task Delete(int id)
        {
            SqliteConnection connection = new SqliteConnection(this._config.ConnectionConfig);

            await connection.ExecuteAsync("DELETE FROM Turma WHERE ID = @ID;", new { ID = id });
            await connection.ExecuteAsync("DELETE FROM Aluno_Turma WHERE TurmaID = @ID;", new { ID = id });
        }

        public async Task Update(TurmaModel turma)
        {
            SqliteConnection connection = new SqliteConnection(this._config.ConnectionConfig);

            await connection.ExecuteAsync("UPDATE Turma SET CursoID = @CursoID, Turma = @Turma, Ano = @Ano WHERE ID = @ID;", turma);
        }
    }
}
