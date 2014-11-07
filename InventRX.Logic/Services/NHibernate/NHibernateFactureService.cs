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
            var result = from s in session.Query<Facture>() select s;

            if (args.IdFacture != 0)
            {
                //Sans les dates
                if (args.Client.Telephone != null && args.Client.Nom == null && args.Client.Prenom == null && args.DateDebut == null && args.DateFin == null)
                {
                    result = result.Where(s => args.IdFacture == s.IdFacture
                                        && args.Client.Telephone == s.Client.Telephone
                                        );
                }
                else if (args.Client.Telephone != null && args.Client.Nom != null && args.Client.Prenom == null && args.DateDebut == null && args.DateFin == null)
                {
                    result = result.Where(s => args.IdFacture == s.IdFacture
                                        && args.Client.Telephone == s.Client.Telephone
                                        && args.Client.Nom == s.Client.Nom
                                        );
                }
                else if (args.Client.Telephone != null && args.Client.Nom == null && args.Client.Prenom != null && args.DateDebut == null && args.DateFin == null)
                {
                    result = result.Where(s => args.IdFacture == s.IdFacture
                                        && args.Client.Telephone == s.Client.Telephone
                                        && args.Client.Prenom == s.Client.Prenom
                                        );
                }
                else if (args.Client.Telephone != null && args.Client.Nom != null && args.Client.Prenom != null && args.DateDebut == null && args.DateFin == null)
                {
                    result = result.Where(s => args.IdFacture == s.IdFacture
                                        && args.Client.Telephone == s.Client.Telephone
                                        && args.Client.Nom == s.Client.Nom
                                        && args.Client.Prenom == s.Client.Prenom
                                        );
                }
                else if (args.Client.Telephone == null && args.Client.Nom == null && args.Client.Prenom == null && args.DateDebut == null && args.DateFin == null)
                {
                    result = result.Where(s => args.IdFacture == s.IdFacture
                                        );
                }
                else if (args.Client.Telephone == null && args.Client.Nom != null && args.Client.Prenom == null && args.DateDebut == null && args.DateFin == null)
                {
                    result = result.Where(s => args.IdFacture == s.IdFacture
                                        && args.Client.Nom == s.Client.Nom
                                        );
                }
                else if (args.Client.Telephone == null && args.Client.Nom == null && args.Client.Prenom != null && args.DateDebut == null && args.DateFin == null)
                {
                    result = result.Where(s => args.IdFacture == s.IdFacture
                                       && args.Client.Prenom == s.Client.Prenom
                                       );
                }
                else if (args.Client.Telephone == null && args.Client.Nom != null && args.Client.Prenom != null && args.DateDebut == null && args.DateFin == null)
                {
                    result = result.Where(s => args.IdFacture == s.IdFacture
                                        && args.Client.Nom == s.Client.Nom
                                        && args.Client.Prenom == s.Client.Prenom
                                        );
                }

                //Avec datedebut
                else if (args.Client.Telephone != null && args.Client.Nom == null && args.Client.Prenom == null && args.DateDebut != null && args.DateFin == null)
                {
                    result = result.Where(s => args.IdFacture == s.IdFacture
                                        && args.Client.Telephone == s.Client.Telephone
                                        && args.DateDebut == s.Date.Date
                                        );
                }
                else if (args.Client.Telephone != null && args.Client.Nom != null && args.Client.Prenom == null && args.DateDebut != null && args.DateFin == null)
                {
                    result = result.Where(s => args.IdFacture == s.IdFacture
                                        && args.Client.Telephone == s.Client.Telephone
                                        && args.Client.Nom == s.Client.Nom
                                        && args.DateDebut == s.Date.Date
                                        );
                }
                else if (args.Client.Telephone != null && args.Client.Nom == null && args.Client.Prenom != null && args.DateDebut != null && args.DateFin == null)
                {
                    result = result.Where(s => args.IdFacture == s.IdFacture
                                        && args.Client.Telephone == s.Client.Telephone
                                        && args.Client.Prenom == s.Client.Prenom
                                        && args.DateDebut == s.Date.Date
                                        );
                }
                else if (args.Client.Telephone != null && args.Client.Nom != null && args.Client.Prenom != null && args.DateDebut != null && args.DateFin == null)
                {
                    result = result.Where(s => args.IdFacture == s.IdFacture
                                        && args.Client.Telephone == s.Client.Telephone
                                        && args.Client.Nom == s.Client.Nom
                                        && args.Client.Prenom == s.Client.Prenom
                                        && args.DateDebut == s.Date.Date
                                        );
                }
                else if (args.Client.Telephone == null && args.Client.Nom == null && args.Client.Prenom == null && args.DateDebut != null && args.DateFin == null)
                {
                    result = result.Where(s => args.IdFacture == s.IdFacture
                                        && args.DateDebut == s.Date.Date
                                        );
                }
                else if (args.Client.Telephone == null && args.Client.Nom != null && args.Client.Prenom == null && args.DateDebut != null && args.DateFin == null)
                {
                    result = result.Where(s => args.IdFacture == s.IdFacture
                                        && args.Client.Nom == s.Client.Nom
                                        && args.DateDebut == s.Date.Date
                                        );
                }
                else if (args.Client.Telephone == null && args.Client.Nom == null && args.Client.Prenom != null && args.DateDebut != null && args.DateFin == null)
                {
                    result = result.Where(s => args.IdFacture == s.IdFacture
                                       && args.Client.Prenom == s.Client.Prenom
                                       && args.DateDebut == s.Date.Date
                                       );
                }
                else if (args.Client.Telephone == null && args.Client.Nom != null && args.Client.Prenom != null && args.DateDebut != null && args.DateFin == null)
                {
                    result = result.Where(s => args.IdFacture == s.IdFacture
                                        && args.Client.Nom == s.Client.Nom
                                        && args.Client.Prenom == s.Client.Prenom
                                        && args.DateDebut == s.Date.Date
                                        );
                }

                //Avec datefin
                else if (args.Client.Telephone != null && args.Client.Nom == null && args.Client.Prenom == null && args.DateDebut == null && args.DateFin != null)
                {
                    result = result.Where(s => args.IdFacture == s.IdFacture
                                        && args.Client.Telephone == s.Client.Telephone
                                        && args.DateFin  == s.Date.Date
                                        );
                }
                else if (args.Client.Telephone != null && args.Client.Nom != null && args.Client.Prenom == null && args.DateDebut == null && args.DateFin != null)
                {
                    result = result.Where(s => args.IdFacture == s.IdFacture
                                        && args.Client.Telephone == s.Client.Telephone
                                        && args.Client.Nom == s.Client.Nom
                                        && args.DateFin  == s.Date.Date
                                        );
                }
                else if (args.Client.Telephone != null && args.Client.Nom == null && args.Client.Prenom != null && args.DateDebut == null && args.DateFin != null)
                {
                    result = result.Where(s => args.IdFacture == s.IdFacture
                                        && args.Client.Telephone == s.Client.Telephone
                                        && args.Client.Prenom == s.Client.Prenom
                                        && args.DateFin  == s.Date.Date
                                        );
                }
                else if (args.Client.Telephone != null && args.Client.Nom != null && args.Client.Prenom != null && args.DateDebut == null && args.DateFin != null)
                {
                    result = result.Where(s => args.IdFacture == s.IdFacture
                                        && args.Client.Telephone == s.Client.Telephone
                                        && args.Client.Nom == s.Client.Nom
                                        && args.Client.Prenom == s.Client.Prenom
                                        && args.DateFin  == s.Date.Date
                                        );
                }
                else if (args.Client.Telephone == null && args.Client.Nom == null && args.Client.Prenom == null && args.DateDebut == null && args.DateFin != null)
                {
                    result = result.Where(s => args.IdFacture == s.IdFacture
                                        && args.DateFin  == s.Date.Date
                                        );
                }
                else if (args.Client.Telephone == null && args.Client.Nom != null && args.Client.Prenom == null && args.DateDebut == null && args.DateFin != null)
                {
                    result = result.Where(s => args.IdFacture == s.IdFacture
                                        && args.Client.Nom == s.Client.Nom
                                        && args.DateFin  == s.Date.Date
                                        );
                }
                else if (args.Client.Telephone == null && args.Client.Nom == null && args.Client.Prenom != null && args.DateDebut == null && args.DateFin != null)
                {
                    result = result.Where(s => args.IdFacture == s.IdFacture
                                       && args.Client.Prenom == s.Client.Prenom
                                       && args.DateFin  == s.Date.Date
                                       );
                }
                else if (args.Client.Telephone == null && args.Client.Nom != null && args.Client.Prenom != null && args.DateDebut == null && args.DateFin != null)
                {
                    result = result.Where(s => args.IdFacture == s.IdFacture
                                        && args.Client.Nom == s.Client.Nom
                                        && args.Client.Prenom == s.Client.Prenom
                                        && args.DateFin  == s.Date.Date
                                        );
                }

                //Avec datedebut et datefin
                else if (args.Client.Telephone != null && args.Client.Nom == null && args.Client.Prenom == null && args.DateDebut != null && args.DateFin != null)
                {
                    result = result.Where(s => args.IdFacture == s.IdFacture
                                        && args.Client.Telephone == s.Client.Telephone
                                        && (s.Date.Date >= args.DateDebut && s.Date.Date <= args.DateFin)
                                        );
                }
                else if (args.Client.Telephone != null && args.Client.Nom != null && args.Client.Prenom == null && args.DateDebut != null && args.DateFin != null)
                {
                    result = result.Where(s => args.IdFacture == s.IdFacture
                                        && args.Client.Telephone == s.Client.Telephone
                                        && args.Client.Nom == s.Client.Nom
                                        && (s.Date.Date >= args.DateDebut && s.Date.Date <= args.DateFin)
                                        );
                }
                else if (args.Client.Telephone != null && args.Client.Nom == null && args.Client.Prenom != null && args.DateDebut != null && args.DateFin != null)
                {
                    result = result.Where(s => args.IdFacture == s.IdFacture
                                        && args.Client.Telephone == s.Client.Telephone
                                        && args.Client.Prenom == s.Client.Prenom
                                        && (s.Date.Date >= args.DateDebut && s.Date.Date <= args.DateFin)
                                        );
                }
                else if (args.Client.Telephone != null && args.Client.Nom != null && args.Client.Prenom != null && args.DateDebut != null && args.DateFin != null)
                {
                    result = result.Where(s => args.IdFacture == s.IdFacture
                                        && args.Client.Telephone == s.Client.Telephone
                                        && args.Client.Nom == s.Client.Nom
                                        && args.Client.Prenom == s.Client.Prenom
                                        && (s.Date.Date >= args.DateDebut && s.Date.Date <= args.DateFin)
                                        );
                }
                else if (args.Client.Telephone == null && args.Client.Nom == null && args.Client.Prenom == null && args.DateDebut != null && args.DateFin != null)
                {
                    result = result.Where(s => args.IdFacture == s.IdFacture
                                        && (s.Date.Date >= args.DateDebut && s.Date.Date <= args.DateFin)
                                        );
                }
                else if (args.Client.Telephone == null && args.Client.Nom != null && args.Client.Prenom == null && args.DateDebut != null && args.DateFin != null)
                {
                    result = result.Where(s => args.IdFacture == s.IdFacture
                                        && args.Client.Nom == s.Client.Nom
                                        && (s.Date.Date >= args.DateDebut && s.Date.Date <= args.DateFin)
                                        );
                }
                else if (args.Client.Telephone == null && args.Client.Nom == null && args.Client.Prenom != null && args.DateDebut != null && args.DateFin != null)
                {
                    result = result.Where(s => args.IdFacture == s.IdFacture
                                       && args.Client.Prenom == s.Client.Prenom
                                       && (s.Date.Date >= args.DateDebut && s.Date.Date <= args.DateFin)
                                       );
                }
                else if (args.Client.Telephone == null && args.Client.Nom != null && args.Client.Prenom != null && args.DateDebut != null && args.DateFin != null)
                {
                    result = result.Where(s => args.IdFacture == s.IdFacture
                                        && args.Client.Nom == s.Client.Nom
                                        && args.Client.Prenom == s.Client.Prenom
                                        && (s.Date.Date >= args.DateDebut && s.Date.Date <= args.DateFin)
                                        );
                }

            }

            else if (args.IdFacture == 0)
            {
                //Sans les dates
                if (args.Client.Telephone != null && args.Client.Nom == null && args.Client.Prenom == null && args.DateDebut == null && args.DateFin == null)
                {
                    result = result.Where(s => args.Client.Telephone == s.Client.Telephone
                                        );
                }
                else if (args.Client.Telephone != null && args.Client.Nom != null && args.Client.Prenom == null && args.DateDebut == null && args.DateFin == null)
                {
                    result = result.Where(s => args.Client.Telephone == s.Client.Telephone
                                        && args.Client.Nom == s.Client.Nom
                                        );
                }
                else if (args.Client.Telephone != null && args.Client.Nom == null && args.Client.Prenom != null && args.DateDebut == null && args.DateFin == null)
                {
                    result = result.Where(s => args.Client.Telephone == s.Client.Telephone
                                        && args.Client.Prenom == s.Client.Prenom
                                        );
                }
                else if (args.Client.Telephone != null && args.Client.Nom != null && args.Client.Prenom != null && args.DateDebut == null && args.DateFin == null)
                {
                    result = result.Where(s => args.Client.Telephone == s.Client.Telephone
                                        && args.Client.Nom == s.Client.Nom
                                        && args.Client.Prenom == s.Client.Prenom
                                        );
                }
                else if (args.Client.Telephone == null && args.Client.Nom == null && args.Client.Prenom == null && args.DateDebut == null && args.DateFin == null)
                {
                    return new List<Facture>();
                }
                else if (args.Client.Telephone == null && args.Client.Nom != null && args.Client.Prenom == null && args.DateDebut == null && args.DateFin == null)
                {
                    result = result.Where(s => args.Client.Nom == s.Client.Nom
                                        );
                }
                else if (args.Client.Telephone == null && args.Client.Nom == null && args.Client.Prenom != null && args.DateDebut == null && args.DateFin == null)
                {
                    result = result.Where(s => args.Client.Prenom == s.Client.Prenom
                                       );
                }
                else if (args.Client.Telephone == null && args.Client.Nom != null && args.Client.Prenom != null && args.DateDebut == null && args.DateFin == null)
                {
                    result = result.Where(s => args.Client.Nom == s.Client.Nom
                                        && args.Client.Prenom == s.Client.Prenom
                                        );
                }

                //Avec datedebut
                else if (args.Client.Telephone != null && args.Client.Nom == null && args.Client.Prenom == null && args.DateDebut != null && args.DateFin == null)
                {
                    result = result.Where(s => args.Client.Telephone == s.Client.Telephone
                                        && args.DateDebut == s.Date.Date
                                        );
                }
                else if (args.Client.Telephone != null && args.Client.Nom != null && args.Client.Prenom == null && args.DateDebut != null && args.DateFin == null)
                {
                    result = result.Where(s => args.Client.Telephone == s.Client.Telephone
                                        && args.Client.Nom == s.Client.Nom
                                        && args.DateDebut == s.Date.Date
                                        );
                }
                else if (args.Client.Telephone != null && args.Client.Nom == null && args.Client.Prenom != null && args.DateDebut != null && args.DateFin == null)
                {
                    result = result.Where(s => args.Client.Telephone == s.Client.Telephone
                                        && args.Client.Prenom == s.Client.Prenom
                                        && args.DateDebut == s.Date.Date
                                        );
                }
                else if (args.Client.Telephone != null && args.Client.Nom != null && args.Client.Prenom != null && args.DateDebut != null && args.DateFin == null)
                {
                    result = result.Where(s => args.Client.Telephone == s.Client.Telephone
                                        && args.Client.Nom == s.Client.Nom
                                        && args.Client.Prenom == s.Client.Prenom
                                        && args.DateDebut == s.Date.Date
                                        );
                }
                else if (args.Client.Telephone == null && args.Client.Nom == null && args.Client.Prenom == null && args.DateDebut != null && args.DateFin == null)
                {
                    result = result.Where(s => args.DateDebut == s.Date.Date
                                        );
                }
                else if (args.Client.Telephone == null && args.Client.Nom != null && args.Client.Prenom == null && args.DateDebut != null && args.DateFin == null)
                {
                    result = result.Where(s => args.Client.Nom == s.Client.Nom
                                        && args.DateDebut == s.Date.Date
                                        );
                }
                else if (args.Client.Telephone == null && args.Client.Nom == null && args.Client.Prenom != null && args.DateDebut != null && args.DateFin == null)
                {
                    result = result.Where(s => args.Client.Prenom == s.Client.Prenom
                                       && args.DateDebut == s.Date.Date
                                       );
                }
                else if (args.Client.Telephone == null && args.Client.Nom != null && args.Client.Prenom != null && args.DateDebut != null && args.DateFin == null)
                {
                    result = result.Where(s => args.Client.Nom == s.Client.Nom
                                        && args.Client.Prenom == s.Client.Prenom
                                        && args.DateDebut == s.Date.Date
                                        );
                }

                //Avec datefin
                else if (args.Client.Telephone != null && args.Client.Nom == null && args.Client.Prenom == null && args.DateDebut == null && args.DateFin != null)
                {
                    result = result.Where(s => args.Client.Telephone == s.Client.Telephone
                                        && args.DateFin == s.Date.Date
                                        );
                }
                else if (args.Client.Telephone != null && args.Client.Nom != null && args.Client.Prenom == null && args.DateDebut == null && args.DateFin != null)
                {
                    result = result.Where(s => args.Client.Telephone == s.Client.Telephone
                                        && args.Client.Nom == s.Client.Nom
                                        && args.DateFin == s.Date.Date
                                        );
                }
                else if (args.Client.Telephone != null && args.Client.Nom == null && args.Client.Prenom != null && args.DateDebut == null && args.DateFin != null)
                {
                    result = result.Where(s => args.Client.Telephone == s.Client.Telephone
                                        && args.Client.Prenom == s.Client.Prenom
                                        && args.DateFin == s.Date.Date
                                        );
                }
                else if (args.Client.Telephone != null && args.Client.Nom != null && args.Client.Prenom != null && args.DateDebut == null && args.DateFin != null)
                {
                    result = result.Where(s => args.Client.Telephone == s.Client.Telephone
                                        && args.Client.Nom == s.Client.Nom
                                        && args.Client.Prenom == s.Client.Prenom
                                        && args.DateFin == s.Date.Date
                                        );
                }
                else if (args.Client.Telephone == null && args.Client.Nom == null && args.Client.Prenom == null && args.DateDebut == null && args.DateFin != null)
                {
                    result = result.Where(s => args.DateFin == s.Date.Date
                                        );
                }
                else if (args.Client.Telephone == null && args.Client.Nom != null && args.Client.Prenom == null && args.DateDebut == null && args.DateFin != null)
                {
                    result = result.Where(s => args.Client.Nom == s.Client.Nom
                                        && args.DateFin == s.Date.Date
                                        );
                }
                else if (args.Client.Telephone == null && args.Client.Nom == null && args.Client.Prenom != null && args.DateDebut == null && args.DateFin != null)
                {
                    result = result.Where(s => args.Client.Prenom == s.Client.Prenom
                                       && args.DateFin == s.Date.Date
                                       );
                }
                else if (args.Client.Telephone == null && args.Client.Nom != null && args.Client.Prenom != null && args.DateDebut == null && args.DateFin != null)
                {
                    result = result.Where(s => args.Client.Nom == s.Client.Nom
                                        && args.Client.Prenom == s.Client.Prenom
                                        && args.DateFin == s.Date.Date
                                        );
                }

                //Avec datedebut et datefin
                else if (args.Client.Telephone != null && args.Client.Nom == null && args.Client.Prenom == null && args.DateDebut != null && args.DateFin != null)
                {
                    result = result.Where(s => args.Client.Telephone == s.Client.Telephone
                                        && (s.Date.Date >= args.DateDebut && s.Date.Date <= args.DateFin)
                                        );
                }
                else if (args.Client.Telephone != null && args.Client.Nom != null && args.Client.Prenom == null && args.DateDebut != null && args.DateFin != null)
                {
                    result = result.Where(s => args.Client.Telephone == s.Client.Telephone
                                        && args.Client.Nom == s.Client.Nom
                                        && (s.Date.Date >= args.DateDebut && s.Date.Date <= args.DateFin)
                                        );
                }
                else if (args.Client.Telephone != null && args.Client.Nom == null && args.Client.Prenom != null && args.DateDebut != null && args.DateFin != null)
                {
                    result = result.Where(s => args.Client.Telephone == s.Client.Telephone
                                        && args.Client.Prenom == s.Client.Prenom
                                        && (s.Date.Date >= args.DateDebut && s.Date.Date <= args.DateFin)
                                        );
                }
                else if (args.Client.Telephone != null && args.Client.Nom != null && args.Client.Prenom != null && args.DateDebut != null && args.DateFin != null)
                {
                    result = result.Where(s => args.Client.Telephone == s.Client.Telephone
                                        && args.Client.Nom == s.Client.Nom
                                        && args.Client.Prenom == s.Client.Prenom
                                        && (s.Date.Date >= args.DateDebut && s.Date.Date <= args.DateFin)
                                        );
                }
                else if (args.Client.Telephone == null && args.Client.Nom == null && args.Client.Prenom == null && args.DateDebut != null && args.DateFin != null)
                {
                    result = result.Where(s => (s.Date.Date >= args.DateDebut && s.Date.Date <= args.DateFin));
                }
                else if (args.Client.Telephone == null && args.Client.Nom != null && args.Client.Prenom == null && args.DateDebut != null && args.DateFin != null)
                {
                    result = result.Where(s => args.Client.Nom == s.Client.Nom
                                         && (s.Date.Date >= args.DateDebut && s.Date.Date <= args.DateFin)
                                        );
                }
                else if (args.Client.Telephone == null && args.Client.Nom == null && args.Client.Prenom != null && args.DateDebut != null && args.DateFin != null)
                {
                    result = result.Where(s => args.Client.Prenom == s.Client.Prenom
                                       && (s.Date.Date >= args.DateDebut && s.Date.Date <= args.DateFin)
                                       );
                }
                else if (args.Client.Telephone == null && args.Client.Nom != null && args.Client.Prenom != null && args.DateDebut != null && args.DateFin != null)
                {
                    result = result.Where(s => args.Client.Nom == s.Client.Nom
                                        && args.Client.Prenom == s.Client.Prenom
                                        && (s.Date.Date >= args.DateDebut && s.Date.Date <= args.DateFin)
                                        );
                }
            }
            return result.ToList();
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
