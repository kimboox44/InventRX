using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventRX.Logic.Models.Entities
{
    public class Facture
    {
        public virtual int? IdFacture { get; set; }
        public virtual Client Client { get; set; }
        public virtual Employe Employe { get; set; }
        public virtual Taxe Taxe { get; set; }
        public virtual DateTime Date { get; set; }

        public Facture()
        {
            //Date = new DateTime();
        }
    }
}
