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
    public class NHibernateSoumissionService : ISoumissionService
    {
        private ISession session = NHibernateConnexion.OpenSession();


        #region ISoumissionService Membres

        public IList<Soumission> RetrieveAll()
        {
            return session.Query<Soumission>().ToList();
        }

        public Soumission Retrieve(RetrieveSoumissionArgs args)
        {
            var result = from s in session.Query<Soumission>()
                         where s.IdSoumission == args.IdSoumission
                         select s;

            return result.FirstOrDefault();
        }

        #endregion
    }
}
