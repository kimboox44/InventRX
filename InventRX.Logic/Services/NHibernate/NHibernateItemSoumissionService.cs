using InventRX.Logic.Models.Args;
using InventRX.Logic.Models.Entities;
using InventRX.Logic.Services.Helpers;
using InventRX.Logic.Services.Definitions;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate.Linq;
using System.Threading.Tasks;


namespace InventRX.Logic.Services.NHibernate
{
    public class NHibernateItemSoumissionService : IItemSoumissionService
    {
        private ISession session = NHibernateConnexion.OpenSession();


        #region IItemSoumissionService Membres

        public IList<ItemSoumission> RetrieveAll()
        {
            return session.Query<ItemSoumission>().ToList();
        }

        public IList<ItemSoumission> RetrieveAllBy(RetrieveItemSoumissionArgs args)
        {
            var result = from s in session.Query<ItemSoumission>()
                         where s.Soumission.IdSoumission == args.Soumission.IdSoumission
                         select s;

            return result.ToList();
        }

        public IList<ItemSoumission> Retrieve(RetrieveItemSoumissionArgs args)
        {
            var result = from s in session.Query<ItemSoumission>()
                         where s.IdItemSoumission == args.IdItemSoumission
                         select s;

            return session.Query<ItemSoumission>().ToList();
        }

        #endregion
    }
}
