using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InventRX.Logic.Model.Entities;
using InventRX.Logic.Model.Args;

namespace InventRX.Services.Definitions
{
    public interface IItemSoumissionService
    {
        IList<ItemSoumission> RetrieveAll();
        IList<ItemSoumission> RetrieveAllBy(RetrieveItemSoumissionArgs args);
        IList<ItemSoumission> Retrieve(RetrieveItemSoumissionArgs args);

        //ItemSoumission Retrieve(RetrieveItemSoumissionArgs args);
    }
}