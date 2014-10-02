using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventRX.Logic.Model.Entities
{
    public class Paiements
    {
        public virtual int? IdPaiements { get; set; }
        public virtual MethodesPaiement MethodesPaiement { get; set; }
        public virtual Client Client { get; set; }
        public virtual DateTime Date { get; set; }

        public Paiements()
        {

        }
    }
}
