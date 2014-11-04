using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InventRX.Logic.Models.Entities;
using InventRX.Logic.Models.Args;

namespace InventRX.Logic.Services.Definitions
{
    public interface IClientService
    {
        IList<Client> RetrieveAll();
        Client Retrieve(RetrieveClientArgs args);
    }
}
