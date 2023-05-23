using Dapper;
using FiapTestAPI.Database.Providers.Interfaces;
using FiapTestAPI.Models;
using Microsoft.Data.Sqlite;

namespace FiapTestAPI.Database.Providers
{
    public class TurmaProvider : ITurmaProvider
    {
        private readonly DatabaseConfig _config;

        public TurmaProvider(DatabaseConfig config)
        {
            this._config = config;
        }

        public async Task<IEnumerable<TurmaModel>> Get()
        {
            SqliteConnection connection = new SqliteConnection(this._config.ConnectionConfig);

            return await connection.QueryAsync<TurmaModel>("SELECT ID, CursoID, Turma, Ano FROM Turma;");
        }

        public async Task<TurmaModel> Get(string turma)
        {
            SqliteConnection connection = new SqliteConnection(this._config.ConnectionConfig);

            return await connection.QueryFirstOrDefaultAsync<TurmaModel>("SELECT ID, CursoID, Turma, Ano FROM Turma WHERE Turma = @Turma;", new { Turma = turma });
        }

        public async Task<TurmaModel> Get(int id)
        {
            SqliteConnection connection = new SqliteConnection(this._config.ConnectionConfig);

            return await connection.QueryFirstOrDefaultAsync<TurmaModel>("SELECT ID, CursoID, Turma, Ano FROM Turma WHERE ID = @ID;", new { ID = id });
        }
    }
}
