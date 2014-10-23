using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventRX.Logic.Model.Entities
{
    public class CategorieProduit
    {
        public virtual int? IdCategorieProduit { get; set; }
        public virtual string Nom { get; set; }

        public CategorieProduit()
        {

        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            return builder.Append(Nom).ToString();
        }

    }
}
