using mvrban_zadaca_1.Podaci;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mvrban_zadaca_1.Pomagaci
{
    class InicijalizatorAkcija
    {
        public InicijalizatorAkcija()
        {
            PokretanjeAkcija();
        }

        private void PokretanjeAkcija()
        {
            string akcija;
            do
            {
                Console.WriteLine("Moguce akcije: ");
                Console.WriteLine("Pregled ljestvice klubova 'T (N)'");
                Console.WriteLine("Pregled ljestvice strijelaca 'S (N)' ");
                Console.WriteLine("Pregled ljestvice kartona klubova 'K (N)' ");
                Console.WriteLine("Pregled rezultata utakmica za klub 'R (KLUB) (KOLO)'");
                Console.WriteLine("Enter za izlaz van programa!");
                Console.WriteLine("");
                Console.Write("Odaberite akciju: ");

                akcija = Console.ReadLine();
                Console.Clear();
                IzvrsiAkciju(akcija);
            } while (akcija.Length != 0);
        }

        private void IzvrsiAkciju(string akcija)
        {
            string[] naredba = akcija.Split(" ");
            if(naredba[0] == "T")
            {
                try
                {
                    if(naredba.Length == 1)
                    {
                        NogometnaLigaSingleton.DohvatiInstancu().DajLjestvicuKlubova(0);
                    }
                    else
                    {
                        NogometnaLigaSingleton.DohvatiInstancu().DajLjestvicuKlubova(int.Parse(naredba[1]));
                    }
                }
                catch(Exception ex)
                {
                    Console.WriteLine("Krivi parametri akcije!");
                }
            }
            if (naredba[0] == "S")
            {
                try
                {
                    if (naredba.Length == 1)
                    {
                        NogometnaLigaSingleton.DohvatiInstancu().DajLjestvicuStrijelaca(0);
                    }
                    else
                    {
                        NogometnaLigaSingleton.DohvatiInstancu().DajLjestvicuStrijelaca(int.Parse(naredba[1]));
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Krivi parametri akcije!");
                }
            }
            if (naredba[0] == "K")
            {
                try
                {
                    if (naredba.Length == 1)
                    {
                        NogometnaLigaSingleton.DohvatiInstancu().DajLjestvicuKartona(0);
                    }
                    else
                    {
                        NogometnaLigaSingleton.DohvatiInstancu().DajLjestvicuKartona(int.Parse(naredba[1]));
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Krivi parametri akcije!");
                }
            }
            if (naredba[0] == "R")
            {
                try
                {
                    if (naredba.Length == 2)
                    {
                        NogometnaLigaSingleton.DohvatiInstancu().DajRezultateKluba(naredba[1], 0);
                    }
                    else
                    {
                        NogometnaLigaSingleton.DohvatiInstancu().DajRezultateKluba(naredba[1], int.Parse(naredba[2]));
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Krivi parametri akcije!");
                }
            }
        }
    }
}
