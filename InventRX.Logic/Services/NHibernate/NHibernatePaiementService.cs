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
    public class NHibernatePaiementService : IPaiementService
    {
        private ISession session = NHibernateConnexion.OpenSession();


        #region IPaiementService Membres

        public IList<Paiement> RetrieveAll()
        {
            return session.Query<Paiement>().ToList();
        }

        public IList<Paiement> RetrieveBy(RetrievePaiementArgs args)
        {
            return session.Query<Paiement>().ToList();
        }

        public Paiement Retrieve(RetrievePaiementArgs args)
        {
            var result = from p in session.Query<Paiement>()
                         where p.IdPaiement == args.IdPaiement
                         select p;

            return result.FirstOrDefault();
        }

        public void Update(Paiement Paiement)
        {
            using (var transaction = session.BeginTransaction())
            {
                session.Update(Paiement);
                transaction.Commit();
                //try
                //{

                //    session.Update(Paiement);
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

        public void Insert(Paiement Paiement)
        {
            using (var transaction = session.BeginTransaction())
            {
                session.Save(Paiement);
                transaction.Commit();
            }
        }

        #endregion
    }
}
