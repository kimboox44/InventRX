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
    public class NHibernateProduitService : IProduitService
    {
        private ISession session = NHibernateConnexion.OpenSession();


    #region IProduitService Membres

        public IList<Produit> RetrieveAll()
        {
            return session.Query<Produit>().ToList();
        }

        public Produit Retrieve(RetrieveProduitArgs args)
        {
            var result = from p in session.Query<Produit>()
                         where p.IdProduit == args.IdProduit
                         select p;

            return result.FirstOrDefault();
        }

        #endregion
    }
}
