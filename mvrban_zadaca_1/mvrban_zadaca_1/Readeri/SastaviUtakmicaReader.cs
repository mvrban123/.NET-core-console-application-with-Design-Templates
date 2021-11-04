using mvrban_zadaca_1.Modeli;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mvrban_zadaca_1.Readeri
{
    class SastaviUtakmicaReader
    {
        private ReaderClass readerClass;

        public List<ClanSastava> sviSastavi = new List<ClanSastava>();

        public SastaviUtakmicaReader(string sastaviDatoteka)
        {
            ProcitajZapiseDatotekeKlubova(sastaviDatoteka);
            sviSastavi = DajSveDogadaje(sastaviDatoteka);
        }

        private List<ClanSastava> DajSveDogadaje(string sastaviDatoteka)
        {
            List<ClanSastava> clanoviSastava = new List<ClanSastava>();

            for(int i = 1; i < DajBrojZapisaDogadajaZaUtakmicuUDatoteci(); i++)
            {
                try
                {
                    string[] redakZapisa = readerClass.DajRedakZapisa(i);
                    if (redakZapisa.Length != 5)
                    {
                        Console.WriteLine("Neispravan redak " + i + " u datoteci " + sastaviDatoteka);
                        continue;
                    }

                    ClanSastava clanSastava = new ClanSastava();
                    clanSastava.brojUtakmice = int.Parse(redakZapisa[0]);
                    clanSastava.oznakaKluba = redakZapisa[1];
                    clanSastava.vrstaStatusa = redakZapisa[2];
                    clanSastava.imeIgraca = redakZapisa[3];
                    clanSastava.vrstaPozicije = redakZapisa[4];

                    clanoviSastava.Add(clanSastava);

                }
                catch (Exception ex)
                {
                    Console.WriteLine("Neispravan redak " + i + " u datoteci " + sastaviDatoteka);
                }
            }

            return clanoviSastava;
        }

        public List<ClanSastava> DajSastaveTimovaZaOdredenuUtakmicu(int brojUtakmice)
        {
            return sviSastavi.Where(x => x.brojUtakmice == brojUtakmice).ToList();
        }

        private int DajBrojZapisaDogadajaZaUtakmicuUDatoteci()
        {
            try
            {
                return readerClass.DajBrojZapisa();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Problem kod dohvacanja broja zapisa u datoteci sastava utakmica!");
            }
            return 0;
        }

        private void ProcitajZapiseDatotekeKlubova(string sastaviDatoteka)
        {
            try
            {
                this.readerClass = new ReaderClass(sastaviDatoteka);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Problem kod otvaranja datoteke: " + sastaviDatoteka);
            }
        }
    }
}
