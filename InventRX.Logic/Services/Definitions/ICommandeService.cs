using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InventRX.Logic.Model.Args;
using InventRX.Logic.Model.Entities;

namespace InventRX.Logic.Services.Definitions
{
    public interface ICommandeService
    {
        IList<Commande> RetrieveAll();
        Commande Retrieve(RetrieveCommandeArgs args);
    }
}
