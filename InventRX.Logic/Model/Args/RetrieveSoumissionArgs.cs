using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InventRX.Logic.Model.Entities;

namespace InventRX.Logic.Model.Args
{
    public class RetrieveSoumissionArgs
    {
        public int IdSoumission { get; set; }
        public Client Client { get; set; }
    }
}
