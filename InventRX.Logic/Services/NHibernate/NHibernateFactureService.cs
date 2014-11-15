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
    public class NHibernateFactureService : IFactureService
    {
        private ISession session = NHibernateConnexion.OpenSession();


        #region IFactureService Membres

        public IList<Facture> RetrieveAll()
        {
            return session.Query<Facture>().ToList();
        }

        public IList<Facture> RetrieveBy(RetrieveFactureArgs args)
        {
            return session.Query<Facture>().ToList();
        }

        public Facture Retrieve(RetrieveFactureArgs args)
        {
            var result = from s in session.Query<Facture>()
                         where s.IdFacture == args.IdFacture
                         select s;

            return result.FirstOrDefault();
        }

        public void Update(Facture Facture)
        {
            using (var transaction = session.BeginTransaction())
            {
                session.Update(Facture);
                transaction.Commit();
                //try
                //{

                //    session.Update(Facture);
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

        public void Insert(Facture Facture)
        {
            using (var transaction = session.BeginTransaction())
            {
                session.Save(Facture);
                transaction.Commit();
            }
        }

        #endregion
    }
}
