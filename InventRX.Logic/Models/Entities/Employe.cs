using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventRX.Logic.Models.Entities
{
    public class Employe : Personne
    {
        public virtual int? IdEmploye { get { return base.IdPersonne; } set { base.IdPersonne = value; } }
        public virtual PosteEmploye PosteEmploye { get; set; }
        public virtual string NomUsager { get; set; }
        public virtual string MotDePasse { get; set; }
        public virtual string NAS { get; set; }

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }

            Employe e = obj as Employe;

            if (e == null)
            {
                return false;
            }

            return this.IdEmploye == e.IdEmploye;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
