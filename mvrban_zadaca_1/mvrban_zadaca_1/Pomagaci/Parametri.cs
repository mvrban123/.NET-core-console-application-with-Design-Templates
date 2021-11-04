using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mvrban_zadaca_1.Pomagaci
{
    class Parametri
    {
        private static string[] parametri;
        public static bool ProvjeraUlaznihParametara(string[] ulazniParametri)
        {
            parametri = ulazniParametri;
            if(ulazniParametri.Contains("-k") 
                && ulazniParametri.Contains("-i")
                && ulazniParametri.Contains("-u")
                && ulazniParametri.Contains("-s")
                && ulazniParametri.Contains("-d"))
            {
                return true;
            }
            return false;
        }

        public static string DajParametarDatotekeKlubova()
        {
            return parametri[Array.IndexOf(parametri, "-k") + 1];
        }

        public static string DajParametarDatotekeIgraca()
        {
            return parametri[Array.IndexOf(parametri, "-i") + 1];
        }

        public static string DajParametarDatotekeUtakmica()
        {
            return parametri[Array.IndexOf(parametri, "-u") + 1];
        }

        public static string DajParametarDatotekeSastava()
        {
            return parametri[Array.IndexOf(parametri, "-s") + 1];
        }

        public static string DajParametarDatotekeDogadaja()
        {
            return parametri[Array.IndexOf(parametri, "-d") + 1];
        }
    }
}
