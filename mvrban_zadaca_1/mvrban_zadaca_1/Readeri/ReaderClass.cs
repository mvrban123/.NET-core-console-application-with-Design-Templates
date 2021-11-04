using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace mvrban_zadaca_1.Readeri
{
    class ReaderClass
    {
        private string[] zapisDatoteke { get; set; }

        public ReaderClass(string nazivDatoteke)
        {
            ProcitajZapisDatoteke(nazivDatoteke);
        }

        public void ProcitajZapisDatoteke(string nazivDatoteke)
        {
            zapisDatoteke = File.ReadAllLines(nazivDatoteke);
        }

        public int DajBrojZapisa()
        {
            return zapisDatoteke.Length;
        }

        public string[] FormatirajRedakZapisa(int brojZapisa)
        {
            return zapisDatoteke[brojZapisa].Split(';');
        }

        public string[] DajRedakZapisa(int brojZapisa)
        {
            return FormatirajRedakZapisa(brojZapisa);
        }
    }
}
