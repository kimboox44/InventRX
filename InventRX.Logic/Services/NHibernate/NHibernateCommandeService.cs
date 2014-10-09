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
using InventRX.Logic.Services.Definitions;

namespace InventRX.Logic.Services.NHibernate
{
    public class NHibernateCommandeService : ICommandeService
    {
        private ISession session = NHibernateConnexion.OpenSession();

        
        #region ICommandeService Membres

        public IList<Commande> RetrieveAll()
        {
            return session.Query<Commande>().ToList();
        }

        public Commande Retrieve(RetrieveCommandeArgs args)
        {
            var result = from c in session.Query<Commande>()
                         where c.IdCommande == args.IdCommande
                         select c;

            return result.FirstOrDefault();
        }

        #endregion
    }
}
