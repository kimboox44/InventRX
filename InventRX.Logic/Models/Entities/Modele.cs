﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventRX.Logic.Models.Entities
{
    public class Modele
    {
        public virtual int? IdModele { get; set; }
        public virtual string Nom { get; set; }

        public Modele()
        {

        }
		
		public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            return builder.Append(Nom).ToString();
        }
    }
}
