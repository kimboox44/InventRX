using FluentNHibernate.Mapping;
using InventRX.Logic.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventRX.Logic.Services.NHibernate.Mappings
{
    public class FournisseurMap : ClassMap<Fournisseur>
    {
        public FournisseurMap()
        {
            Table("Fournisseurs");
            LazyLoad();
            Id(x => x.IdFournisseur)
                .Column("idFournisseur")
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
                .Column("codePostal")
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
            References(x => x.Province)
                .Class<Province>()
                .Access.Property()
                .LazyLoad(Laziness.False)
                .Cascade.None()
                .Columns("idProvince");
        }
    }
}
