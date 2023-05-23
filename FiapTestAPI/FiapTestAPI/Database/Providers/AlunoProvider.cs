using Dapper;
using FiapTestAPI.Database.Providers.Interfaces;
using FiapTestAPI.Models;
using Microsoft.Data.Sqlite;

namespace FiapTestAPI.Database.Providers
{
    public class AlunoProvider : IAlunoProvider
    {
        private readonly DatabaseConfig _config;

        public AlunoProvider(DatabaseConfig config)
        {
            this._config = config;
        }

        public async Task<IEnumerable<AlunoModel>> Get()
        {
            SqliteConnection connection = new SqliteConnection(this._config.ConnectionConfig);

            return await connection.QueryAsync<AlunoModel>("SELECT ID, Nome, Usuario, Senha FROM Aluno;");
        }

        public async Task<AlunoModel> Get(string usuario)
        {
            SqliteConnection connection = new SqliteConnection(this._config.ConnectionConfig);

            return await connection.QueryFirstOrDefaultAsync<AlunoModel>("SELECT ID, Nome, Usuario FROM Aluno WHERE Usuario = @Usuario;", new { Usuario = usuario });
        }

        public async Task<AlunoModel> Get(int id)
        {
            SqliteConnection connection = new SqliteConnection(this._config.ConnectionConfig);

            return await connection.QueryFirstOrDefaultAsync<AlunoModel>("SELECT ID, Nome, Usuario FROM Aluno WHERE ID = @ID;", new { ID = id });
        }
    }
}
