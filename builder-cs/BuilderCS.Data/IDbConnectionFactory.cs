using System.Data;
using System.Threading.Tasks;

namespace BuilderCS.Data
{
    public interface IDbConnectionFactory
    {
        Task<IDbConnection> CreateConnectionAsync();
    }
}