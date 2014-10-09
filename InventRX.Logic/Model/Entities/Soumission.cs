using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventRX.Logic.Model.Entities
{
    public class Soumission
    {
        public virtual int? IdSoumission { get; set; }
        public virtual Client Client { get; set; }
        public virtual Employe Employe { get; set; }
        public virtual DateTime Date { get; set; }
        public virtual IList<ItemSoumission> ItemsSoumission { get; set; }

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }

            Soumission s = obj as Soumission;

            if (s == null)
            {
                return false;
            }

            return this.IdSoumission == s.IdSoumission;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
