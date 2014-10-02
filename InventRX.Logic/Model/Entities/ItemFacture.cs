using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventRX.Logic.Model.Entities
{
    public class ItemFacture
    {
        public virtual int? IdItemFacture { get; set; }
        public virtual Commande Commande { get; set; }
        public virtual Produit Produit { get; set; }
        public virtual string NomProduit { get; set; }
        public virtual int Quantite { get; set; }
        public virtual double PrixUnitaire { get; set; }

        public ItemFacture()
        {
            //Date = new DateTime();
        }
    }
}
