using InventRX.Logic.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventRX.Logic.Models.Args
{
    public class RetrieveItemSoumissionArgs
    {
        public int IdItemSoumission { get; set; }
        public Soumission Soumission  { get; set; }
    }
}
