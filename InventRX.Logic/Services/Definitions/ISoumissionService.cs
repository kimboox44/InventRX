using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InventRX.Logic.Models.Entities;
using InventRX.Logic.Models.Args;

namespace InventRX.Logic.Services.Definitions
{
    public interface ISoumissionService
    {
        IList<Soumission> RetrieveAll();
        Soumission Retrieve(RetrieveSoumissionArgs args);
        void Update(Soumission soumission);
    }
}