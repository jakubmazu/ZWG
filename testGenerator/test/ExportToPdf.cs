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
        public static void GenerateTest(List<Pytanie> listaPytan, int id, string tytulTestu)
        {
            // moje proby i wypociny
            Document doc = new Document(PageSize.A4);

            BaseFont arial = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
            iTextSharp.text.Font f_15_bold = new iTextSharp.text.Font(arial, 15, iTextSharp.text.Font.BOLD);
            iTextSharp.text.Font f_12_normal = new iTextSharp.text.Font(arial, 12, iTextSharp.text.Font.NORMAL);
            iTextSharp.text.Font f_12_bold = new iTextSharp.text.Font(arial, 12, iTextSharp.text.Font.BOLD);

            FileStream os = new FileStream("Tescik.pdf", FileMode.Create);
            using (os)
            {
                PdfWriter.GetInstance(doc, os);
                doc.Open();

                //Paragraph paragraph = new Paragraph()
                //{
                //    new Phrase("1 Pytanie: " + "Numero uno" + "\n", f_12_bold),
                //    new Phrase("           " + "A. pierwsza odpowiedx" + "\n", f_12_normal),
                //    new Phrase("           " + "B. druga odpo" + "\n", f_12_normal),
                //    new Phrase("           " + "C. trzecia odpowiedx" + "\n", f_12_normal),
                //    new Phrase("           " + "D. i ostatnia odpowiedx" + "\n", f_12_normal),
                //};
                //paragraph.Alignment = Element.ALIGN_JUSTIFIED;
                //doc.Add(paragraph);

                Paragraph pytania = new Paragraph();
                for (int i = 0; i < listaPytan.Count; i++)
                {
                    pytania.Add(new Phrase(i + 1 + ". " + listaPytan[i].GetTresc().ToString() + "\n", f_12_bold));        // numer i tresc pytania
                    pytania.Add(new Phrase("           a: " + listaPytan[i].GetA().ToString() + "\n", f_12_normal));   // odpowiedz A
                    pytania.Add(new Phrase("           b: " + listaPytan[i].GetB().ToString() + "\n", f_12_normal));   // odpowiedz B
                    pytania.Add(new Phrase("           c: " + listaPytan[i].GetC().ToString() + "\n", f_12_normal));   // odpowiedz C
                    pytania.Add(new Phrase("           d: " + listaPytan[i].GetB().ToString() + "\n", f_12_normal));   // odpowiedz D
                }
                pytania.Alignment = Element.ALIGN_JUSTIFIED;
                doc.Add(pytania);

                doc.Close();

                // Open the document automatically
                System.Diagnostics.Process.Start(@"Tescik.pdf");
            }
        }

        public static void GeneratePDF(List<Pytanie> listaPytan)
        {
            Document doc = new Document(PageSize.A4.Rotate());

            BaseFont arial = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
            iTextSharp.text.Font f_15_bold = new iTextSharp.text.Font(arial, 15, iTextSharp.text.Font.BOLD);
            iTextSharp.text.Font f_12_normal = new iTextSharp.text.Font(arial, 12, iTextSharp.text.Font.NORMAL);

            Random rnd = new Random();
            int name = rnd.Next(1, 1000);
            FileStream os = new FileStream("facture" + name.ToString() + ".pdf", FileMode.Create);

            using (os)
            {
                PdfWriter.GetInstance(doc, os);
                doc.Open();

                // information about company
                PdfPTable table1 = new PdfPTable(1);
                float[] width = new float[] { 40f, 60f };

                PdfPCell cel1 = new PdfPCell(new Phrase("\n\nVAMONET SARL", f_15_bold));
                PdfPCell cel2 = new PdfPCell(new Phrase("Developpement des Solutions Informatiques", f_15_bold));
                PdfPCell cel3 = new PdfPCell(new Phrase("Securite Electronique", f_15_bold));
                PdfPCell cel4 = new PdfPCell(new Phrase("\nTotentuan Maroc \nTelephone Portable: (+212)6 12 12 12 12\nTelephone Fixe: (+212)5 39 99 99 99", f_12_normal));

                cel1.Border = iTextSharp.text.Rectangle.NO_BORDER;
                cel2.Border = iTextSharp.text.Rectangle.NO_BORDER;
                cel3.Border = iTextSharp.text.Rectangle.NO_BORDER;
                cel4.Border = iTextSharp.text.Rectangle.NO_BORDER;

                cel1.HorizontalAlignment = Element.ALIGN_CENTER;
                cel2.HorizontalAlignment = Element.ALIGN_CENTER;
                cel3.HorizontalAlignment = Element.ALIGN_CENTER;
                cel4.HorizontalAlignment = Element.ALIGN_JUSTIFIED;

                table1.WidthPercentage = 40;
                table1.HorizontalAlignment = Element.ALIGN_LEFT;
                table1.AddCell(cel1);
                table1.AddCell(cel2);
                table1.AddCell(cel3);
                table1.AddCell(cel4);

                table1.SpacingAfter = 20;
                table1.SpacingBefore = 50;
                doc.Add(table1);

                // Client
                table1 = new PdfPTable(1);
                //cel1 = new PdfPCell(new Phrase("ID CLIENT:"+radTextBox2.Text, f_15_bold));        // jak wczytywal z komorki programu
                cel1 = new PdfPCell(new Phrase("ID CLIENT: " + " Jakies cos wpisac.", f_15_bold));
                cel2 = new PdfPCell(new Phrase("Nom Client: " + " Jakies cos wpisac.", f_15_bold));
                cel3 = new PdfPCell(new Phrase("Adress Client: " + " Jakies cos wpisac.", f_15_bold));

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

                PdfPTable table2 = new PdfPTable(1);
                table2.AddCell(table1);
                table2.HorizontalAlignment = Element.ALIGN_RIGHT;
                table2.WidthPercentage = 40;
                doc.Add(table2);

                // Date, Echeance + Product
                //Paragraph paragraph = new Paragraph(new Phrase("Date: " + radDateTimePicker1.Text + "\n", f_12_normal));  // wczytywal date z Boxa
                Paragraph paragraph = new Paragraph(new Phrase("Date: " + DateTime.Now.ToShortDateString() + "\n", f_12_normal));   // aktualna data z systemu
                paragraph.Add(new Phrase("Echance: " + "Jakas kolejna data z Boxa2" + "\n\n", f_12_normal));
                paragraph.Add(new Phrase("Facture N: " + "Jakis radTextBox1", f_15_bold));
                paragraph.Alignment = Element.ALIGN_JUSTIFIED;
                doc.Add(paragraph);

                //List of product
                //table1 = new PdfPTable(5);      //5 liczba kolumn
                //decimal ht = 0, tva = 0, ttc = 0;

                // copy the header of the datagridview - TO NIE JEST MI POTRZEBNE
                //for (int i = 0; i < 5; i++)
                //{
                //    cel1 = new PdfPCell(new Phrase (rad))
                //}









                doc.Close();

                // Open the document automatically
                System.Diagnostics.Process.Start(@"facture" + name.ToString() + ".pdf");
            }



        }
    }
}