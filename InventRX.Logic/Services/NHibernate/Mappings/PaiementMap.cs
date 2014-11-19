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
                .Column("idPaiement")
                .CustomType<int?>()
                .Access.Property()
                .CustomSqlType("INTEGER")
                .Not.Nullable()
                .GeneratedBy.Identity();

            Map(x => x.Date)
                .Column("datePaiement")
                .CustomType<DateTime>()
                .Access.Property()
                .Not.Nullable().Generated.Insert()
                .CustomSqlType("TIMESTAMP");

            Map(x => x.Montant)
                .Column("montant")
                .CustomType<double>()
                .Access.Property()
                .Generated.Never();

            Map(x => x.MethodePaiement)
            .Column("methodePaiement")
            .CustomType<string>()
            .Access.Property()
            .Generated.Never();

            References(v => v.Client)
                .Class<Client>()
                .Access.Property()
                .LazyLoad(Laziness.False)
                .Cascade.None()
                .Columns("idClient");

            References(v => v.Facture)
                 .Class<Facture>()
                 .Access.Property()
                 .LazyLoad(Laziness.False)
                 .Cascade.None()
                 .Columns("idFacture");

            /*References(v => v.MethodePaiement)
                .Class<Modele>()
                .Access.Property()
                .LazyLoad(Laziness.False)
                .Cascade.None()
                .Columns("idMethodePaiement");*/
        }


    }
}
