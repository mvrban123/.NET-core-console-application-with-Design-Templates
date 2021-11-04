using mvrban_zadaca_1.Modeli;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mvrban_zadaca_1.Readeri
{
    class IgraciReader
    {
        ReaderClass readerClass;

        string nazivDatoteke;
        public IgraciReader(string nazivDatoteke)
        {
            readerClass = new ReaderClass(nazivDatoteke);
            this.nazivDatoteke = nazivDatoteke;
        }
        public List<Igrac> DajSveIgraceKluba(string oznakaKluba)
        {
            List<Igrac> igraci = new List<Igrac>();     

            for(int i = 1; i < readerClass.DajBrojZapisa(); i++)
            {
                try
                {
                    string[] redakZapisa = readerClass.DajRedakZapisa(i);

                    if(redakZapisa.Length != 4)
                    {
                        Console.WriteLine("Neispravan redak " + i + " u datoteci " + nazivDatoteke);
                        continue;
                    }

                    if(redakZapisa[0] == oznakaKluba)
                    {
                        Igrac igrac = new Igrac();
                        igrac.imeIgraca = redakZapisa[1];
                        igrac.pozicije = redakZapisa[2];
                        igrac.datumRodenja = redakZapisa[3];

                        igraci.Add(igrac);
                    }

                }
                catch(Exception ex)
                {
                    Console.WriteLine("Neispravan redak " + i + " u datoteci " + nazivDatoteke);
                }
            }
            return igraci;
        }
    }
}
