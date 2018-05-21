using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace test
{
    public class Pomieszaj
    {
        internal static List<Pytanie> Losowo(List<Pytanie> PwszystkiePytania, bool losowoPytania, bool losowoOdpowiedzi)
        {
            List<Pytanie> losowePytania = new List<Pytanie>();      // nowa lista, tu beda pytania poukladane losowo

            //losowanie bez powtorzen zeby pytania byly w losowej kolejnosci
            int[] liczbyBezPowtorzenPytania = new int[PwszystkiePytania.Count()];
            int[] liczbyBezPowtorzenOdpowiedzi = new int[PwszystkiePytania[0].listaOdpowiedzi.Count()];
       
            liczbyBezPowtorzenPytania = LosowanieBezPowtorzen.Losowanie(PwszystkiePytania.Count(), PwszystkiePytania.Count());

            if (losowoPytania == true)          // pomieszaj wszystkie pytania i (ewentualnie) odpowiedzi
            {
                for (int i = 0; i < PwszystkiePytania.Count(); i++)
                {
                    losowePytania.Add(PwszystkiePytania[liczbyBezPowtorzenPytania[i]]);     // przypisanie losowego pytania do nowej listy z losowymi pytaniami
                    
                    //losowePytania[i].listaOdpowiedzi.Clear();                             // usun wszysktie odpowiedzi z losowej listy pytan, zeby za chwile dopisac losowo ulozone odpowiedzi
                    if (losowoOdpowiedzi)   // pomieszaj odpowiedzi
                    {
                        // skopiuj wszystkie odpowiedzi
                        List<Odpowiedz> kopiaOdpowiedzi = new List<Odpowiedz>();            // lista zeby skopiowac wszsytkie odpowiedzi z danego pytania
                        for (int j = 0; j < PwszystkiePytania[i].listaOdpowiedzi.Count(); j++)
                        {
                            kopiaOdpowiedzi.Add(losowePytania[i].listaOdpowiedzi[j]);       // kopiujemy wszystkie odpowiedzi z każdego pytania
                        }
                        liczbyBezPowtorzenOdpowiedzi = LosowanieBezPowtorzen.Losowanie(kopiaOdpowiedzi.Count(), kopiaOdpowiedzi.Count());       // losowe liczby bez powtorzen, tyle ile jest odpowiedzi w danym pytaniu
                        for (int k = 0; k < PwszystkiePytania[i].listaOdpowiedzi.Count(); k++)
                        {
                            losowePytania[i].listaOdpowiedzi[k] = kopiaOdpowiedzi[liczbyBezPowtorzenOdpowiedzi[k]]; // przepisanie skopiowanych odpowiedzi w losowej kolejnosci
                        }
                    }
                }
            }

            if ((!losowoPytania) && (losowoOdpowiedzi))      // pomieszaj tylko odpowiedzi 
            {
                for (int i = 0; i < PwszystkiePytania.Count(); i++)
                {
                    losowePytania.Add(PwszystkiePytania[i]);     // skopiowanie pytań (bez losowego układu) do nowej listy

                    // skopiuj wszystkie odpowiedzi
                    List<Odpowiedz> kopiaOdpowiedzi = new List<Odpowiedz>();        // lista zeby skopiowac wszsytkie odpowiedzi z danego pytania
                    for (int j = 0; j < PwszystkiePytania[i].listaOdpowiedzi.Count(); j++)
                    {
                        kopiaOdpowiedzi.Add(losowePytania[i].listaOdpowiedzi[j]);   // kopiujemy wszystkie odpowiedzi z każdego pytania
                    }

                    liczbyBezPowtorzenOdpowiedzi = LosowanieBezPowtorzen.Losowanie(kopiaOdpowiedzi.Count(), kopiaOdpowiedzi.Count());       // losowe liczby bez powtorzen, tyle ile jest odpowiedzi w danym pytaniu
                    for (int k = 0; k < PwszystkiePytania[i].listaOdpowiedzi.Count(); k++)
                    {
                        losowePytania[i].listaOdpowiedzi[k] = kopiaOdpowiedzi[liczbyBezPowtorzenOdpowiedzi[k]]; // przepisanie skopiowanych odpowiedzi w losowej kolejnosci
                    }
                }
            }
            return losowePytania;
        }
    }
}
