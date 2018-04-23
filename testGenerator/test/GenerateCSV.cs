using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace test
{
    public class GenerateCSV
    {
        /// <summary>
        /// Generowanie klucza odpowiedzi do pliku *.csv, aby potem wczytac ten plik do bazy danych
        /// </summary>
        /// <param name="listaPytan">lista obiektow Pytanie, wczytana z Excela</param>
        /// <param name="idTestu">idTestu zwrocone z bazy danych</param>
        /// <param name="testName">nazwa testu podawana przez uzytkownika</param>
        public static void Generate(List<Pytanie> listaPytan, int idTestu, string testName)
        {
            // stworzenie pliku o zadanej nazwie, testName jest od uzytkownika, a idTestu jest zwracane z bazy danych
            StreamWriter writer = new StreamWriter("Key_" + idTestu + ".csv");

            // pierwsze dwie kolumny to IdTestu i NazwaTestu
            writer.Write(idTestu.ToString());
            writer.Write(";");      // wpisanie ; o=powoduje przejście do komórki po prawej stronie
            writer.Write(testName.ToString());
            writer.Write(";");      // wpisanie ; o=powoduje przejście do komórki po prawej stronie

            // kolejne kolumny to sekwencje odpowiedzi wczytane z Excela z lista pytan
            for (int i = 0; i < listaPytan.Count; i++)
            {
//writer.Write(listaPytan[i].GetSekwencjaOdpowiedzi().ToString());
                writer.Write(";");  // wpisanie ; o=powoduje przejście do komórki po prawej stronie
            }
            writer.Close();         // zamyka i zapisuje plik CSV (wypelnia go danymi [jesli go nie bedzie utworzony plik bedzie pusty])

            // Open the document automatically - przy tworzeniu funkcji, do podgladu czy dobrze dziala
            //System.Diagnostics.Process.Start(@testName.ToString() + '_' + idTestu + ".csv");



            //......... TO PONIZEJ NIE JEST JUZ MI POTRZEBNE .............//

            /*
            //StringBuilder csvcontent = new StringBuilder();
            //csvcontent.AppendLine("Name;Age");      // ; - oddziela kolumny
            //csvcontent.AppendLine("Mahesh;31");
            //string csvpath = "D:\\xyz.csv";
            ////File.AppendAllText(csvpath, csvcontent.ToString());

            //List<string> teksty = new List<string>();
            //teksty.Add("1blabla");
            //teksty.Add("2costam");
            //teksty.Add("3dupa jasiu karuzela");

            //string wiersz = "";

            //for (int i = 0; i < teksty.Count; i++)
            //{
            //    wiersz += teksty[i];
            //    wiersz += ';';
            //}

            //string csvpath = "D:\\Key.csv";
            string csvpath = "D:\\Key" + idTestu + ".csv";

            string sekwencjaOdpowiedzi = "";

            for (int i = 0; i < listaPytan.Count; i++)
            {
                sekwencjaOdpowiedzi += listaPytan[i].GetSekwencjaOdpowiedzi().ToString();
                sekwencjaOdpowiedzi += ';';
            }

            string cos = "";

            //File.WriteAllText(csvpath, sekwencjaOdpowiedzi.ToString());

            File.AppendAllText(csvpath, sekwencjaOdpowiedzi.ToString());
            
            // Open the document automatically
            //System.Diagnostics.Process.Start(@"D:\\xyz.csv");
            */
        }
    }
}