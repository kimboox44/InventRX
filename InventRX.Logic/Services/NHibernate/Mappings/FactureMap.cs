using FluentNHibernate.Mapping;
using InventRX.Logic.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventRX.Logic.Services.NHibernate.Mappings
{
    public class FactureMap : ClassMap<Facture>
    {
        public FactureMap()
        {
            Table("Factures");
            LazyLoad();
            Id(x => x.IdFacture)
                .Column("idFacture")
                .CustomType<int?>()
                .Access.Property()
                .CustomSqlType("INTEGER")
                .Not.Nullable()
                .GeneratedBy.Identity();
            Map(x => x.Date)
                .Column("dateFacture")
                .CustomType<DateTime>()
                .Access.Property()
                .Not.Nullable().Generated.Insert()
                .CustomSqlType("TIMESTAMP");

            References(v => v.Client)
                .Class<Client>()
                .Access.Property()
                .LazyLoad(Laziness.False)
                .Cascade.None()
                .Columns("idClient");

            References(v => v.Employe)
                .Class<Employe>()
                .Access.Property()
                .LazyLoad(Laziness.False)
                .Cascade.None()
                .Columns("idEmploye");

            References(v => v.Taxe)
                .Class<Taxe>()
                .Access.Property()
                .LazyLoad(Laziness.False)
                .Cascade.None()
                .Columns("idTaxe");

            HasMany<ItemFacture>(x => x.ItemsFacture)
                .KeyColumn("idFacture")
                .Inverse()
                .Cascade.AllDeleteOrphan();

            HasMany<Paiement>(x => x.Paiements)
                .KeyColumn("idFacture")
                .Inverse()
                .Cascade.AllDeleteOrphan();
        }
    }
}
