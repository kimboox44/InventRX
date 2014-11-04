using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventRX.Logic.Models.Entities
{
    public class CategorieProduit
    {
        public virtual int? IdCategorieProduit { get; set; }
        public virtual string Nom { get; set; }

        public CategorieProduit()
        {

        }
    }
}
