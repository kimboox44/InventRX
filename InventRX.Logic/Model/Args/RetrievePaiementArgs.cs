using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InventRX.Logic.Model.Entities;

namespace InventRX.Logic.Model.Args
{
    public class RetrievePaiementArgs
    {
        public int IdPaiement { get; set; }
        public Facture Facture { get; set; }
    }
}
