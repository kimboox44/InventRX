using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InventRX.Logic.Services.Definitions;
using InventRX.Logic.Models.Entities;
using InventRX.Logic.Models.Args;

namespace InventRX.Services
{
    public class SimpleClientService : IClientService
    {

        private List<Client> _clients;

        public SimpleClientService()
        {
            _clients = new List<Client>()
            {
                new Client() {IdClient = 6, Nom = "NomClient6", Prenom="PrenomClient6"},
                new Client() {IdClient = 7, Nom = "NomClient7", Prenom="PrenomClient7"},
                new Client() {IdClient = 8, Nom = "NomClient8", Prenom="PrenomClient8"},
            };
        }

        #region IClientService Membres
        public IList<Client> RetrieveAll()
        {
            return _clients;
        }

        public Client Retrieve(RetrieveClientArgs args)
        {
            return _clients.Where(c => c.IdClient == args.IdClient).First();
        }

        #endregion
    }
}
