using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventRX.Logic.Models.Entities
{
    public class CategorieFournisseur
    {
        public virtual int? IdCategorieFournisseur { get; set; }
        public virtual string Nom { get; set; }

        public CategorieFournisseur()
        {

        }
    }
}
