using FluentNHibernate.Cfg;
using NHibernate;
using NHibernate.Cfg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace InventRX.Logic.Services.Helpers
{
    public class NHibernateConnexion
    {
        private static ISessionFactory _sessionFactory;
        private static readonly bool IS_FLUENT = true;

        static NHibernateConnexion()
        {

            if (IS_FLUENT)
            {

                _sessionFactory = Fluently.Configure().Mappings(m => m.FluentMappings.AddFromAssemblyOf<NHibernateConnexion>()).BuildSessionFactory();
            }
            else
            {
                _sessionFactory = new Configuration().AddAssembly(typeof(NHibernateConnexion).Assembly).BuildSessionFactory();
            }

        }

        private static ISessionFactory SessionFactory
        {
            get
            {
                return _sessionFactory;
            }
        }

        public static ISession OpenSession()
        {
            return SessionFactory.OpenSession();

        }
    }
}
