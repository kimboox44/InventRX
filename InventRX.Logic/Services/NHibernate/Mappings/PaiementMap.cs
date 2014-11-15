using FluentNHibernate.Mapping;
using InventRX.Logic.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventRX.Logic.Services.NHibernate.Mappings
{
    public class PaiementMap : ClassMap<Paiement>
    {
        public PaiementMap()
        {
            Table("Paiements");
            LazyLoad();
            Id(x => x.IdPaiement)
                .Column("id")
                .CustomType<int?>()
                .Access.Property()
                .CustomSqlType("INTEGER")
                .Not.Nullable()
                .GeneratedBy.Identity();

            Map(x => x.Montant)
            .Column("montant")
            .CustomType<double>()
            .Access.Property()
            .Generated.Never()
            .CustomSqlType("DECIMAL");

            References(v => v.MethodePaiement)
                .Class<Modele>()
                .Access.Property()
                .LazyLoad(Laziness.False)
                .Cascade.None()
                .Columns("idMethodePaiement");
        }


    }
}
