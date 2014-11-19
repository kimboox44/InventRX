using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InventRX.Logic.Model.Entities;
using InventRX.Logic.Model.Args;

namespace InventRX.Services.Definitions
{
    public interface IItemFactureService
    {
        IList<ItemFacture> RetrieveAll();
        IList<ItemFacture> RetrieveAllBy(RetrieveItemFactureArgs args);
        IList<ItemFacture> Retrieve(RetrieveItemFactureArgs args);

        //ItemFacture Retrieve(RetrieveItemFactureArgs args);
    }
}