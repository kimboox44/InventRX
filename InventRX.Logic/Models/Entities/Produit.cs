using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventRX.Logic.Models.Entities
{
    public class Produit
    {
        public virtual int? IdProduit { get; set; }
        public virtual Modele Modele { get; set; }
        public virtual CategorieProduit CategorieProduit { get; set; }
        public virtual string Nom { get; set; }
        public virtual string Description { get; set; }
        public virtual string DescriptionDimension { get; set; }
        public virtual string PetiteImage { get; set; }
        public virtual string GrandeImage { get; set; }
        public virtual double Prix { get; set; }
        public virtual int Quantite { get; set; }

        public Produit()
        {

        }
    }
}
