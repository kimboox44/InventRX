using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventRX.Logic.Model.Entities
{
    public abstract class Personne
    {
        #region Membres privés
        public virtual int? IdPersonne { get; set; }
        public virtual string Nom { get; set; }
        public virtual string Prenom { get; set; }
        public virtual string NumeroCivique { get; set; }
        public virtual string Rue { get; set; }
        public virtual string Ville { get; set; }
        public virtual string CodePostal { get; set; }
        public virtual Province Province { get; set; }
        public virtual string Telephone { get; set; }
        public virtual string Telephone2 { get; set; }
        #endregion

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            return builder.Append(Nom).Append(", ").Append(Prenom).ToString();
        }
    }
}
