using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mvrban_zadaca_1.Modeli
{
    abstract class StatistikaZapis
    {
        public string oznakaKluba { get; set; }
        public string nazivKluba { get; set; }

        public bool IgracUSastavuTimu(Utakmica utakmica, string klubDogadaja,string igrac)
        {
            return utakmica.sastaviTimova.Exists(x => x.imeIgraca == igrac && x.oznakaKluba == klubDogadaja && x.vrstaStatusa == "S");
        }

        public bool IgracBioZamjena(Utakmica utakmica, string klubDogadaja, string igrac)
        {
            return utakmica.dogadajiUtakmice.Exists(x => x.vrstaDogadaja == "20" && x.klubDogadaja == klubDogadaja && x.imeZamjeneIgraca == igrac);
        }
    }

    class StatistikaStrijelaca : StatistikaZapis
    {
        public string imeIgraca { get; set; }
        public int brojGolova = 0;

        public List<StatistikaStrijelaca> DajListuStrijelaca(List<NogometniKlub> nogometniKlubovi, List<Utakmica> odigraneUtakmice)
        {
            List<StatistikaStrijelaca> statistikaStrijelaca = new List<StatistikaStrijelaca>();
            
            for(int i = 0; i < odigraneUtakmice.Count; i++)
            {
                for(int j = 0; j < odigraneUtakmice[i].dogadajiUtakmice.Count; j++)
                {
                    if (odigraneUtakmice[i].dogadajiUtakmice[j].vrstaDogadaja == "1" || odigraneUtakmice[i].dogadajiUtakmice[j].vrstaDogadaja == "2")
                    {
                        if(!(IgracUSastavuTimu(odigraneUtakmice[i], odigraneUtakmice[i].dogadajiUtakmice[j].klubDogadaja, odigraneUtakmice[i].dogadajiUtakmice[j].imeIgraca) ||
                            IgracBioZamjena(odigraneUtakmice[i], odigraneUtakmice[i].dogadajiUtakmice[j].klubDogadaja, odigraneUtakmice[i].dogadajiUtakmice[j].imeIgraca)))
                        {
                            continue;
                        }
                        if(statistikaStrijelaca.Exists(x => 
                            x.imeIgraca == odigraneUtakmice[i].dogadajiUtakmice[j].imeIgraca && x.oznakaKluba == odigraneUtakmice[i].dogadajiUtakmice[j].klubDogadaja))
                        {
                            statistikaStrijelaca.FirstOrDefault(x => 
                                x.imeIgraca == odigraneUtakmice[i].dogadajiUtakmice[j].imeIgraca && x.oznakaKluba == odigraneUtakmice[i].dogadajiUtakmice[j].klubDogadaja).brojGolova++;
                        }
                        else
                        {
                            StatistikaStrijelaca statistikaStrijelca = new StatistikaStrijelaca();
                            statistikaStrijelca.oznakaKluba = odigraneUtakmice[i].dogadajiUtakmice[j].klubDogadaja;
                            statistikaStrijelca.nazivKluba = nogometniKlubovi.FirstOrDefault(
                                x => x.oznakaKluba == odigraneUtakmice[i].dogadajiUtakmice[j].klubDogadaja).nazivKluba;
                            statistikaStrijelca.imeIgraca = odigraneUtakmice[i].dogadajiUtakmice[j].imeIgraca;
                            statistikaStrijelca.brojGolova++;
                            statistikaStrijelaca.Add(statistikaStrijelca);
                        }
                    }
                }
            }

            return statistikaStrijelaca.OrderByDescending(x => x.brojGolova).ToList();
        }
    }

    class StatistikaKartonaKlubova : StatistikaZapis
    {
        public int brojZutihKartona { get; set; }
        public int brojDrugihZutihKartona { get; set; }
        public int brojCrvenihKartona { get; set; }
        
        public List<StatistikaKartonaKlubova> DajStatistikuKartonaZaKlubove(List<NogometniKlub> nogometniKlubovi, List<Utakmica> odigraneUtakmice)
        {
            List<StatistikaKartonaKlubova> statistikaKartonaKlubova = PostaviKluboveZaStatistiku(nogometniKlubovi);
            StatistikaKartonaIgraca statistikaKartonaIgraca = new StatistikaKartonaIgraca();
            List<StatistikaKartonaIgraca> kartoniIgracaNaUtakmici = new List<StatistikaKartonaIgraca>();

            for(int i = 0; i < odigraneUtakmice.Count; i++)
            {
                kartoniIgracaNaUtakmici = statistikaKartonaIgraca.OdrediKartoneIgracaPoUtakmici(odigraneUtakmice[i]);
                for(int j = 0; j < kartoniIgracaNaUtakmici.Count; j++)
                {
                    statistikaKartonaKlubova.FirstOrDefault(x =>
                        x.oznakaKluba == kartoniIgracaNaUtakmici[j].oznakaKluba).brojZutihKartona += kartoniIgracaNaUtakmici[j].brojZutihKartona;
                    statistikaKartonaKlubova.FirstOrDefault(x =>
                        x.oznakaKluba == kartoniIgracaNaUtakmici[j].oznakaKluba).brojDrugihZutihKartona += kartoniIgracaNaUtakmici[j].brojDrugihZutihKartona;
                    statistikaKartonaKlubova.FirstOrDefault(x =>
                        x.oznakaKluba == kartoniIgracaNaUtakmici[j].oznakaKluba).brojCrvenihKartona += kartoniIgracaNaUtakmici[j].brojCrvenihKartona;
                }
            }

            return statistikaKartonaKlubova;
        }

        private List<StatistikaKartonaKlubova> PostaviKluboveZaStatistiku(List<NogometniKlub> nogometniKlubovi)
        {
            List<StatistikaKartonaKlubova> kluboviZaStatistiku = new List<StatistikaKartonaKlubova>();
            for(int i = 0; i < nogometniKlubovi.Count; i++)
            {
                StatistikaKartonaKlubova statistikaKartonaKlubova = new StatistikaKartonaKlubova();
                statistikaKartonaKlubova.oznakaKluba = nogometniKlubovi[i].oznakaKluba;
                statistikaKartonaKlubova.nazivKluba = nogometniKlubovi[i].nazivKluba;
                statistikaKartonaKlubova.brojZutihKartona = 0;
                statistikaKartonaKlubova.brojDrugihZutihKartona = 0;
                statistikaKartonaKlubova.brojCrvenihKartona = 0;
                kluboviZaStatistiku.Add(statistikaKartonaKlubova);
            }

            return kluboviZaStatistiku;
        }
    }

    class StatistikaKartonaIgraca : StatistikaZapis
    {
        public string imeIgraca { get; set; }
        public int brojZutihKartona { get; set; }
        public int brojDrugihZutihKartona { get; set; }
        public int brojCrvenihKartona { get; set; }

        public List<StatistikaKartonaIgraca> OdrediKartoneIgracaPoUtakmici(Utakmica utakmica)
        {
            List<StatistikaKartonaIgraca> statistikaKartonaIgraca = new List<StatistikaKartonaIgraca>();

            for(int i = 0; i<utakmica.dogadajiUtakmice.Count; i++)
            {
                if (!(IgracUSastavuTimu(utakmica, utakmica.dogadajiUtakmice[i].klubDogadaja, utakmica.dogadajiUtakmice[i].imeIgraca) ||
                IgracBioZamjena(utakmica, utakmica.dogadajiUtakmice[i].klubDogadaja, utakmica.dogadajiUtakmice[i].imeIgraca)))
                {
                    continue;
                }
                if (utakmica.dogadajiUtakmice[i].vrstaDogadaja == "10")
                {
                    if(statistikaKartonaIgraca.Exists(x => x.oznakaKluba == utakmica.dogadajiUtakmice[i].klubDogadaja 
                        && x.imeIgraca == utakmica.dogadajiUtakmice[i].imeIgraca))
                    {
                        statistikaKartonaIgraca.FirstOrDefault(x => x.oznakaKluba == utakmica.dogadajiUtakmice[i].klubDogadaja
                        && x.imeIgraca == utakmica.dogadajiUtakmice[i].imeIgraca).brojDrugihZutihKartona++;
                    }
                    else
                    {
                        StatistikaKartonaIgraca kartoniIgraca = new StatistikaKartonaIgraca();
                        kartoniIgraca.oznakaKluba = utakmica.dogadajiUtakmice[i].klubDogadaja;
                        kartoniIgraca.imeIgraca = utakmica.dogadajiUtakmice[i].imeIgraca;
                        kartoniIgraca.brojZutihKartona = 1;
                        kartoniIgraca.brojDrugihZutihKartona = 0;
                        kartoniIgraca.brojCrvenihKartona = 0;
                        statistikaKartonaIgraca.Add(kartoniIgraca);
                    }
                }
                else if(utakmica.dogadajiUtakmice[i].vrstaDogadaja == "11")
                {
                    StatistikaKartonaIgraca kartoniIgraca = new StatistikaKartonaIgraca();
                    kartoniIgraca.oznakaKluba = utakmica.dogadajiUtakmice[i].klubDogadaja;
                    kartoniIgraca.imeIgraca = utakmica.dogadajiUtakmice[i].imeIgraca;
                    kartoniIgraca.brojZutihKartona = 0;
                    kartoniIgraca.brojDrugihZutihKartona = 0;
                    kartoniIgraca.brojCrvenihKartona = 1;
                   
                }
            }
            return statistikaKartonaIgraca;
        }
    }

    class StatistikaLjestvice : StatistikaZapis
    {
        public int brojOdigranihKola { get; set; }
        public int brojPobjeda { get; set; }
        public int brojNerješenihUtakcmica { get; set; }
        public int brojPoraza { get; set; }
        public int brojDanihGolova { get; set; }
        public int brojPrimljenihGolova { get; set; }
        public int brojBodova { get; set; }

        public List<StatistikaLjestvice> DajLjestvicuKlubova(List<NogometniKlub> nogometniKlubovi, List<Utakmica> odigraneUtakmice)
        {
            List<StatistikaLjestvice> statistikaLjestvice = PostaviNogometneKluboveULjestvicu(nogometniKlubovi);
            StatistikaRezultataUtakmica statistikaRezultataUtakmica = new StatistikaRezultataUtakmica();

            for(int i = 0; i < odigraneUtakmice.Count; i++)
            {
                statistikaRezultataUtakmica = statistikaRezultataUtakmica.OdrediRezultatUtakmice(nogometniKlubovi, odigraneUtakmice[i]);
                //Domacin
                statistikaLjestvice.FirstOrDefault(x => x.oznakaKluba == odigraneUtakmice[i].oznakaDomacina).brojOdigranihKola++;
                statistikaLjestvice.FirstOrDefault(x => x.oznakaKluba == odigraneUtakmice[i].oznakaDomacina).brojDanihGolova += statistikaRezultataUtakmica.goloviDomacegTime;
                statistikaLjestvice.FirstOrDefault(x => x.oznakaKluba == odigraneUtakmice[i].oznakaDomacina).brojPrimljenihGolova += statistikaRezultataUtakmica.goloviGostujucegTime;

                //Gost
                statistikaLjestvice.FirstOrDefault(x => x.oznakaKluba == odigraneUtakmice[i].oznakaGosta).brojOdigranihKola++;
                statistikaLjestvice.FirstOrDefault(x => x.oznakaKluba == odigraneUtakmice[i].oznakaGosta).brojDanihGolova += statistikaRezultataUtakmica.goloviGostujucegTime;
                statistikaLjestvice.FirstOrDefault(x => x.oznakaKluba == odigraneUtakmice[i].oznakaGosta).brojPrimljenihGolova += statistikaRezultataUtakmica.goloviDomacegTime;

                if(statistikaRezultataUtakmica.goloviDomacegTime > statistikaRezultataUtakmica.goloviGostujucegTime)
                {
                    //Domacin
                    statistikaLjestvice.FirstOrDefault(x => x.oznakaKluba == odigraneUtakmice[i].oznakaDomacina).brojBodova += 3;
                    statistikaLjestvice.FirstOrDefault(x => x.oznakaKluba == odigraneUtakmice[i].oznakaDomacina).brojPobjeda++;
                    //Gost
                    statistikaLjestvice.FirstOrDefault(x => x.oznakaKluba == odigraneUtakmice[i].oznakaGosta).brojPoraza++;
                }
                else if(statistikaRezultataUtakmica.goloviDomacegTime < statistikaRezultataUtakmica.goloviGostujucegTime)
                {
                    //Domacin
                    statistikaLjestvice.FirstOrDefault(x => x.oznakaKluba == odigraneUtakmice[i].oznakaDomacina).brojPoraza++;

                    //Gost
                    statistikaLjestvice.FirstOrDefault(x => x.oznakaKluba == odigraneUtakmice[i].oznakaGosta).brojBodova += 3;
                    statistikaLjestvice.FirstOrDefault(x => x.oznakaKluba == odigraneUtakmice[i].oznakaGosta).brojPobjeda++;
                }
                else
                {
                    //Domacin
                    statistikaLjestvice.FirstOrDefault(x => x.oznakaKluba == odigraneUtakmice[i].oznakaDomacina).brojNerješenihUtakcmica++;
                    statistikaLjestvice.FirstOrDefault(x => x.oznakaKluba == odigraneUtakmice[i].oznakaDomacina).brojBodova++;

                    //Gost
                    statistikaLjestvice.FirstOrDefault(x => x.oznakaKluba == odigraneUtakmice[i].oznakaGosta).brojNerješenihUtakcmica++;
                    statistikaLjestvice.FirstOrDefault(x => x.oznakaKluba == odigraneUtakmice[i].oznakaGosta).brojBodova++;
                }
            }

            return statistikaLjestvice.OrderByDescending(x => x.brojBodova).ToList();
        }

        private List<StatistikaLjestvice> PostaviNogometneKluboveULjestvicu(List<NogometniKlub> nogometniKlubovi)
        {
            List<StatistikaLjestvice> statistikaLjestvice = new List<StatistikaLjestvice>();

            for(int i = 0; i < nogometniKlubovi.Count; i++)
            {
                StatistikaLjestvice statistika = new StatistikaLjestvice();
                statistika.oznakaKluba = nogometniKlubovi[i].oznakaKluba;
                statistika.nazivKluba = nogometniKlubovi[i].nazivKluba;
                statistika.brojOdigranihKola = 0;
                statistika.brojPobjeda = 0;
                statistika.brojNerješenihUtakcmica = 0;
                statistika.brojPoraza = 0;
                statistika.brojDanihGolova = 0;
                statistika.brojBodova = 0;

                statistikaLjestvice.Add(statistika);
            }

            return statistikaLjestvice;
        }
    }

    class StatistikaRezultataUtakmica : StatistikaZapis
    {
        public int brojKola { get; set; }
        public string vrijemeUtakmice { get; set; }
        public string oznakaGostujucegTima { get; set; }
        public string imeGostujucegTima { get; set; }
        public int goloviDomacegTime { get; set; }
        public int goloviGostujucegTime { get; set; }

        public StatistikaRezultataUtakmica OdrediRezultatUtakmice(List<NogometniKlub> nogometniKlubovi, Utakmica utakmica)
        {
            StatistikaRezultataUtakmica statistikaRezultataUtakmica = new StatistikaRezultataUtakmica();

            statistikaRezultataUtakmica.brojKola = utakmica.brojKola;
            statistikaRezultataUtakmica.vrijemeUtakmice = utakmica.datumUtakmice;
            statistikaRezultataUtakmica.oznakaKluba = utakmica.oznakaDomacina;
            statistikaRezultataUtakmica.nazivKluba = nogometniKlubovi.FirstOrDefault(x => x.oznakaKluba == utakmica.oznakaDomacina).nazivKluba;
            statistikaRezultataUtakmica.oznakaGostujucegTima = utakmica.oznakaGosta;
            statistikaRezultataUtakmica.imeGostujucegTima = nogometniKlubovi.FirstOrDefault(x => x.oznakaKluba == utakmica.oznakaGosta).nazivKluba;
            statistikaRezultataUtakmica.goloviDomacegTime = 0;
            statistikaRezultataUtakmica.goloviGostujucegTime = 0;

            for(int i = 0; i < utakmica.dogadajiUtakmice.Count; i++)
            {
                if (!(IgracUSastavuTimu(utakmica, utakmica.dogadajiUtakmice[i].klubDogadaja, utakmica.dogadajiUtakmice[i].imeIgraca) ||
                IgracBioZamjena(utakmica, utakmica.dogadajiUtakmice[i].klubDogadaja, utakmica.dogadajiUtakmice[i].imeIgraca)))
                {
                    continue;
                }
                if (utakmica.dogadajiUtakmice[i].vrstaDogadaja == "1" || utakmica.dogadajiUtakmice[i].vrstaDogadaja == "2")
                {
                    if(utakmica.dogadajiUtakmice[i].klubDogadaja == statistikaRezultataUtakmica.oznakaKluba)
                    {
                        statistikaRezultataUtakmica.goloviDomacegTime++;
                    }
                    else
                    {
                        statistikaRezultataUtakmica.goloviGostujucegTime++;
                    }
                }
                else if(utakmica.dogadajiUtakmice[i].vrstaDogadaja == "3")
                {
                    if(utakmica.dogadajiUtakmice[i].klubDogadaja == statistikaRezultataUtakmica.oznakaKluba)
                    {
                        statistikaRezultataUtakmica.goloviGostujucegTime++;
                    }
                    else
                    {
                        statistikaRezultataUtakmica.goloviGostujucegTime++;
                    }
                }
            }
            return statistikaRezultataUtakmica;
        }
        public List<StatistikaRezultataUtakmica> OdrediRezultateSvihUtakmicaZaJedanKlub(List<NogometniKlub> nogometniKlubovi, List<Utakmica> utakmice)
        {
            List<StatistikaRezultataUtakmica> statistikaRezultataUtakmica = new List<StatistikaRezultataUtakmica>();

            for (int i = 0; i < utakmice.Count; i++)
            {
                statistikaRezultataUtakmica.Add(
                    OdrediRezultatUtakmice(nogometniKlubovi, utakmice[i])
                    );
            }

            return statistikaRezultataUtakmica;
        }
    }
}
