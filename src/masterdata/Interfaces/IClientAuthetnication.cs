using System.Threading.Tasks;
using masterdata.Models;

namespace masterdata.Interfaces
{
    public interface IClientAuthentication
    {
        Task<Client> Authenticate(string username, string password);
    }
}