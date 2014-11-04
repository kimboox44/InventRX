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

        public IList<Soumission> RetrieveBy(RetrieveSoumissionArgs args)
        {
            /*var result = from s in session.Query<Soumission>() select;

            if(args.IdSoumission != null || args.IdSoumission != 0)
            {
                result.Where(s.)
                where s.IdSoumission == args.IdSoumission;
            }
            
                         where
                             //(s.IdSoumission != null || s.IdSoumission != 0 ? args.IdSoumission : args.IdSoumission) 
                               s.IdSoumission == args.IdSoumission
                            || s.Client.Nom.Contains == args.Client.Nom
                            || s.Client.Prenom == args.Client.Prenom
                            || s.Client.Telephone == args.Client.Telephone
                         select s;
            return result.ToList();
            */
            var result = from s in session.Query<Soumission>()
                         where 
                            //(s.IdSoumission != null || s.IdSoumission != 0 ? args.IdSoumission : args.IdSoumission) 
                               s.IdSoumission == args.IdSoumission 
                            || s.Client.Nom == args.Client.Nom
                            || s.Client.Prenom == args.Client.Prenom
                            || s.Client.Telephone == args.Client.Telephone 
                         select s;
           return result.ToList();
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

        public void Insert(Soumission soumission)
        {
            using (var transaction = session.BeginTransaction())
            {
                session.Save(soumission);
                transaction.Commit();
            }
        }

        #endregion
    }
}
