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

        #endregion
    }
}
