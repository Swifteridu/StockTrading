using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace code
{
    public class ETF : Wertpapier
    {
        public string Basis { get; set; }

        public ETF(string name, string isinNummer, string basis)
            : base(name, isinNummer)
        {
            Basis = basis;
        }
    }
}
