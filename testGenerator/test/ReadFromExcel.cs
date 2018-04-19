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
    public class ReadFromExcel
    {
        //public static void Read()
        internal static List<Pytanie> Read(string lokalizacjaExcela)
        {
            List<Pytanie> listaPytan = new List<Pytanie>();               // lista obiektów Pytanie (treść, odpowiedzi i inne)

            //Create COM Objects. Create a COM object for everything that is referenced
            Excel.Application xlApp = new Excel.Application();

            // !!!!!!!!!!!!!!!! tu zmienic lokalizacje pliku tak zeby wystarczylo sama nazwe pliku podac i nie trzeba bylo podawac lokalizacji!!!!!!!!!!!!!!!!!
            //Excel.Workbook xlWorkbook = xlApp.Workbooks.Open(@"zestawPytań2.xlsx");
            //Excel.Workbook xlWorkbook = xlApp.Workbooks.Open(@"C:\Users\kubam\Downloads\zestawPytań2.xlsx");
            Excel.Workbook xlWorkbook = xlApp.Workbooks.Open(lokalizacjaExcela.ToString());
            
            Excel._Worksheet xlWorksheet = xlWorkbook.Sheets[1];
            Excel.Range xlRange = xlWorksheet.UsedRange;

            int rowCount = xlRange.Rows.Count;
            int colCount = xlRange.Columns.Count;

            //iterate over the rows and columns and print to the console as it appears in the file
            //excel is not zero based!!

            // zmienne do wczytywania komorek z excela a nastepnie do uzycia konstruktrora Pytanie() i dodania go do listy obiektow Pytanie
            int readNrPytania = 0;
            string readTresc = "";
            string readA = "";
            string readB = "";
            string readC = "";
            string readD = "";
            string readSekwencjaOdpowiedzi = "";

            for (int i = 2; i <= rowCount; i++) // iteracja po wierszach excela, od 2 bo pierwszy wiersz to nazwy kolumn
            {
                int aktualnyNrKolumny = 1;      // numer kolumny z ktorej wczytujemy pytanie, od niej zaczynamy czytac komurki w wierszach

                for (int j = 1; j <= colCount; j++) // iteracja po kolumnach excela
                {
                    if (aktualnyNrKolumny % 7 == 1 && xlRange.Cells[i, j] != null && xlRange.Cells[i, j].Value2 != null)
                    {
                        bool result = int.TryParse(xlRange.Cells[i, j].Value.ToString(), out readNrPytania);
                        //readNrPytania = xlRange.Cells[i, j].Value.ToInt());
                        aktualnyNrKolumny++;
                    }
                    else if (aktualnyNrKolumny % 7 == 2 && xlRange.Cells[i, j] != null && xlRange.Cells[i, j].Value2 != null)
                    {
                        readTresc = xlRange.Cells[i, j].Value2.ToString();
                        aktualnyNrKolumny++;
                    }
                    else if (aktualnyNrKolumny % 7 == 3 && xlRange.Cells[i, j] != null && xlRange.Cells[i, j].Value2 != null)
                    {
                        readA = xlRange.Cells[i, j].Value2.ToString();
                        aktualnyNrKolumny++;
                    }
                    else if (aktualnyNrKolumny % 7 == 4 && xlRange.Cells[i, j] != null && xlRange.Cells[i, j].Value2 != null)
                    {
                        readB = xlRange.Cells[i, j].Value2.ToString();
                        aktualnyNrKolumny++;
                    }
                    else if (aktualnyNrKolumny % 7 == 5 && xlRange.Cells[i, j] != null && xlRange.Cells[i, j].Value2 != null)
                    {
                        readC = xlRange.Cells[i, j].Value2.ToString();
                        aktualnyNrKolumny++;
                    }
                    else if (aktualnyNrKolumny % 7 == 6 && xlRange.Cells[i, j] != null && xlRange.Cells[i, j].Value2 != null)
                    {
                        readD = xlRange.Cells[i, j].Value2.ToString();
                        aktualnyNrKolumny++;
                    }
                    else if (aktualnyNrKolumny % 7 == 0 && xlRange.Cells[i, j] != null && xlRange.Cells[i, j].Value2 != null)
                    {
                        readSekwencjaOdpowiedzi = xlRange.Cells[i, j].Value2.ToString();
                        aktualnyNrKolumny = 1;
                    }
                }
                // wywolanie kontstruktora Pytanie() aby wpisal wczytany wiersz z Excedo do obiektu Pytanie
                listaPytan.Add(new Pytanie(readNrPytania, readTresc, readA, readB, readC, readD, readSekwencjaOdpowiedzi));

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