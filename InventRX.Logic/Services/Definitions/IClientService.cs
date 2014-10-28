﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InventRX.Logic.Model.Entities;
using InventRX.Logic.Model.Args;

namespace InventRX.Services.Definitions
{
    public interface IClientService
    {
        IList<Client> RetrieveAll();
        Client Retrieve(RetrieveClientArgs args);
        Client RetrieveByPhone(RetrieveClientArgs args);
        void Update(Client client);
        void Insert(Client client);
    }
}
