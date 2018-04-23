using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace test
{
    public class Odpowiedz
    {
        public string trescOdpowiedz;
        public string czyPoprawna;

        public Odpowiedz(string odp, string czyTrue)
        {
            trescOdpowiedz = odp;
            czyPoprawna = czyTrue;
        }

        // settery
        public void SetTrescOdpowiedz(string odp)
        {
            trescOdpowiedz = odp;
        }

        public void SetCzyPoprawna(string czyTrue)
        {
            czyPoprawna = czyTrue;
        }

        // gettery

        public string GetTrescOdpowiedz()
        {
            return trescOdpowiedz;
        }

        public string GetCzyPoprawna()
        {
            return czyPoprawna;
        }
    }
}
