using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventRX.Logic.Model.Entities
{
    public class Commande
    {
        public virtual int? IdCommande { get; set; }
        public virtual Client Client { get; set; }
        public virtual Employe Employe { get; set; }
        public virtual DateTime Date { get; set; }
        public virtual IList<ItemCommande> ItemsCommande { get; set; }

        public Commande()
        {
            //Date = new DateTime();
        }

       
        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }

            Commande c = obj as Commande;

            if (c == null)
            {
                return false;
            }

            return this.IdCommande == c.IdCommande;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }


    }
}
