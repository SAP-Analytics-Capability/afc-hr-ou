using System.Threading.Tasks;

using bwsync.Models;

namespace bwsync.Interfaces
{
    public interface IClientAuthentication
    {
        Task<Client> Authenticate(string username, string password);
    }
}