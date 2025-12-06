using System.Threading.Tasks;
using rulesmngt.Models;

namespace rulesmngt.Interfaces
{
    public interface IClientAuthentication
    {
        Task<Client> Authenticate(string username, string password);
    }
}