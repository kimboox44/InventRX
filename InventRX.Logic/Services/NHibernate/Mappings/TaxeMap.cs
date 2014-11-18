using FluentNHibernate.Mapping;
using InventRX.Logic.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventRX.Logic.Services.NHibernate.Mappings
{
    public class TaxeMap : ClassMap<Taxe>
    {
        public TaxeMap()
        {
            Table("Taxes");
            LazyLoad();
            Id(x => x.IdTaxe)
                .Column("idTaxe")
                .CustomType<int?>()
                .Access.Property()
                .CustomSqlType("INTEGER")
                .Not.Nullable()
                .GeneratedBy.Identity();
            Map(x => x.TPS)
                .Column("TPS")
                .CustomType<double>()
                .Access.Property()
                .Not.Nullable().Generated.Insert()
                .CustomSqlType("DECIMAL");
            Map(x => x.TVQ)
                .Column("TVQ")
                .CustomType<double>()
                .Access.Property()
                .Not.Nullable().Generated.Insert()
                .CustomSqlType("DECIMAL");
            Map(x => x.Date)
                .Column("dateTaxe")
                .CustomType<DateTime>()
                .Access.Property()
                .Not.Nullable().Generated.Insert()
                .CustomSqlType("TIMESTAMP");
        }
    }
}
