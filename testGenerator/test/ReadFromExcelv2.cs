using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Excel = Microsoft.Office.Interop.Excel;       //microsoft Excel 14 object in references-> COM tab
using System.Threading;

namespace test
{
    public class ReadFromExcelv2
    {
        //public static void Read()
        internal static List<Pytanie> Read(string lokalizacjaExcela)
        {
            List<Pytanie> listaPytan = new List<Pytanie>();               // lista obiektów Pytanie (treść, odpowiedzi i inne)

            //Create COM Objects. Create a COM object for everything that is referenced
            Excel.Application xlApp = new Excel.Application();

            // lokalizacja pliku z baza pytan
            Excel.Workbook xlWorkbook = xlApp.Workbooks.Open(lokalizacjaExcela.ToString());

            Excel._Worksheet xlWorksheet = xlWorkbook.Sheets[1];
            Excel.Range xlRange = xlWorksheet.UsedRange;

            int rowCount = xlRange.Rows.Count;
            //int colCount = xlRange.Columns.Count;

            //iterate over the rows and columns and print to the console as it appears in the file
            //excel is not zero based!!

            // zmienne do wczytywania komorek z excela a nastepnie do uzycia konstruktrora Pytanie() i dodania go do listy obiektow Pytanie
            string readTresc = "";
            string readOdp = "";
            string readCzyPoprawna = "";


            //////////////////////////////////////////////////////////////// dotąd jest dobrze

            int aktualnyNumerPytania = 0;
            for (int i = 2; i <= rowCount; i++) // iteracja po wierszach excela, od 2 bo pierwszy wiersz to nazwy kolumn
            {
                int aktualnyNrKolumny = 2;              // numer kolumny z ktorej wczytujemy pytanie, od niej zaczynamy czytac komurki w wierszach, od 2 bo kolumna numer pytania nie jest potrzbena
                int colCount = xlRange.Columns.Count;   // liczba zapisanych kolumn Excelu
                int liczbaOdpowiedzi = (colCount - 2) / 2;      // sprawdzam ile jest odpowiedzi w excelu i tyle razy dodaje rozne odpowiedzi
                
                for (int j = 2; j <= (colCount - liczbaOdpowiedzi); j++)     // iteracja po kolumnach excela, od 2 bo kolumny numer pytanie nie wczytujemy
                                                                             // colCount - liczbaOdpowiedzi bo iterujemy tylko tyle razy ile jest (wszystkich odpowiedzi + tresc pytania)
                {
                    if (aktualnyNrKolumny % colCount == 2 && xlRange.Cells[i, j] != null && xlRange.Cells[i, j].Value2 != null)
                    {
                        readTresc = xlRange.Cells[i, j].Value2.ToString();
                        listaPytan.Add(new Pytanie(readTresc));
                        aktualnyNrKolumny++;
                    }
                    else if (xlRange.Cells[i, j] != null && xlRange.Cells[i, j].Value2 != null)
                    {
                        readOdp = xlRange.Cells[i, j].Value2.ToString();
                        readCzyPoprawna = xlRange.Cells[i, j+liczbaOdpowiedzi].Value2.ToString();
                        Odpowiedz odp = new Odpowiedz(readOdp, readCzyPoprawna);
                        listaPytan[aktualnyNumerPytania].listaOdpowiedzi.Add(odp);
                        aktualnyNrKolumny ++;
                    }
                }
                aktualnyNumerPytania++;

                // wywolanie kontstruktora Pytanie() aby wpisal wczytany wiersz z Excela do do obiektu Pytanie
                //listaPytan.Add(new Pytanie(readNrPytania, readTresc, readA, readB, readC, readD, readSekwencjaOdpowiedzi));

                //Console.WriteLine(readNrPytania.ToString() + readTresc + readA + readB + readC + readD + readSekwencjaOdpowiedzi);
                //listaPytan[aktualnePytanie].WyswietlPytanie();
            }

            //cleanup
            GC.Collect();
            GC.WaitForPendingFinalizers();

            //rule of thumb for releasing com objects:
            //  never use two dots, all COM objects must be referenced and released individually
            //  ex: [somthing].[something].[something] is bad

            //release com objects to fully kill excel process from running in the background
            Marshal.ReleaseComObject(xlRange);
            Marshal.ReleaseComObject(xlWorksheet);

            //close and release
            xlWorkbook.Close();
            Marshal.ReleaseComObject(xlWorkbook);

            //quit and release
            xlApp.Quit();
            Marshal.ReleaseComObject(xlApp);

            return listaPytan;
        }
    }
}