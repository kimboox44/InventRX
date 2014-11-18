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
    public class NHibernateTaxeService : ITaxeService
    {
        private ISession session = NHibernateConnexion.OpenSession();


        #region IProvinceService Membres

        public IList<Taxe> RetrieveAll()
        {
            return session.Query<Taxe>().ToList();
        }

        public Taxe Retrieve(RetrieveTaxeArgs args)
        {
            var result = from s in session.Query<Taxe>()
                         where s.Date == args.Date
                         select s;

            return result.FirstOrDefault();
        }

        public Taxe RetrieveNow()
        {
            DateTime now = new DateTime();
            var result = from t in session.Query<Taxe>() select t;

            result = result.Where(t => t.Date.Year >= now.Year);

            return result.FirstOrDefault();
        }

        #endregion
    }
}
