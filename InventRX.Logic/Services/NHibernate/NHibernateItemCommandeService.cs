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
    public class NHibernateItemCommandeService : IItemCommandeService
    {
        private ISession session = NHibernateConnexion.OpenSession();


        #region IItemCommandeService Membres

        public IList<ItemCommande> RetrieveAll()
        {
            return session.Query<ItemCommande>().ToList();
        }

        public IList<ItemCommande> RetrieveAllBy(RetrieveItemCommandeArgs args)
        {
            var result = from c in session.Query<ItemCommande>()
                         where c.Commande.IdCommande == args.Commande.IdCommande
                         select c;

            return result.ToList();
        }

        public IList<ItemCommande> Retrieve(RetrieveItemCommandeArgs args)
        {
            var result = from s in session.Query<ItemCommande>()
                         where s.IdItemCommande == args.IdItemCommande
                         select s;

            return session.Query<ItemCommande>().ToList();
        }

        #endregion
    }
}
