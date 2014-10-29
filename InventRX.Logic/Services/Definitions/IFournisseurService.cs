using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InventRX.Logic.Model.Entities;
using InventRX.Logic.Model.Args;

namespace InventRX.Services.Definitions
{
    public interface IFournisseurService
    {
        IList<Fournisseur> RetrieveAll();
        Fournisseur Retrieve(RetrieveFournisseurArgs args);
        Fournisseur RetrieveByPhone(RetrieveFournisseurArgs args);
        void Update(Fournisseur fournisseur);
        void Insert(Fournisseur fournisseur);
    }
}
