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

        public void Update(Soumission soumission)
        {
            using (var transaction = session.BeginTransaction())
            {
                session.Update(soumission);
                transaction.Commit();
                //try
                //{

                //    session.Update(soumission);
                //    transaction.Commit();
                //}
                //catch (Exception e)
                //{
                //    transaction.Rollback();

                //}
                //finally {
                //    transaction.Dispose();
                //}
            }
        }

        #endregion
    }
}
