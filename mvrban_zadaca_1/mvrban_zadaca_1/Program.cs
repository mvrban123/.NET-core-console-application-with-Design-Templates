using mvrban_zadaca_1.Modeli;
using mvrban_zadaca_1.Podaci;
using mvrban_zadaca_1.Pomagaci;
using mvrban_zadaca_1.Readeri;
using System;
using System.Collections.Generic;
using System.IO;

namespace mvrban_zadaca_1
{
    class Program
    {
        static void Main(string[] args)
        {
            if (!Parametri.ProvjeraUlaznihParametara(args))
            {
                Console.WriteLine("Krivo uneseni pocetni parametri!\n Program ne moze poceti sa radom!");              
                return;
            }

            NogometnaLigaSingleton.DohvatiInstancu().DohvatiSvePodatkeLige(
                Parametri.DajParametarDatotekeKlubova(), Parametri.DajParametarDatotekeIgraca(), Parametri.DajParametarDatotekeUtakmica(),
                Parametri.DajParametarDatotekeDogadaja(), Parametri.DajParametarDatotekeSastava());

            InicijalizatorAkcija inicijalizatorAkcija = new InicijalizatorAkcija();

            /*NogometnaLigaSingleton.DohvatiInstancu().RandomPrintUtakmica();
            NogometnaLigaSingleton.DohvatiInstancu().RandomPrintStrijelaca();
            NogometnaLigaSingleton.DohvatiInstancu().RandomPrintKartona();
            NogometnaLigaSingleton.DohvatiInstancu().RandomPrintRezultatZaKlub();*/

            //Console.WriteLine(Path.GetFullPath("DZ1_utakmice.csv"));
        }
    }
}
