using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventRX.Logic.Model.Entities
{
    public class Paiement
    {
        public virtual int? IdPaiement { get; set; }
        public virtual Client Client { get; set; }
        public virtual Employe Employe { get; set; }
        public virtual DateTime Date { get; set; }
        public virtual double Montant { get; set; }
        public virtual string MethodePaiement { get; set; }
        public virtual Facture Facture { get; set; }
        //public virtual Client Client { get; set; }
        //public virtual DateTime Date { get; set; }

        public Paiement()
        {

        }
    }
}
