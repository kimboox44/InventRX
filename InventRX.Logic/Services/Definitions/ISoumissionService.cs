﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InventRX.Logic.Model.Entities;
using InventRX.Logic.Model.Args;

namespace InventRX.Services.Definitions
{
    public interface ISoumissionService
    {
        IList<Soumission> RetrieveAll();
        //IList<Client> Retrieve(RetrieveSoumissionArgs args);
        Soumission Retrieve(RetrieveSoumissionArgs args);
    }
}