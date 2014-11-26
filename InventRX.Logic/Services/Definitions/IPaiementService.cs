using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InventRX.Logic.Model.Entities;
using InventRX.Logic.Model.Args;

namespace InventRX.Services.Definitions
{
    public interface IPaiementService
    {
        IList<Paiement> RetrieveAll();
        Paiement Retrieve(RetrievePaiementArgs args);
        IList<Paiement> RetrieveBy(RetrievePaiementArgs args);
        void Update(Paiement paiement);
        void Insert(Paiement paiement);
    }
}