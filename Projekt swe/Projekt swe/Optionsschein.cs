using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt_swe
{
    public class Optionsschein : Wertpapier
    {
        public DateTime LaufzeitEnde { get; set; }
        public string Bezeichner { get; set; }

        public Optionsschein(string name, string isin, DateTime laufzeitEnde, string bezeichner) : base(name, isin)
        {
            LaufzeitEnde = laufzeitEnde;
            Bezeichner = bezeichner;
        }
    }
}
