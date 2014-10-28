using InventRX.Logic.Model.Args;
using InventRX.Logic.Model.Entities;
using InventRX.Logic.Services.Helpers;
using InventRX.Services.Definitions;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate.Linq;
using System.Threading.Tasks;

namespace InventRX.Logic.Services.NHibernate
{
    public class NHibernateClientService : IClientService
    {
        private ISession session = NHibernateConnexion.OpenSession();


        #region IClientService Membres

        public IList<Client> RetrieveAll()
        {
            return session.Query<Client>().ToList();
        }

        public Client Retrieve(RetrieveClientArgs args)
        {
            var result = from c in session.Query<Client>()
                         where c.IdClient == args.IdClient
                         select c;

            return result.FirstOrDefault();
        }

        public Client RetrieveByPhone(RetrieveClientArgs args)
        {
            var result = from c in session.Query<Client>()
                         where c.Telephone == args.Telephone
                         select c;

            return result.FirstOrDefault();
        }

        public void Update(Client client)
        {
            using (var transaction = session.BeginTransaction())
            {
                session.Update(client);
                transaction.Commit();
            }
        }

        public void Insert(Client client)
        {
            using (var transaction = session.BeginTransaction())
            {
                session.Save(client);
                transaction.Commit();
            }
        }

        #endregion
    }
}
