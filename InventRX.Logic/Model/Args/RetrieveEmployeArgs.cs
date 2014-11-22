using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventRX.Logic.Model.Args
{
    public class RetrieveEmployeArgs
    {
        public int IdEmploye { get; set; }
        public string NomUsager{ get; set; }
        public string MotDePasse { get; set; }
    }
}
