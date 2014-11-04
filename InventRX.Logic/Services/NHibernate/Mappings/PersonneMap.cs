using FluentNHibernate.Mapping;
using InventRX.Logic.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventRX.Logic.Services.NHibernate.Mappings
{
    public class PersonneMap : ClassMap<Personne>
    {
        public PersonneMap()
        {
            Table("Personnes");
            LazyLoad();
            Id(x => x.IdPersonne)
                .Column("id")
                .CustomType<int?>()
                .Access.Property()
                .CustomSqlType("INTEGER")
                .Not.Nullable()
                .GeneratedBy.Identity();
            Map(x => x.Nom)
                .Column("nom")
                .CustomType<string>()
                .Access.Property()
                .Generated.Never()
                .CustomSqlType("VARCHAR");
            Map(x => x.Prenom)
                .Column("prenom")
                .CustomType<string>()
                .Access.Property()
                .Generated.Never()
                .CustomSqlType("VARCHAR");
            Map(x => x.Telephone)
                .Column("telephone")
                .CustomType<string>()
                .Access.Property()
                .Generated.Never()
                .CustomSqlType("VARCHAR");
        }

        public class ClientMap : SubclassMap<Client>
        {
            public ClientMap()
            {
                Table("Clients");
                LazyLoad();
                KeyColumn("id");
                Map(x => x.Solde)
                    .Column("solde")
                    .CustomType<double>()
                    .Access.Property()
                    .Generated.Never()
                    .CustomSqlType("VARCHAR");
            }

        }

        public class EmployeMap : SubclassMap<Employe>
        {
            public EmployeMap()
            {
                Table("Employes");
                LazyLoad();
                KeyColumn("id");
            }
        }
    }
}
