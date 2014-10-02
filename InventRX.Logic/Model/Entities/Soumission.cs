﻿using System;
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

        public Soumission()
        {
            //Date = new DateTime();
        }
    }
}
