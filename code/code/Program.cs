using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace code
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Bank Sparkasse = new Bank();

            Kunde dominik = new Kunde("Knobloch", "Dominik", new Portfolio("Dominik", "Knobloch"));
            Kunde zaka = new Kunde("Silas", "Hüfken", new Portfolio("Silas", "Huefken"));
            Kunde silas = new Kunde("Zakaria", "Sbihi", new Portfolio("Zakaria", "Sbihi"));

            dominik.Portfolio.Portfoliospeichern();
            zaka.Portfolio.Portfoliospeichern();
            silas.Portfolio.Portfoliospeichern();
        }
        }
    }
}
