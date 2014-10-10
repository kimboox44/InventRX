using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InventRX.Logic.Model.Entities;

namespace InventRX.Logic.Services.NHibernate.Mappings
{
    class ItemSoumissionMap : ClassMap<ItemSoumission>
    {
        public ItemSoumissionMap()
        {
            Table("ItemsSoumission");
            LazyLoad();
            Id(x => x.IdItemSoumission)
                .Column("id")
                .CustomType<int?>()
                .Access.Property()
                .CustomSqlType("INTEGER")
                .Not.Nullable()
                .GeneratedBy.Identity();

            Map(x => x.NomProduit)
                .Column("nomProduit")
                .CustomType<string>()
                .Access.Property()
                .Generated.Never()
                .CustomSqlType("VARCHAR");

            Map(x => x.Quantite)
              .Column("quantite")
              .CustomType<int>()
              .Access.Property()
              .Generated.Never()
              .CustomSqlType("INTEGER");

            Map(x => x.PrixUnitaire)
               .Column("prixUnitaire")
               .CustomType<double>()
               .Access.Property()
               .Generated.Never()
               .CustomSqlType("DECIMAL");

          /* References(v => v.Produit)
                .Class<Produit>()
                .Access.Property()
                .LazyLoad(Laziness.False)
                .Cascade.None()
                .Columns("id");
           * */

            References(v => v.Soumission)
                .Class<Soumission>()
                .Access.Property()
                .LazyLoad(Laziness.False)
                .Cascade.None()
                .Columns("id");
        }

    }
}
