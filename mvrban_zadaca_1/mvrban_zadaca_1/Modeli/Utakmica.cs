using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mvrban_zadaca_1.Modeli
{
    class Utakmica
    {
        public int brojUtakmice { get; set; }
        public int brojKola { get; set; }
        public string oznakaDomacina { get; set; }
        public string oznakaGosta { get; set; }
        public string datumUtakmice { get; set; }
        public List<Dogadaj> dogadajiUtakmice { get; set; }
        public List<ClanSastava> sastaviTimova { get; set; }
    }
}
