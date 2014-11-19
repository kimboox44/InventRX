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
    public class NHibernateItemFactureService : IItemFactureService
    {
        private ISession session = NHibernateConnexion.OpenSession();


        #region IItemFactureService Membres

        public IList<ItemFacture> RetrieveAll()
        {
            return session.Query<ItemFacture>().ToList();
        }

        public IList<ItemFacture> RetrieveAllBy(RetrieveItemFactureArgs args)
        {
            var result = from f in session.Query<ItemFacture>()
                         where f.Facture.IdFacture == args.Facture.IdFacture
                         select f;

            return result.ToList();
        }

        public IList<ItemFacture> Retrieve(RetrieveItemFactureArgs args)
        {
            var result = from f in session.Query<ItemFacture>()
                         where f.IdItemFacture == args.IdItemFacture
                         select f;

            return session.Query<ItemFacture>().ToList();
        }

        #endregion
    }
}
