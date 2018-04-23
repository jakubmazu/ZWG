using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace test
{
    public class Pytanie //: Odpowiedz
    {
        //public int NrPytania;     // nr pytania
        public string Tresc;       // tresc pytania
        public List<Odpowiedz> listaOdpowiedzi = new List<Odpowiedz>();

        //public string A, B, C, D;  // odpowiedzi a, b, c, d        
        //public string SekwencjaOdpowiedzi;    // tu znajduje sie poprawna odpowiedz wczytana z pliku *.txt

        //public List<string> odp = new List<string>();
        // odp.add(strin odop);

        //konstruktory

        public Pytanie(string tre)//, Odpowiedz(string trescOdpowiedzi, bool poprawnosc))
        {
            Tresc = tre;
        }

        //public Pytanie(int nr, string tre, string odpA, string odpB, string odpC, string odpD)//, string odpowiedzi)
        //{
        //    NrPytania = nr;
        //    Tresc = tre;
        //    A = odpA;
        //    B = odpB;
        //    C = odpC;
        //    D = odpD;
        //}

        //metody
        /// <summary>
        /// Wyswietla dane pytanie
        /// </summary>
        //public void WyswietlPytanie()
        //{
        //    Console.WriteLine(GetNrPytania() + " " + GetTresc() + ":");
        //    Console.WriteLine("A. " + GetA());
        //    Console.WriteLine("B. " + GetB());
        //    Console.WriteLine("C. " + GetC());
        //    Console.WriteLine("D. " + GetD());
        //    Console.WriteLine("Sekwencja odpowiedzi: " + SekwencjaOdpowiedzi);
        //}

        // settery
        //public void SetNrPytania(int nr)
        //{
        //    NrPytania = nr;
        //}

        public void SetTresc(string tre)
        {
            Tresc = tre;
        }

        //public void SetA(string a)
        //{
        //    A = a;
        //}

        //public void SetB(string b)
        //{
        //    B = b;
        //}

        //public void SetC(string c)
        //{
        //    C = c;
        //}

        //public void SetD(string d)
        //{
        //    D = d;
        //}

        //public void SetSekwencjaOdpowiedzi(string odpowiedzi)
        //{
        //    SekwencjaOdpowiedzi = odpowiedzi;
        //}

        // gettery
        //public int GetNrPytania()
        //{
        //    return NrPytania;
        //}

        public string GetTresc()
        {
            return Tresc;
        }

        //public string GetA()
        //{
        //    return A;
        //}

        //public string GetB()
        //{
        //    return B;
        //}

        //public string GetC()
        //{
        //    return C;
        //}

        //public string GetD()
        //{
        //    return D;
        //}

        //public string GetSekwencjaOdpowiedzi()
        //{
        //    return SekwencjaOdpowiedzi;
        //}

    }
}