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
    public class NHibernateCommandeService : ICommandeService
    {
        private ISession session = NHibernateConnexion.OpenSession();

        
        #region ICommandeService Membres

        public IList<Commande> RetrieveAll()
        {
            return session.Query<Commande>().ToList();
        }

        public Commande Retrieve(RetrieveCommandeArgs args)
        {
            var result = from c in session.Query<Commande>()
                         where c.IdCommande == args.IdCommande
                         select c;

            return result.FirstOrDefault();
        }

        public void Update(Commande commande)
        {
            using (var transaction = session.BeginTransaction())
            {
                session.Update(commande);
                transaction.Commit();
                //try
                //{

                //    session.Update(commande);
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

        public void Insert(Commande commande)
        {
            using (var transaction = session.BeginTransaction())
            {
                session.Save(commande);
                transaction.Commit();
            }
        }

        #endregion
    }
}
