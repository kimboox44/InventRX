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
    public class NHibernateModeleService : IModeleService
    {
        private ISession session = NHibernateConnexion.OpenSession();


        #region IModeleService Membres

        public IList<Modele> RetrieveAll()
        {
            return session.Query<Modele>().ToList();
        }

        public Modele Retrieve(RetrieveModeleArgs args)
        {
            var result = from m in session.Query<Modele>()
                         where m.IdModele == args.IdModele
                         select m;

            return result.FirstOrDefault();
        }

        #endregion
    }
}
