using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InventRX.Logic.Model.Args;
using InventRX.Logic.Model.Entities;

namespace InventRX.Services.Definitions
{
    public interface IProduitService
    {
        IList<Produit> RetrieveAll();
        Produit Retrieve(RetrieveProduitArgs args);
    }
}

