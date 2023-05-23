using Dapper;
using Microsoft.Data.Sqlite;

namespace FiapTestAPI.Database
{
    /// <summary>
    /// Calsse para iniciar o banco de dados com as tabelas.
    /// </summary>
    public class DatabaseBootStrap : IDatabaseBootStrap
    {
        private readonly DatabaseConfig _config;

        public DatabaseBootStrap(DatabaseConfig config)
        {
            this._config = config;
        }

        /// <summary>
        /// Faz o set up do banco com as tabelas.
        /// </summary>
        /// <returns>True se criou/ja tem as tabelas, false se deu erro.</returns>
        public bool SetUp()
        {
            try
            {
                SqliteConnection connection = new SqliteConnection(this._config.ConnectionConfig);


                //Checa se ja tem tabela.
                var table = connection.Query<string>("SELECT name FROM sqlite_master WHERE type='table' AND name = 'Aluno';");
                var tableName = table.FirstOrDefault();

                //Se sim, entao nao criar as tabelas
                if (!string.IsNullOrEmpty(tableName) && tableName == "Aluno")
                {
                    return true;
                }
                else
                {
                    //Tabela de Alunos
                    connection.Execute("CREATE TABLE Aluno (" +
                   "[ID] INTEGER NOT NULL," +
                   "[Nome] VARCHAR(255) UNIQUE NOT NULL," +
                   "[Usuario] VARCHAR(45) UNIQUE NOT NULL," +
                   "[Senha] VARCHAR(60) NOT NULL," +
                   "CONSTRAINT [PK_Aluno] PRIMARY KEY ([ID]));");

                    //Tabela de Turmas
                    connection.Execute("CREATE TABLE Turma (" +
                   "ID INTEGER NOT NULL," +
                   "CursoID INTEGER NOT NULL," +
                   "Turma VARCHAR(45) UNIQUE NOT NULL, " +
                   "Ano INTEGER NOT NULL," +
                   "CONSTRAINT [PK_Turma] PRIMARY KEY ([ID]));");

                    //Tabela de ligacao
                    connection.Execute("CREATE TABLE Aluno_Turma (" +
                   "ID INTEGER NOT NULL," +
                   "AlunoID INTEGER NOT NULL," +
                   "TurmaID INTEGER NOT NULL," +
                   "CONSTRAINT [Aluno_Turma] PRIMARY KEY ([ID]));");
                }

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
    }
}
