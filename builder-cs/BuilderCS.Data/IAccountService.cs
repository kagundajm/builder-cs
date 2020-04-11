using System.Collections.Generic;
using System.Threading.Tasks;
using BuilderCS.Core;

namespace BuilderCS.Data
{
    public interface IAccountService
    {
        Task<IEnumerable<Account>> GetAccountsAsync();
    }
}