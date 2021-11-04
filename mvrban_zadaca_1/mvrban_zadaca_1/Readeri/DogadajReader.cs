using mvrban_zadaca_1.Modeli;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mvrban_zadaca_1.Readeri
{
    class DogadajReader
    {
        private ReaderClass readerClass;

        public List<Dogadaj> sviDogadaji = new List<Dogadaj>();

        public DogadajReader(string dogadajiDatoteka)
        {
            ProcitajZapiseDatotekeKlubova(dogadajiDatoteka);
            sviDogadaji = DajSveDogadaje(dogadajiDatoteka);
        }

        public List<Dogadaj> DajSveDogadaje(string dogadajiDatoteka)
        {
            List<Dogadaj> dogadaji = new List<Dogadaj>();

            for(int i = 1; i < DajBrojZapisaDogadajaZaUtakmicuUDatoteci(); i++)
            {
                try
                {
                    string[] redakZapisa = readerClass.DajRedakZapisa(i);

                    Dogadaj dogadaj = new Dogadaj();
                    dogadaj.brojDogadaja = int.Parse(redakZapisa[0]);
                    dogadaj.minutaDogadaja = redakZapisa[1];
                    dogadaj.vrstaDogadaja = redakZapisa[2];
                    dogadaj.klubDogadaja = redakZapisa[3];
                    dogadaj.imeIgraca = redakZapisa[4];
                    dogadaj.imeZamjeneIgraca = redakZapisa[5];

                    dogadaji.Add(dogadaj);
                }
                catch(Exception ex)
                {
                    Console.WriteLine("Neispravan redak " + i + " u datoteci " + dogadajiDatoteka);
                }
            }

            return dogadaji;
        }

        public List<Dogadaj> DajSveDogadajeZaOdredenuUtakmicu(int brojUtakmice)
        {
            return sviDogadaji.Where(x => x.brojDogadaja == brojUtakmice).ToList();
        }

        private void ProcitajZapiseDatotekeKlubova(string dogadajiDatoteka)
        {
            try
            {
                this.readerClass = new ReaderClass(dogadajiDatoteka);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Problem kod otvaranja datoteke: " + dogadajiDatoteka);
            }
        }

        private int DajBrojZapisaDogadajaZaUtakmicuUDatoteci()
        {
            try
            {
                return readerClass.DajBrojZapisa();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Problem kod dohvacanja broja zapisa u datoteci dogadaja!");
            }
            return 0;
        }
    }
}
