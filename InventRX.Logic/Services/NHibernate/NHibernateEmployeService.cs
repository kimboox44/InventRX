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
    public class NHibernateEmployeService : IEmployeService
    {
        private ISession session = NHibernateConnexion.OpenSession();


        #region IEmployeService Membres

        public IList<Employe> RetrieveAll()
        {
            return session.Query<Employe>().ToList();
        }

        public Employe Retrieve(RetrieveEmployeArgs args)
        {
            try
            {
                var result = from e in session.Query<Employe>()
                            where e.IdEmploye == args.IdEmploye
                            select e;

                return result.FirstOrDefault();
            }
            catch (Exception)
            {
                return session.Query<Employe>().ToList().FirstOrDefault();
            }
            
        }

        public void Insert(Employe employe)
        {
            using (var transaction = session.BeginTransaction())
            {
                session.Save(employe);
                transaction.Commit();
            }
        }

        #endregion
    }
}
