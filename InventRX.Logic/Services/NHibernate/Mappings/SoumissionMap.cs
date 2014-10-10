using FluentNHibernate.Mapping;
using InventRX.Logic.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventRX.Logic.Services.NHibernate.Mappings
{
    public class SoumissionMap : ClassMap<Soumission>
    {
        public SoumissionMap()
        {
            Table("Soumissions");
            LazyLoad();
            Id(x => x.IdSoumission)
                .Column("id")
                .CustomType<int?>()
                .Access.Property()
                .CustomSqlType("INTEGER")
                .Not.Nullable()
                .GeneratedBy.Identity();
            Map(x => x.Date)
                .Column("dateSoumission")
                .CustomType<DateTime>()
                .Access.Property()
                .Generated.Never()
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

            HasMany(x => x.ItemsSoumission)
                .Not.LazyLoad()
                .Access.Property()
                .KeyColumn("idSoumission")
                .Inverse()
                .Cascade.All();
        }
    }
}
