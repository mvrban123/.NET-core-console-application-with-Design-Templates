using ConsoleTables;
using mvrban_zadaca_1.Modeli;
using mvrban_zadaca_1.Readeri;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mvrban_zadaca_1.Podaci
{
    class NogometnaLigaSingleton
    {
        private static NogometnaLigaSingleton instanca;
        private List<NogometniKlub> nogometniKlubovi = new List<NogometniKlub>();
        private List<Utakmica> listaUtakmica = new List<Utakmica>();

        public static NogometnaLigaSingleton DohvatiInstancu()
        {
            if (instanca == null)
            {
                instanca = new NogometnaLigaSingleton();
            }

            return instanca;
        }

        public void DohvatiSvePodatkeLige(string datotekaKlubova, string datotekaIgraca, string datotekaUtakmica, string datotekaDogadaja, string datotekaSastava)
        {
            NogometniKlubReader nogometniKlubReader = new NogometniKlubReader();
            nogometniKlubovi = nogometniKlubReader.DajNogometneKlubove(datotekaKlubova, datotekaIgraca);

            UtakmicaReader utakmicaReader = new UtakmicaReader();
            listaUtakmica = utakmicaReader.DajSveUtakmice(datotekaUtakmica, datotekaDogadaja, datotekaSastava);
        }

        public List<NogometniKlub> DajSveNogometneKlubove()
        {
            return nogometniKlubovi;
        }

        public List<Utakmica> DajSveUtakmice()
        {
            return listaUtakmica;
        }


        public void DajLjestvicuKlubova(int brojKola)
        {
            if (brojKola < 1) return;
            StatistikaLjestvice statistikaLjestvice = new StatistikaLjestvice();
            List<StatistikaLjestvice> utakmice;
            try
            {
                if (brojKola == 0)
                {
                    utakmice =
                        statistikaLjestvice.DajLjestvicuKlubova(nogometniKlubovi, listaUtakmica.Where(x => x.dogadajiUtakmice.Count > 0).ToList());
                }
                else
                {
                    utakmice =
                        statistikaLjestvice.DajLjestvicuKlubova(nogometniKlubovi, listaUtakmica.Where(x => x.dogadajiUtakmice.Count > 0 && x.brojKola <= brojKola).ToList());
                }

                var table = new ConsoleTable("Pozicija", "Ime kluba", "Broj odigranih kola", "Broj pobjeda", "Broj nerješenih", "Broj poraza", "Broj danih golova",
                    "Broj primljenih golova", "Razlika golova", "Broj bodova");
                for(int i = 0; i < utakmice.Count; i++)
                {
                    table.AddRow((i+1),utakmice[i].nazivKluba, utakmice[i].brojOdigranihKola, utakmice[i].brojPobjeda, utakmice[i].brojNerješenihUtakcmica,
                        utakmice[i].brojPoraza, utakmice[i].brojDanihGolova, utakmice[i].brojPrimljenihGolova, (utakmice[i].brojDanihGolova - utakmice[i].brojPrimljenihGolova)
                        , utakmice[i].brojBodova);
                }
                table.Write(Format.Alternative);
            }
            catch(Exception ex)
            {
                Console.WriteLine("Problem kod izvrsavanja radnje!");
            }
        }

        public void DajLjestvicuStrijelaca(int brojKola)
        {
            if (brojKola < 1) return;
            StatistikaStrijelaca statistikaStrijelaca = new StatistikaStrijelaca();
            List<StatistikaStrijelaca> listaStrijelaca;
            try
            {
                if(brojKola == 0)
                {
                    listaStrijelaca =
                        statistikaStrijelaca.DajListuStrijelaca(nogometniKlubovi, listaUtakmica.Where(x => x.dogadajiUtakmice.Count > 0).ToList());
                }
                else
                {
                    listaStrijelaca =
                       statistikaStrijelaca.DajListuStrijelaca(nogometniKlubovi, listaUtakmica.Where(x => x.dogadajiUtakmice.Count > 0 && x.brojKola <= brojKola).ToList());
                }

                var table = new ConsoleTable("Pozicija", "Ime igraca", "Klub", "Broj golova");
                for(int i = 0; i < listaStrijelaca.Count; i++)
                {
                    table.AddRow((i+1),listaStrijelaca[i].imeIgraca, listaStrijelaca[i].nazivKluba, listaStrijelaca[i].brojGolova);
                }
                table.Write(Format.Alternative);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Problem kod izvrsavanja radnje!");
            }
        }

        public void DajLjestvicuKartona(int brojKola)
        {
            if (brojKola < 1) return;
            StatistikaKartonaKlubova statistikaKartonaKlubova = new StatistikaKartonaKlubova();
            List<StatistikaKartonaKlubova> listaKartonaKlubova;
            try
            {
                if(brojKola == 0)
                {
                    listaKartonaKlubova = statistikaKartonaKlubova.DajStatistikuKartonaZaKlubove(nogometniKlubovi, listaUtakmica.Where(
                        x => x.dogadajiUtakmice.Count > 0).ToList());
                }
                else
                {
                    listaKartonaKlubova = statistikaKartonaKlubova.DajStatistikuKartonaZaKlubove(nogometniKlubovi, listaUtakmica.Where(
                    x => x.dogadajiUtakmice.Count > 0 && x.brojKola <= brojKola).ToList());
                }

                var table = new ConsoleTable("Pozicija", "Ime kluba", "Broj zutih kartona", "Broj drugih zutih kartona", "Broj crvenih kartona", "Ukupni broj kartona");

                for(int i = 0; i < listaKartonaKlubova.Count; i++)
                {
                    table.AddRow((i + 1), listaKartonaKlubova[i].nazivKluba, listaKartonaKlubova[i].brojZutihKartona, 
                        listaKartonaKlubova[i].brojDrugihZutihKartona, listaKartonaKlubova[i].brojCrvenihKartona,
                        (listaKartonaKlubova[i].brojZutihKartona + listaKartonaKlubova[i].brojDrugihZutihKartona + listaKartonaKlubova[i].brojCrvenihKartona));
                }
                table.Write(Format.Alternative);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Problem kod izvrsavanja radnje!");
            }

        }

        public void DajRezultateKluba(string oznakaKluba, int brojKola)
        {
            if (brojKola < 1) return;
            StatistikaRezultataUtakmica statistikaRezultataUtakmica = new StatistikaRezultataUtakmica();
            List<StatistikaRezultataUtakmica> listaRezultataUtakmica;
            try
            {
                if(brojKola == 0)
                {
                    listaRezultataUtakmica = statistikaRezultataUtakmica.
                        OdrediRezultateSvihUtakmicaZaJedanKlub(nogometniKlubovi, listaUtakmica.Where(
                        x => x.dogadajiUtakmice.Count > 0 && (x.oznakaDomacina == "D" || x.oznakaGosta == "D")).ToList());
                }
                else
                {
                    listaRezultataUtakmica = statistikaRezultataUtakmica.
                        OdrediRezultateSvihUtakmicaZaJedanKlub(nogometniKlubovi, listaUtakmica.Where(
                        x => x.dogadajiUtakmice.Count > 0 && x.brojKola <= brojKola && (x.oznakaDomacina == oznakaKluba || x.oznakaGosta == oznakaKluba)).ToList());
                }

                var table = new ConsoleTable("Broj kola", "Datum utakmice", "Domacin", "Gost", "Rezultat");

                for(int i = 0; i < listaRezultataUtakmica.Count; i++)
                {
                    table.AddRow(listaRezultataUtakmica[i].brojKola, listaRezultataUtakmica[i].vrijemeUtakmice, listaRezultataUtakmica[i].nazivKluba,
                        listaRezultataUtakmica[i].imeGostujucegTima, listaRezultataUtakmica[i].goloviDomacegTime + " : " + listaRezultataUtakmica[i].goloviGostujucegTime);
                }
                table.Write(Format.Alternative);
            }
            catch(Exception ex)
            {
                Console.WriteLine("Problem kod izvrsavanja radnje!");
            }
        }
    }
}
