using System.Threading.Tasks;
using hrsync.Models;

namespace hrsync.Interface
{
    public interface IClientAuthentication
    {
        Task<Client> Authenticate(string username, string password);
    }
}