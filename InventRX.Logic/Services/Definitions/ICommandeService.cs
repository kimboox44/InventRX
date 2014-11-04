using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InventRX.Logic.Models.Args;
using InventRX.Logic.Models.Entities;

namespace InventRX.Logic.Services.Definitions
{
    public interface ICommandeService
    {
        IList<Commande> RetrieveAll();
        Commande Retrieve(RetrieveCommandeArgs args);
    }
}
