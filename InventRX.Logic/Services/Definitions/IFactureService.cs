using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InventRX.Logic.Model.Entities;
using InventRX.Logic.Model.Args;

namespace InventRX.Services.Definitions
{
    public interface IFactureService
    {
        IList<Facture> RetrieveAll();
        Facture Retrieve(RetrieveFactureArgs args);
        IList<Facture> RetrieveBy(RetrieveFactureArgs args);
        void Update(Facture facture);
        void Insert(Facture facture);
    }
}