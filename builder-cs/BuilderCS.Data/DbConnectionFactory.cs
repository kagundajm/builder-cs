using System;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using Npgsql;

namespace BuilderCS.Data
{

    public class DbConnectionFactory : IDbConnectionFactory
    {
        private readonly string _connectionString;

        public DbConnectionFactory(string connectionString) => _connectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));

        public async Task<IDbConnection> CreateConnectionAsync()
        {
            var cnx = new NpgsqlConnection(_connectionString);
            DefaultTypeMap.MatchNamesWithUnderscores = true;
            await cnx.OpenAsync();
            return cnx;
        }
    }
}