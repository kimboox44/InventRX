using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventRX.Logic.Models.Entities
{
    public class Province
    {
        public virtual int? IdProvince { get; set; }
        public virtual string Nom { get; set; }
        public virtual string Abreviation { get; set; }

        public Province()
        {

        }
    }
}
