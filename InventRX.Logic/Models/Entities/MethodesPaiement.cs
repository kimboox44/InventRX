using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventRX.Logic.Models.Entities
{
    public class MethodesPaiement
    {
        public virtual int? IdMethodesPaiement { get; set; }
        public virtual string Nom { get; set; }

        public MethodesPaiement()
        {

        }
    }
}
