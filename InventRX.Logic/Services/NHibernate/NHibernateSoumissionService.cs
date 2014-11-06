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
            var result = from s in session.Query<Soumission>() select s;

            if (args.IdSoumission != 0)
            {
                //Sans les dates
                if (args.Client.Telephone != null && args.Client.Nom == null && args.Client.Prenom == null && args.DateDebut == null && args.DateFin == null)
                {
                    result = result.Where(s => args.IdSoumission == s.IdSoumission
                                        && args.Client.Telephone == s.Client.Telephone
                                        );
                }
                else if (args.Client.Telephone != null && args.Client.Nom != null && args.Client.Prenom == null && args.DateDebut == null && args.DateFin == null)
                {
                    result = result.Where(s => args.IdSoumission == s.IdSoumission
                                        && args.Client.Telephone == s.Client.Telephone
                                        && args.Client.Nom == s.Client.Nom
                                        );
                }
                else if (args.Client.Telephone != null && args.Client.Nom == null && args.Client.Prenom != null && args.DateDebut == null && args.DateFin == null)
                {
                    result = result.Where(s => args.IdSoumission == s.IdSoumission
                                        && args.Client.Telephone == s.Client.Telephone
                                        && args.Client.Prenom == s.Client.Prenom
                                        );
                }
                else if (args.Client.Telephone != null && args.Client.Nom != null && args.Client.Prenom != null && args.DateDebut == null && args.DateFin == null)
                {
                    result = result.Where(s => args.IdSoumission == s.IdSoumission
                                        && args.Client.Telephone == s.Client.Telephone
                                        && args.Client.Nom == s.Client.Nom
                                        && args.Client.Prenom == s.Client.Prenom
                                        );
                }
                else if (args.Client.Telephone == null && args.Client.Nom == null && args.Client.Prenom == null && args.DateDebut == null && args.DateFin == null)
                {
                    result = result.Where(s => args.IdSoumission == s.IdSoumission
                                        );
                }
                else if (args.Client.Telephone == null && args.Client.Nom != null && args.Client.Prenom == null && args.DateDebut == null && args.DateFin == null)
                {
                    result = result.Where(s => args.IdSoumission == s.IdSoumission
                                        && args.Client.Nom == s.Client.Nom
                                        );
                }
                else if (args.Client.Telephone == null && args.Client.Nom == null && args.Client.Prenom != null && args.DateDebut == null && args.DateFin == null)
                {
                    result = result.Where(s => args.IdSoumission == s.IdSoumission
                                       && args.Client.Prenom == s.Client.Prenom
                                       );
                }
                else if (args.Client.Telephone == null && args.Client.Nom != null && args.Client.Prenom != null && args.DateDebut == null && args.DateFin == null)
                {
                    result = result.Where(s => args.IdSoumission == s.IdSoumission
                                        && args.Client.Nom == s.Client.Nom
                                        && args.Client.Prenom == s.Client.Prenom
                                        );
                }

                //Avec datedebut
                else if (args.Client.Telephone != null && args.Client.Nom == null && args.Client.Prenom == null && args.DateDebut != null && args.DateFin == null)
                {
                    result = result.Where(s => args.IdSoumission == s.IdSoumission
                                        && args.Client.Telephone == s.Client.Telephone
                                        && args.DateDebut == s.Date.Date
                                        );
                }
                else if (args.Client.Telephone != null && args.Client.Nom != null && args.Client.Prenom == null && args.DateDebut != null && args.DateFin == null)
                {
                    result = result.Where(s => args.IdSoumission == s.IdSoumission
                                        && args.Client.Telephone == s.Client.Telephone
                                        && args.Client.Nom == s.Client.Nom
                                        && args.DateDebut == s.Date.Date
                                        );
                }
                else if (args.Client.Telephone != null && args.Client.Nom == null && args.Client.Prenom != null && args.DateDebut != null && args.DateFin == null)
                {
                    result = result.Where(s => args.IdSoumission == s.IdSoumission
                                        && args.Client.Telephone == s.Client.Telephone
                                        && args.Client.Prenom == s.Client.Prenom
                                        && args.DateDebut == s.Date.Date
                                        );
                }
                else if (args.Client.Telephone != null && args.Client.Nom != null && args.Client.Prenom != null && args.DateDebut != null && args.DateFin == null)
                {
                    result = result.Where(s => args.IdSoumission == s.IdSoumission
                                        && args.Client.Telephone == s.Client.Telephone
                                        && args.Client.Nom == s.Client.Nom
                                        && args.Client.Prenom == s.Client.Prenom
                                        && args.DateDebut == s.Date.Date
                                        );
                }
                else if (args.Client.Telephone == null && args.Client.Nom == null && args.Client.Prenom == null && args.DateDebut != null && args.DateFin == null)
                {
                    result = result.Where(s => args.IdSoumission == s.IdSoumission
                                        && args.DateDebut == s.Date.Date
                                        );
                }
                else if (args.Client.Telephone == null && args.Client.Nom != null && args.Client.Prenom == null && args.DateDebut != null && args.DateFin == null)
                {
                    result = result.Where(s => args.IdSoumission == s.IdSoumission
                                        && args.Client.Nom == s.Client.Nom
                                        && args.DateDebut == s.Date.Date
                                        );
                }
                else if (args.Client.Telephone == null && args.Client.Nom == null && args.Client.Prenom != null && args.DateDebut != null && args.DateFin == null)
                {
                    result = result.Where(s => args.IdSoumission == s.IdSoumission
                                       && args.Client.Prenom == s.Client.Prenom
                                       && args.DateDebut == s.Date.Date
                                       );
                }
                else if (args.Client.Telephone == null && args.Client.Nom != null && args.Client.Prenom != null && args.DateDebut != null && args.DateFin == null)
                {
                    result = result.Where(s => args.IdSoumission == s.IdSoumission
                                        && args.Client.Nom == s.Client.Nom
                                        && args.Client.Prenom == s.Client.Prenom
                                        && args.DateDebut == s.Date.Date
                                        );
                }

                //Avec datefin
                else if (args.Client.Telephone != null && args.Client.Nom == null && args.Client.Prenom == null && args.DateDebut == null && args.DateFin != null)
                {
                    result = result.Where(s => args.IdSoumission == s.IdSoumission
                                        && args.Client.Telephone == s.Client.Telephone
                                        && args.DateFin  == s.Date.Date
                                        );
                }
                else if (args.Client.Telephone != null && args.Client.Nom != null && args.Client.Prenom == null && args.DateDebut == null && args.DateFin != null)
                {
                    result = result.Where(s => args.IdSoumission == s.IdSoumission
                                        && args.Client.Telephone == s.Client.Telephone
                                        && args.Client.Nom == s.Client.Nom
                                        && args.DateFin  == s.Date.Date
                                        );
                }
                else if (args.Client.Telephone != null && args.Client.Nom == null && args.Client.Prenom != null && args.DateDebut == null && args.DateFin != null)
                {
                    result = result.Where(s => args.IdSoumission == s.IdSoumission
                                        && args.Client.Telephone == s.Client.Telephone
                                        && args.Client.Prenom == s.Client.Prenom
                                        && args.DateFin  == s.Date.Date
                                        );
                }
                else if (args.Client.Telephone != null && args.Client.Nom != null && args.Client.Prenom != null && args.DateDebut == null && args.DateFin != null)
                {
                    result = result.Where(s => args.IdSoumission == s.IdSoumission
                                        && args.Client.Telephone == s.Client.Telephone
                                        && args.Client.Nom == s.Client.Nom
                                        && args.Client.Prenom == s.Client.Prenom
                                        && args.DateFin  == s.Date.Date
                                        );
                }
                else if (args.Client.Telephone == null && args.Client.Nom == null && args.Client.Prenom == null && args.DateDebut == null && args.DateFin != null)
                {
                    result = result.Where(s => args.IdSoumission == s.IdSoumission
                                        && args.DateFin  == s.Date.Date
                                        );
                }
                else if (args.Client.Telephone == null && args.Client.Nom != null && args.Client.Prenom == null && args.DateDebut == null && args.DateFin != null)
                {
                    result = result.Where(s => args.IdSoumission == s.IdSoumission
                                        && args.Client.Nom == s.Client.Nom
                                        && args.DateFin  == s.Date.Date
                                        );
                }
                else if (args.Client.Telephone == null && args.Client.Nom == null && args.Client.Prenom != null && args.DateDebut == null && args.DateFin != null)
                {
                    result = result.Where(s => args.IdSoumission == s.IdSoumission
                                       && args.Client.Prenom == s.Client.Prenom
                                       && args.DateFin  == s.Date.Date
                                       );
                }
                else if (args.Client.Telephone == null && args.Client.Nom != null && args.Client.Prenom != null && args.DateDebut == null && args.DateFin != null)
                {
                    result = result.Where(s => args.IdSoumission == s.IdSoumission
                                        && args.Client.Nom == s.Client.Nom
                                        && args.Client.Prenom == s.Client.Prenom
                                        && args.DateFin  == s.Date.Date
                                        );
                }

                //Avec datedebut et datefin
                else if (args.Client.Telephone != null && args.Client.Nom == null && args.Client.Prenom == null && args.DateDebut != null && args.DateFin != null)
                {
                    result = result.Where(s => args.IdSoumission == s.IdSoumission
                                        && args.Client.Telephone == s.Client.Telephone
                                        && (s.Date.Date >= args.DateDebut && s.Date.Date <= args.DateFin)
                                        );
                }
                else if (args.Client.Telephone != null && args.Client.Nom != null && args.Client.Prenom == null && args.DateDebut != null && args.DateFin != null)
                {
                    result = result.Where(s => args.IdSoumission == s.IdSoumission
                                        && args.Client.Telephone == s.Client.Telephone
                                        && args.Client.Nom == s.Client.Nom
                                        && (s.Date.Date >= args.DateDebut && s.Date.Date <= args.DateFin)
                                        );
                }
                else if (args.Client.Telephone != null && args.Client.Nom == null && args.Client.Prenom != null && args.DateDebut != null && args.DateFin != null)
                {
                    result = result.Where(s => args.IdSoumission == s.IdSoumission
                                        && args.Client.Telephone == s.Client.Telephone
                                        && args.Client.Prenom == s.Client.Prenom
                                        && (s.Date.Date >= args.DateDebut && s.Date.Date <= args.DateFin)
                                        );
                }
                else if (args.Client.Telephone != null && args.Client.Nom != null && args.Client.Prenom != null && args.DateDebut != null && args.DateFin != null)
                {
                    result = result.Where(s => args.IdSoumission == s.IdSoumission
                                        && args.Client.Telephone == s.Client.Telephone
                                        && args.Client.Nom == s.Client.Nom
                                        && args.Client.Prenom == s.Client.Prenom
                                        && (s.Date.Date >= args.DateDebut && s.Date.Date <= args.DateFin)
                                        );
                }
                else if (args.Client.Telephone == null && args.Client.Nom == null && args.Client.Prenom == null && args.DateDebut != null && args.DateFin != null)
                {
                    result = result.Where(s => args.IdSoumission == s.IdSoumission
                                        && (s.Date.Date >= args.DateDebut && s.Date.Date <= args.DateFin)
                                        );
                }
                else if (args.Client.Telephone == null && args.Client.Nom != null && args.Client.Prenom == null && args.DateDebut != null && args.DateFin != null)
                {
                    result = result.Where(s => args.IdSoumission == s.IdSoumission
                                        && args.Client.Nom == s.Client.Nom
                                        && (s.Date.Date >= args.DateDebut && s.Date.Date <= args.DateFin)
                                        );
                }
                else if (args.Client.Telephone == null && args.Client.Nom == null && args.Client.Prenom != null && args.DateDebut != null && args.DateFin != null)
                {
                    result = result.Where(s => args.IdSoumission == s.IdSoumission
                                       && args.Client.Prenom == s.Client.Prenom
                                       && (s.Date.Date >= args.DateDebut && s.Date.Date <= args.DateFin)
                                       );
                }
                else if (args.Client.Telephone == null && args.Client.Nom != null && args.Client.Prenom != null && args.DateDebut != null && args.DateFin != null)
                {
                    result = result.Where(s => args.IdSoumission == s.IdSoumission
                                        && args.Client.Nom == s.Client.Nom
                                        && args.Client.Prenom == s.Client.Prenom
                                        && (s.Date.Date >= args.DateDebut && s.Date.Date <= args.DateFin)
                                        );
                }

               // fonctionnel sans date
                /*else if (args.Client.Telephone != null && args.Client.Nom == null && args.Client.Prenom == null)
                {
                    result = result.Where(s => args.IdSoumission == s.IdSoumission
                                        && args.Client.Telephone == s.Client.Telephone
                                        );
                }
                else if (args.Client.Telephone != null && args.Client.Nom != null && args.Client.Prenom == null)
                {
                    result = result.Where(s => args.IdSoumission == s.IdSoumission
                                        && args.Client.Telephone == s.Client.Telephone
                                        && args.Client.Nom == s.Client.Nom
                                        );
                }
                else if (args.Client.Telephone != null && args.Client.Nom == null && args.Client.Prenom != null)
                {
                    result = result.Where(s => args.IdSoumission == s.IdSoumission
                                        && args.Client.Telephone == s.Client.Telephone
                                        && args.Client.Prenom == s.Client.Prenom
                                        );
                }
                else if (args.Client.Telephone != null && args.Client.Nom != null && args.Client.Prenom != null)
                {
                    result = result.Where(s => args.IdSoumission == s.IdSoumission
                                        && args.Client.Telephone == s.Client.Telephone
                                        && args.Client.Nom == s.Client.Nom
                                        && args.Client.Prenom == s.Client.Prenom
                                        );
                }
                else if (args.Client.Telephone == null && args.Client.Nom == null && args.Client.Prenom == null)
                {
                    result = result.Where(s => args.IdSoumission == s.IdSoumission
                                        );
                }
                else if (args.Client.Telephone == null && args.Client.Nom != null && args.Client.Prenom == null)
                {
                    result = result.Where(s => args.IdSoumission == s.IdSoumission
                                        && args.Client.Nom == s.Client.Nom
                                        );
                }
                else if (args.Client.Telephone == null && args.Client.Nom == null && args.Client.Prenom != null)
                {
                    result = result.Where(s => args.IdSoumission == s.IdSoumission
                                       && args.Client.Prenom == s.Client.Prenom
                                       );
                }
                else if (args.Client.Telephone == null && args.Client.Nom != null && args.Client.Prenom != null)
                {
                    result = result.Where(s => args.IdSoumission == s.IdSoumission
                                        && args.Client.Nom == s.Client.Nom
                                        && args.Client.Prenom == s.Client.Prenom
                                        );
                }*/
            }

            else if (args.IdSoumission == 0)
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
                    return new List<Soumission>();
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

           /* if (args.IdSoumission != 0 && args.Client.Telephone != null && args.Client.Nom != null && args.Client.Prenom != null)
            {
                result = result.Where(s => args.IdSoumission == s.IdSoumission 
                                        && args.Client.Telephone == s.Client.Telephone
                                        && args.Client.Nom == s.Client.Nom
                                        && args.Client.Prenom == s.Client.Prenom
                                        );
            }
            else if (args.IdSoumission != 0 && args.Client.Telephone != null && args.Client.Nom != null)
            {
                result = result.Where(s => args.IdSoumission == s.IdSoumission 
                                        && args.Client.Telephone == s.Client.Telephone
                                        && args.Client.Nom == s.Client.Nom
                                        );
            }
            else if (args.IdSoumission != 0 && args.Client.Telephone != null && args.Client.Prenom != null)
            {
                result = result.Where(s => args.IdSoumission == s.IdSoumission
                                        && args.Client.Telephone == s.Client.Telephone
                                        && args.Client.Prenom == s.Client.Prenom
                                        );
            }
            else if (args.IdSoumission != 0 && args.Client.Telephone != null)
            {
                result = result.Where(s => args.IdSoumission == s.IdSoumission && args.Client.Telephone == s.Client.Telephone);
            }
            else if (args.IdSoumission != 0 && args.Client.Telephone == null)
            {
                result = result.Where(s => args.IdSoumission == s.IdSoumission);
            }
            else if (args.IdSoumission != 0 && args.Client.Telephone != null && args.Client.Nom != null && args.Client.Prenom != null)
            {
                result = result.Where(s => args.IdSoumission == s.IdSoumission 
                                        && args.Client.Telephone == s.Client.Telephone
                                        && args.Client.Nom == s.Client.Nom
                                        && args.Client.Prenom == s.Client.Prenom
                                        );
            }
            else if (args.IdSoumission != 0 && args.Client.Telephone != null && args.Client.Nom != null)
            {
                result = result.Where(s => args.IdSoumission == s.IdSoumission 
                                        && args.Client.Telephone == s.Client.Telephone
                                        && args.Client.Nom == s.Client.Nom
                                        );
            }
            else if (args.IdSoumission != 0 && args.Client.Telephone != null && args.Client.Prenom != null)
            {
                result = result.Where(s => args.IdSoumission == s.IdSoumission
                                        && args.Client.Telephone == s.Client.Telephone
                                        && args.Client.Prenom == s.Client.Prenom
                                        );
            }
            else if (args.IdSoumission != 0 && args.Client.Telephone != null)
            {
                result = result.Where(s => args.IdSoumission == s.IdSoumission && args.Client.Telephone == s.Client.Telephone);
            }
            //Sans un téléphone de défini


            //Si IdSoumission n'est pas défini
            //Et un téléphone de défini
            else if (args.IdSoumission == 0 && args.Client.Telephone != null && args.Client.Nom != null && args.Client.Prenom != null)
            {
                result = result.Where(s => args.IdSoumission == s.IdSoumission
                                        && args.Client.Telephone == s.Client.Telephone
                                        && args.Client.Nom == s.Client.Nom
                                        && args.Client.Prenom == s.Client.Prenom
                                        );
            }
            else if (args.IdSoumission == 0 && args.Client.Telephone != null && args.Client.Nom != null)
            {
                result = result.Where(s => args.Client.Telephone == s.Client.Telephone
                                        && args.Client.Nom == s.Client.Nom
                                        );
            }
            else if (args.IdSoumission == 0 && args.Client.Telephone != null && args.Client.Prenom != null)
            {
                result = result.Where(s => args.Client.Telephone == s.Client.Telephone
                                        && args.Client.Prenom == s.Client.Prenom
                                        );
            }
            else if (args.IdSoumission == 0 && args.Client.Telephone != null)
            {
                result = result.Where(s => args.Client.Telephone == s.Client.Telephone);
            }
            //Sans un téléphone de défini
            else if (args.IdSoumission == 0 && args.Client.Telephone == null && args.Client.Nom != null && args.Client.Prenom != null)
            {
                result = result.Where(s => args.IdSoumission == s.IdSoumission
                                        && args.Client.Telephone == s.Client.Telephone
                                        && args.Client.Nom == s.Client.Nom
                                        && args.Client.Prenom == s.Client.Prenom
                                        );
            }
            else if (args.IdSoumission == 0 && args.Client.Telephone != null && args.Client.Nom != null)
            {
                result = result.Where(s => args.Client.Telephone == s.Client.Telephone
                                        && args.Client.Nom == s.Client.Nom
                                        );
            }
            else if (args.IdSoumission == 0 && args.Client.Telephone != null && args.Client.Prenom != null)
            {
                result = result.Where(s => args.Client.Telephone == s.Client.Telephone
                                        && args.Client.Prenom == s.Client.Prenom
                                        );
            }
            else if (args.IdSoumission == 0 && args.Client.Telephone != null)
            {
                result = result.Where(s => args.Client.Telephone == s.Client.Telephone);
            }
            */
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
