using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt_swe
{
    public class ETF : Wertpapier
    {
        public string Basis { get; set; }

        public ETF(string name, string isin, string basis) : base(name, isin)
        {
            Basis = basis;
        }
    }
}
