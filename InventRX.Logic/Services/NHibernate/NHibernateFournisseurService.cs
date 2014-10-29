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
    public class NHibernateFournisseurService : IFournisseurService
    {
        private ISession session = NHibernateConnexion.OpenSession();


        #region IFournisseurService Membres

        public IList<Fournisseur> RetrieveAll()
        {
            return session.Query<Fournisseur>().ToList();
        }

        public Fournisseur Retrieve(RetrieveFournisseurArgs args)
        {
            var result = from c in session.Query<Fournisseur>()
                         where c.IdFournisseur == args.IdFournisseur
                         select c;

            return result.FirstOrDefault();
        }

        public Fournisseur RetrieveByPhone(RetrieveFournisseurArgs args)
        {
            var result = from c in session.Query<Fournisseur>()
                         where c.Telephone == args.Telephone
                         select c;

            return result.FirstOrDefault();
        }

        public void Update(Fournisseur fournisseur)
        {
            using (var transaction = session.BeginTransaction())
            {
                session.Update(fournisseur);
                transaction.Commit();
            }
        }

        public void Insert(Fournisseur fournisseur)
        {
            using (var transaction = session.BeginTransaction())
            {
                session.Save(fournisseur);
                transaction.Commit();
            }
        }

        #endregion
    }
}
