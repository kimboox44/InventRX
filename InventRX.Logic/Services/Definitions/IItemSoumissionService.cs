using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InventRX.Logic.Models.Entities;
using InventRX.Logic.Models.Args;

namespace InventRX.Logic.Services.Definitions
{
    public interface IItemSoumissionService
    {
        IList<ItemSoumission> RetrieveAll();
        IList<ItemSoumission> RetrieveAllBy(RetrieveItemSoumissionArgs args);
        IList<ItemSoumission> Retrieve(RetrieveItemSoumissionArgs args);

        //ItemSoumission Retrieve(RetrieveItemSoumissionArgs args);
    }
}