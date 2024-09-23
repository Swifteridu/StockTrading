using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace code
{
    public class Optionsschein : Wertpapier
    {
        public DateTime LaufzeitEnde { get; set; }
        public string Bezeichner { get; set; }

        public Optionsschein(string name, string isinNummer, DateTime laufzeitEnde, string bezeichner)
            : base(name, isinNummer)
        {
            LaufzeitEnde = laufzeitEnde;
            Bezeichner = bezeichner;
        }
    }
}
