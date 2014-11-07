using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InventRX.Logic.Model.Entities;
using InventRX.Logic.Model.Args;

namespace InventRX.Services.Definitions
{
    public interface ITaxeService
    {
        IList<Taxe> RetrieveAll();
        Taxe Retrieve(RetrieveTaxeArgs args);
    }
}