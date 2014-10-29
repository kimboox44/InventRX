using FluentNHibernate.Mapping;
using InventRX.Logic.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventRX.Logic.Services.NHibernate.Mappings
{
    public class CommandeMap : ClassMap<Commande>
    {
        public CommandeMap()
        {
            Table("Commandes");
            LazyLoad();
            Id(x => x.IdCommande)
                .Column("idCommande")
                .CustomType<int?>()
                .Access.Property()
                .CustomSqlType("INTEGER")
                .Not.Nullable()
                .GeneratedBy.Identity();
            Map(x => x.Date)
                .Column("dateCommande")
                .CustomType<DateTime>()
                .Access.Property()
                .Not.Nullable().Generated.Insert()
                .CustomSqlType("TIMESTAMP");

            References(v => v.Fournisseur)
                .Class<Fournisseur>()
                .Access.Property()
                .LazyLoad(Laziness.False)
                .Cascade.None()
                .Columns("idFournisseur");
            References(v => v.Employe)
                .Class<Employe>()
                .Access.Property()
                .LazyLoad(Laziness.False)
                .Cascade.None()
                .Columns("idEmploye");
        }
    }
}
