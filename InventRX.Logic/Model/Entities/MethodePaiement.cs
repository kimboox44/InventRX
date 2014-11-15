using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventRX.Logic.Model.Entities
{
    public class MethodePaiement
    {
        public virtual int? IdMethodePaiement { get; set; }
        public virtual string Nom { get; set; }

        public MethodePaiement()
        {

        }
    }
}
