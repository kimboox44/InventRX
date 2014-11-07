using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventRX.Logic.Model.Entities
{
    public class SoumissionFacture
    {
        public virtual int? IdCommandeFacture { get; set; }
        public virtual Produit Produit { get; set; }

        public SoumissionFacture()
        {
            //Date = new DateTime();
        }
    }
}
