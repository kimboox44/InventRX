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
    public class NHibernateProvinceService : IProvinceService
    {
        private ISession session = NHibernateConnexion.OpenSession();


        #region IProvinceService Membres

        public IList<Province> RetrieveAll()
        {
            return session.Query<Province>().ToList();
        }

        public Province Retrieve(RetrieveProvinceArgs args)
        {
            var result = from s in session.Query<Province>()
                         where s.IdProvince == args.IdProvince
                         select s;

            return result.FirstOrDefault();
        }
        #endregion
    }
}
