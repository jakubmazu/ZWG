using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace test
{
    class LosowanieBezPowtorzen
    {
        /// <summary>
        /// Losowanie bez powtorzen
        /// </summary>
        /// <param name="n">zakres liczb n (Liczby od 0 do n-1)</param>
        /// <param name="k">Ile liczb (z "n") wylosowac</param>
        /// <returns></returns>
        public static int[] Losowanie(int n, int k)
        {
            Random rand = new Random();
            // wypełnianie tablicy liczbami 1,2...n-1
            int[] numbers = new int[n];
            for (int i = 0; i < n; i++)
                numbers[i] = i;

            int[] wylosowane = new int[n];

            // losowanie k liczb
            for (int i = 0; i < k; i++)
            {
                // tworzenie losowego indeksu pomiędzy 0 i n - 1
                int r = rand.Next(n);

                wylosowane[i] = numbers[r];

                // wybieramy element z losowego miejsca
                //Console.WriteLine(numbers[r]);

                // przeniesienia ostatniego elementu do miejsca z którego wzięliśmy
                numbers[r] = numbers[n-1];
                n--;
            }
            return wylosowane;
        }
    }
}
