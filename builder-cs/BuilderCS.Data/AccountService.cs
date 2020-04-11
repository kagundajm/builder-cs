using System.Collections.Generic;
using System.Threading.Tasks;
using BuilderCS.Core;
using Dapper;

namespace BuilderCS.Data
{
    public class AccountService : IAccountService
    {
        private IDbConnectionFactory _connectionFactory;

        public AccountService(IDbConnectionFactory dbCnxFactory) => _connectionFactory = dbCnxFactory;

        public async Task<IEnumerable<Account>> GetAccountsAsync()
        {
            using (var cnx = await _connectionFactory.CreateConnectionAsync())
            {
                var qry = @"SELECT * FROM accounts WHERE is_hidden = false;";
                var accts = await cnx.QueryAsync<Account>(qry);
                return accts;
            }
        }
    }
}