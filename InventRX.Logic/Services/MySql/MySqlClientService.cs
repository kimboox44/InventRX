using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InventRX.Logic.Models.Args;
using InventRX.Logic.Models.Entities;
using InventRX.Logic.Services.Helpers;
using InventRX.Logic.Services.Definitions;
using System.Data;
using MySql.Data.MySqlClient;


namespace InventRX.Logic.Services.MySql
{
    public class MySqlClientService : IClientService
    {
        private MySqlConnexion connexion;
        public IList<Client> RetrieveAll()
        {
            IList<Client> result = new List<Client>();
            try
            {
                connexion = new MySqlConnexion();

                string requete = "SELECT * FROM Clients INNER JOIN Personnes ON Clients.idClient = Personnes.idPersonne";

                DataSet dataset = connexion.Query(requete);
                DataTable table = dataset.Tables[0];

                foreach (DataRow client in table.Rows)
                {
                    result.Add(ConstructClient(client));
                }

            }
            catch (MySqlException)
            {
                throw;
            }

            return result;
        }

        public Client Retrieve(RetrieveClientArgs args)
        {
            try
            {
                connexion = new MySqlConnexion();

                string requete = String.Format("SELECT * FROM Clients INNER JOIN Personnes ON Clients.idClient = Personnes.idPersonne WHERE idClient = {0}", args.IdClient);
                DataSet dataset = connexion.Query(requete);

                return ConstructClient(dataset.Tables[0].Rows[0]);

            }
            catch (MySqlException)
            {
                throw;
            }
        }

        private Client ConstructClient(DataRow row)
        {

            return new Client()
            {
                IdClient = (int)row["idClient"],
                Nom = (string)row["nom"],
                Prenom = (string)row["prenom"]
            };

        }
    }
}
