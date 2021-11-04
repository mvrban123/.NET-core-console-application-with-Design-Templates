using mvrban_zadaca_1.Modeli;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mvrban_zadaca_1.Readeri
{
    class NogometniKlubReader
    {
        private ReaderClass readerClass;
        private IgraciReader igraciReader;
        public List<NogometniKlub> DajNogometneKlubove(string kluboviDatoteka, string igraciDatoteka)
        {
            List<NogometniKlub> nogometniKlubovi = new List<NogometniKlub>();

            procitajZapiseDatotekeKlubova(kluboviDatoteka);

            procitajZapiseDatotekeIgraca(igraciDatoteka);

            for (int i = 1; i < dajBrojZapisaKlubovaUDatoteci(); i++)
            {
                try
                {
                    string[] redakZapisa = readerClass.DajRedakZapisa(i);
                    if(redakZapisa.Length != 3)
                    {
                        Console.WriteLine("Neispravan redak " + i + " u datoteci " + kluboviDatoteka);
                        continue;
                    }

                    NogometniKlub nogometniKlub = new NogometniKlub();

                    nogometniKlub.oznakaKluba = redakZapisa[0];
                    nogometniKlub.nazivKluba = redakZapisa[1];
                    nogometniKlub.trenerKluba = redakZapisa[2];

                    nogometniKlub.igraciKluba = igraciReader.DajSveIgraceKluba(nogometniKlub.oznakaKluba);

                    nogometniKlubovi.Add(nogometniKlub);
                }
                catch(Exception ex)
                {
                    Console.WriteLine("Neispravan redak " + i + " u datoteci " + kluboviDatoteka);
                }
            }

            return nogometniKlubovi;
        }
        private void procitajZapiseDatotekeKlubova(string kluboviDatoteka)
        {
            try
            {
                this.readerClass = new ReaderClass(kluboviDatoteka);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Problem kod otvaranja datoteke: " + kluboviDatoteka);
            }
        }

        private void procitajZapiseDatotekeIgraca(string igraciDatoteka)
        {
            try
            {
                this.igraciReader = new IgraciReader(igraciDatoteka);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Problem kod otvaranja datoteke: " + igraciDatoteka);
            }
        }

        private int dajBrojZapisaKlubovaUDatoteci()
        {
            try
            {
                return readerClass.DajBrojZapisa();
            }
            catch(Exception ex)
            {
                Console.WriteLine("Problem kod dohvacanja broja zapisa u datoteci klubova!");
            }
            return 0;
        }
    }
}
