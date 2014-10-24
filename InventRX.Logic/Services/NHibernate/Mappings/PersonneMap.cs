using FluentNHibernate.Mapping;
using InventRX.Logic.Model.Entities;
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
            Map(x => x.NumeroCivique)
                .Column("numeroCivique")
                .CustomType<string>()
                .Access.Property()
                .Generated.Never()
                .CustomSqlType("VARCHAR");
            Map(x => x.Rue)
                .Column("rue")
                .CustomType<string>()
                .Access.Property()
                .Generated.Never()
                .CustomSqlType("VARCHAR");
            Map(x => x.Ville)
                .Column("ville")
                .CustomType<string>()
                .Access.Property()
                .Generated.Never()
                .CustomSqlType("VARCHAR");
            Map(x => x.CodePostal)
                .Column("codePostale")
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
            Map(x => x.Telephone2)
                .Column("telephone2")
                .CustomType<string>()
                .Access.Property()
                .Generated.Never()
                .CustomSqlType("VARCHAR");
            References(x => x.Province)
                .Class<Province>()
                .Access.Property()
                .LazyLoad(Laziness.False)
                .Cascade.None()
                .Columns("idProvince");
        }

        public class ClientMap : SubclassMap<Client>
        {
            public ClientMap()
            {
                Table("Clients");
                LazyLoad();
                KeyColumn("idClient");
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
                KeyColumn("idEmploye");
                Map(x => x.NomUsager)
                    .Column("nomUsager")
                    .CustomType<string>()
                    .Access.Property()
                    .Generated.Never()
                    .CustomSqlType("VARCHAR");
                Map(x => x.MotDePasse)
                    .Column("motDePasse")
                    .CustomType<string>()
                    .Access.Property()
                    .Generated.Never()
                    .CustomSqlType("VARCHAR");
                Map(x => x.NAS)
                    .Column("NAS")
                    .CustomType<string>()
                    .Access.Property()
                    .Generated.Never()
                    .CustomSqlType("VARCHAR");
                //Il manque le poste de l'employé, mais ce n'est pas pertinent pour le moment.
            }
        }
    }
}
