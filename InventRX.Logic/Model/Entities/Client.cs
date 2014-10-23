using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventRX.Logic.Model.Entities
{
    public class Client : Personne
    {
        public virtual int? IdClient { get; set; }
        //public virtual int? IdClient { get { return base.IdPersonne; } set { base.IdPersonne = value; } }
        public virtual double Solde { get; set; }

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }

            Client c = obj as Client;

            if (c == null)
            {
                return false;
            }

            return this.IdClient == c.IdClient;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
