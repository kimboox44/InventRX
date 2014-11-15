using FluentNHibernate.Mapping;
using InventRX.Logic.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventRX.Logic.Services.NHibernate.Mappings
{
    public class ProduitMap : ClassMap<Produit>
    {
        public ProduitMap()
        {
            Table("Produits");
            LazyLoad();
            Id(x => x.IdProduit)
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

                Map(x => x.Description)
                .Column("description")
                .CustomType<string>()
                .Access.Property()
                .Generated.Never()
                .CustomSqlType("VARCHAR");

                Map(x => x.DescriptionDimension)
                .Column("descriptionDimension")
                .CustomType<string>()
                .Access.Property()
                .Generated.Never()
                .CustomSqlType("VARCHAR");


            ////PetiteImage
            ////GrandeImage

                Map(x => x.Prix)
                .Column("prix")
                .CustomType<double>()
                .Access.Property()
                .Generated.Never()
                .CustomSqlType("DECIMAL");

                Map(x => x.Quantite)
                .Column("quantite")
                .CustomType<int>()
                .Access.Property()
                .Generated.Never()
                .CustomSqlType("INTEGER");

                References(v => v.Modele)
                    .Class<Modele>()
                    .Access.Property()
                    .LazyLoad(Laziness.False)
                    .Cascade.None()
                    //.NotFound.Ignore()
                    .Columns("idModele");

                References(v => v.CategorieProduit)
                    .Class<CategorieProduit>()
                    .Access.Property()
                    .LazyLoad(Laziness.False)
                    .Cascade.None()
                    //.NotFound.Ignore()
                    .Columns("idCategorieProduit");
        }


    }
}
