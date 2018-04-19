using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using iTextSharp.text.pdf;
using iTextSharp.text;
using System.IO;

namespace test
{
    public class ExportToPdf
    {
        /// <summary>
        /// Generowanie testu *.pdf z pytan wczytanych z Excela
        /// </summary>
        /// <param name="listaPytan">Lista obiektow typu Pytanie wczytana z Excela</param>
        /// <param name="id">id jest wartoscia z bazy danych</param>
        /// <param name="tytulTestu">z czego jest ten test</param>
        /// <param name="nazwaTestu">skor dla id testu</param>
        /// <param name="liczbyBezPowtarzania">losowe liczby bez powtorzen zeby pytania byly w losowej kolejnosci</param>
        /// <param name="mniejszaIloscPytan">ilosc pytan jaka ma byc wypisana</param>
        public static void GenerateTest(List<Pytanie> listaPytan, int id, string tytulTestu, string nazwaTestu, int[] liczbyBezPowtarzania, int mniejszaIloscPytan)
        {
            // moje proby i wypociny
            Document doc = new Document(PageSize.A4);

            BaseFont arial = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
            iTextSharp.text.Font f_15_bold = new iTextSharp.text.Font(arial, 15, iTextSharp.text.Font.BOLD);
            iTextSharp.text.Font f_12_normal = new iTextSharp.text.Font(arial, 12, iTextSharp.text.Font.NORMAL);
            iTextSharp.text.Font f_12_bold = new iTextSharp.text.Font(arial, 12, iTextSharp.text.Font.BOLD);

            FileStream os = new FileStream(nazwaTestu.ToString() + '_' + id + ".pdf", FileMode.Create);
            using (os)
            {
                PdfWriter.GetInstance(doc, os);
                doc.Open();

                // Informacje o teœcie, nazwa testu, data wygenerowania testu (prawy górny róg, w ramce)
                PdfPTable table1 = new PdfPTable(1);
                PdfPCell cel1 = new PdfPCell(new Phrase("Test: " + tytulTestu.ToString(), f_15_bold));
                PdfPCell cel2 = new PdfPCell(new Phrase("id Testu: " + nazwaTestu.ToString() + '_' + id, f_15_bold));
                PdfPCell cel3 = new PdfPCell(new Phrase("Data: " + DateTime.Now.ToShortDateString(), f_15_bold));
                cel1.HorizontalAlignment = Element.ALIGN_JUSTIFIED;
                cel2.HorizontalAlignment = Element.ALIGN_JUSTIFIED;
                cel3.HorizontalAlignment = Element.ALIGN_JUSTIFIED;
                cel1.Border = iTextSharp.text.Rectangle.NO_BORDER;
                cel2.Border = iTextSharp.text.Rectangle.NO_BORDER;
                cel3.Border = iTextSharp.text.Rectangle.NO_BORDER;
                table1.AddCell(cel1);
                table1.AddCell(cel2);
                table1.AddCell(cel3);
                table1.SpacingAfter = 20;
                table1.SpacingBefore = 10;
                // ¿ety to wszystko by³o ³adnie w ramce
                PdfPTable table2 = new PdfPTable(1);
                table2.AddCell(table1);
                table2.HorizontalAlignment = Element.ALIGN_RIGHT;
                table2.WidthPercentage = 40;         // jak tytu³ testu bêdzie za du¿y to trzeba zwiêkszyæ
                //table2.WidthPercentage = tytulTestu.Count();  // to jest nie do koñca poprawne
                //table2.WidthPercentage = 90;
                doc.Add(table2);


                Paragraph pytania = new Paragraph();
                for (int i = 0; i < mniejszaIloscPytan; i++)
                {
                    pytania.Add(new Phrase(i + 1 + ". " + listaPytan[liczbyBezPowtarzania[i] - 1].GetTresc().ToString() + "\n", f_12_bold));        // numer i tresc pytania
                    pytania.Add(new Phrase("           a: " + listaPytan[liczbyBezPowtarzania[i] - 1].GetA().ToString() + "\n", f_12_normal));   // odpowiedz A
                    pytania.Add(new Phrase("           b: " + listaPytan[liczbyBezPowtarzania[i] - 1].GetB().ToString() + "\n", f_12_normal));   // odpowiedz B
                    pytania.Add(new Phrase("           c: " + listaPytan[liczbyBezPowtarzania[i] - 1].GetC().ToString() + "\n", f_12_normal));   // odpowiedz C
                    pytania.Add(new Phrase("           d: " + listaPytan[liczbyBezPowtarzania[i] - 1].GetD().ToString() + "\n", f_12_normal));   // odpowiedz D
                }
                pytania.Alignment = Element.ALIGN_JUSTIFIED;
                doc.Add(pytania);

                doc.Close();

                // Open the document automatically
                //System.Diagnostics.Process.Start(@"Tescik.pdf");
                System.Diagnostics.Process.Start(nazwaTestu.ToString() + '_' + id + ".pdf");
            }
        }
    }
}