using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventRX.Logic.Model.Entities
{
    public abstract class Fournisseur
    {
        #region Membres privés
        public virtual int? IdFournisseur { get; set; }
        public virtual CategorieFournisseur CategorieFournisseur { get; set; }
        public virtual string Nom { get; set; }
        public virtual string Description { get; set; }
        public virtual string NumeroCivique { get; set; }
        public virtual string Rue { get; set; }
        public virtual string Ville { get; set; }
        public virtual string CodePostal { get; set; }
        public virtual Province Province { get; set; }
        public virtual string Telephone { get; set; }
        #endregion

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            return builder.Append(Nom).ToString();
        }
    }
}
