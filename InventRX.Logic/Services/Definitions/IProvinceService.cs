using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InventRX.Logic.Model.Entities;
using InventRX.Logic.Model.Args;

namespace InventRX.Services.Definitions
{
    public interface IProvinceService
    {
        IList<Province> RetrieveAll();
        Province Retrieve(RetrieveProvinceArgs args);
        Province RetrieveByAbreviation(RetrieveProvinceArgs args);

    }
}