using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventRX.Logic.Models.Entities
{
    public class PosteEmploye
    {
        public virtual int? IdPosteEmploye { get; set; }
        public virtual string Nom { get; set; }
        public virtual bool estAdmin { get; set; }
        public virtual bool estManager { get; set; }
        public virtual bool estEmploye { get; set; }

        public PosteEmploye()
        {

        }
    }
}
