using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventRX.Logic.Model.Entities
{
    public class Taxe
    {
        public virtual int? IdTaxe { get; set; }
        public virtual double TPS { get; set; }
        public virtual double TVQ { get; set; }
        public virtual DateTime Date { get; set; }

        public Taxe()
        {

        }
    }
}
