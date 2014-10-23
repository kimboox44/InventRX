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
    public class NHibernateCategorieProduitService : ICategorieProduitService
    {
        private ISession session = NHibernateConnexion.OpenSession();


        #region ICategorieProduitService Membres

        public IList<CategorieProduit> RetrieveAll()
        {
            return session.Query<CategorieProduit>().ToList();
        }

        public CategorieProduit Retrieve(RetrieveCategorieProduitArgs args)
        {
            var result = from cp in session.Query<CategorieProduit>()
                         where cp.IdCategorieProduit == args.IdCategorieProduit
                         select cp;

            return result.FirstOrDefault();
        }

        #endregion
    }
}
