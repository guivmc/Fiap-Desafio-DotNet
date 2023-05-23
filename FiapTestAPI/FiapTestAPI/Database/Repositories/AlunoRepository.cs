using Dapper;
using FiapTestAPI.Database.Repositories.Interfaces;
using FiapTestAPI.Models;
using Microsoft.Data.Sqlite;

namespace FiapTestAPI.Database.Repositories
{
    public class AlunoRepository : IAlunoRepository
    {
        private readonly DatabaseConfig _config;

        public AlunoRepository(DatabaseConfig config)
        {
            this._config = config;
        }

        public async Task Create(AlunoModel aluno)
        {
            SqliteConnection connection = new SqliteConnection(this._config.ConnectionConfig);

            await connection.ExecuteAsync("INSERT INTO Aluno (Nome, Usuario, Senha)" +
                "VALUES (@Nome, @Usuario, @Senha);", aluno);
        }

        public async Task Delete(int id)
        {
            SqliteConnection connection = new SqliteConnection(this._config.ConnectionConfig);

            await connection.ExecuteAsync("DELETE FROM Aluno WHERE ID = @ID;", new { ID = id });
            await connection.ExecuteAsync("DELETE FROM Aluno_Turma WHERE AlunoID = @ID;", new { ID = id });
        }

        public async Task Update(AlunoModel aluno)
        {
            SqliteConnection connection = new SqliteConnection(this._config.ConnectionConfig);

            await connection.ExecuteAsync("UPDATE Aluno SET Nome = @Nome, Usuario = @Usuario, Senha = @Senha WHERE ID = @ID;", aluno);
        }
    }
}
