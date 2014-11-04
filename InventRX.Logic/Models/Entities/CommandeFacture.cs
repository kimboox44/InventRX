using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventRX.Logic.Models.Entities
{
    public class CommandeFacture
    {
        public virtual int? IdCommandeFacture { get; set; }
        public virtual Produit Produit { get; set; }

        public CommandeFacture()
        {
            //Date = new DateTime();
        }
    }
}
