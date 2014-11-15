using FluentNHibernate.Mapping;
using InventRX.Logic.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventRX.Logic.Services.NHibernate.Mappings
{
    public class MethodePaiementMap : ClassMap<MethodePaiement>
    {
        public MethodePaiementMap()
        {
            Table("MethodesPaiement");
            LazyLoad();
            Id(x => x.IdMethodePaiement)
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
        }
    }
}
