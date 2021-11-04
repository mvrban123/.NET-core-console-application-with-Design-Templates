using mvrban_zadaca_1.Modeli;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mvrban_zadaca_1.Readeri
{
    class UtakmicaReader
    {
        private ReaderClass readerClass;
        private DogadajReader dogadajReader;
        private SastaviUtakmicaReader sastaviUtakmica;

        public UtakmicaReader()
        {

        }
        public List<Utakmica> DajSveUtakmice(string utakmiceDatoteka, string dogadajiDatoteka, string sastaviDatoteka)
        {
            List<Utakmica> utakmice = new List<Utakmica>();

            ProcitajZapiseDatotekeUtakmica(utakmiceDatoteka);
            ProcitajSveDogadaje(dogadajiDatoteka);
            ProcitajSveSastaveTimova(sastaviDatoteka);

            for (int i = 1; i < DajBrojZapisaUtakmicaUDatoteci(); i++)
            {
                try
                {
                    string[] redakZapisa = readerClass.DajRedakZapisa(i);
                    if(redakZapisa.Length != 5)
                    {
                        Console.WriteLine("Neispravan redak " + i + " u datoteci " + utakmiceDatoteka);
                        continue;
                    }

                    Utakmica utakmica = new Utakmica();

                    utakmica.brojUtakmice = int.Parse(redakZapisa[0]);
                    utakmica.brojKola = int.Parse(redakZapisa[1]);
                    utakmica.oznakaDomacina = redakZapisa[2];
                    utakmica.oznakaGosta = redakZapisa[3];
                    utakmica.datumUtakmice = redakZapisa[4];
                    utakmica.dogadajiUtakmice = DajSveDogadajeZaUtakmicu(utakmica.brojUtakmice);
                    utakmica.sastaviTimova = DajSveSastaveZaUtakmicu(utakmica.brojUtakmice);

                    utakmice.Add(utakmica);
                }
                catch(Exception ex)
                {
                    Console.WriteLine("Neispravan redak " + i + " u datoteci " + utakmiceDatoteka);
                }
            }

            return utakmice;
        }
        private void ProcitajZapiseDatotekeUtakmica(string utakmiceDatoteka)
        {
            try
            {
                this.readerClass = new ReaderClass(utakmiceDatoteka);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Problem kod otvaranja datoteke: " + utakmiceDatoteka);
            }
        }

        private int DajBrojZapisaUtakmicaUDatoteci()
        {
            try
            {
                return readerClass.DajBrojZapisa();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Problem kod dohvacanja broja zapisa u datoteci utakmica!");
            }
            return 0;
        }

        private void ProcitajSveDogadaje(string dogadajiDatoteka)
        {
            try
            {
                this.dogadajReader = new DogadajReader(dogadajiDatoteka);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Problem kod otvaranja datoteke: " + dogadajiDatoteka);
            }
        }

        private List<Dogadaj> DajSveDogadajeZaUtakmicu(int brojUtakmice)
        {
            return dogadajReader.DajSveDogadajeZaOdredenuUtakmicu(brojUtakmice);
        }

        private void ProcitajSveSastaveTimova(string sastaviDatoteka)
        {
            try
            {
                this.sastaviUtakmica = new SastaviUtakmicaReader(sastaviDatoteka);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Problem kod otvaranja datoteke: " + sastaviDatoteka);
            }
        }
        private List<ClanSastava> DajSveSastaveZaUtakmicu(int brojUtakmice)
        {
            return sastaviUtakmica.DajSastaveTimovaZaOdredenuUtakmicu(brojUtakmice);
        }


    }
}
