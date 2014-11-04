using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventRX.Logic.Models.Entities
{
    public class ItemSoumission
    {
        public virtual int? IdItemSoumission { get; set; }
        public virtual Soumission Soumission { get; set; }
        public virtual Produit Produit { get; set; }
        public virtual string NomProduit { get; set; }
        public virtual int Quantite { get; set; }
        public virtual double PrixUnitaire { get; set; }

        public ItemSoumission()
        {
            //Date = new DateTime();
        }
    }
}
