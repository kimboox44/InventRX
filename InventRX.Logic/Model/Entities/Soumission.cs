using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventRX.Logic.Model.Entities
{
    public class Soumission
    {
        public virtual int? IdSoumission { get; set; }
        public virtual Client Client { get; set; }
        public virtual Employe Employe { get; set; }
        public virtual DateTime Date { get; set; }
        public virtual IList<ItemSoumission> ItemsSoumission { get; set; }
        public virtual Facture Facture { get; set; }

        public virtual double Price()
        {
            double price = 0;
            if (ItemsSoumission != null)
            {
                foreach (ItemSoumission i in ItemsSoumission)
                {
                    price += i.PrixUnitaire * i.Quantite;
                }
            }
            return price;
        }

        public virtual Facture ConstruireFacture()
        {
            if (Facture == null)
            {
                Facture = new Facture();
            }
            Facture.Client = Client;
            Facture.Employe = Employe;
            Facture.Date = new DateTime();

            double price = 0;
            List<ItemFacture> itemsFacture = new List<ItemFacture>();
            foreach (ItemSoumission i in ItemsSoumission)
            {
                price += i.PrixUnitaire * i.Quantite;

                ItemFacture itemFacture = new ItemFacture();
                itemFacture.Facture = Facture;
                itemFacture.NomProduit = i.NomProduit;
                itemFacture.PrixUnitaire = i.PrixUnitaire;
                itemFacture.Produit = i.Produit;
                itemFacture.Quantite = i.Quantite;
                itemsFacture.Add(itemFacture);
            }

            return Facture;
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }

            Soumission s = obj as Soumission;

            if (s == null)
            {
                return false;
            }

            return this.IdSoumission == s.IdSoumission;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
