using InventRX.Logic.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventRX.Logic.Model.Args
{
    public class RetrieveItemSoumissionArgs
    {
        public int IdItemSoumission { get; set; }
        public Soumission Soumission  { get; set; }
    }
}
