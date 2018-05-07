using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace test
{
    class answersChecker
    {
        private string[,] answers;
        private string[] key;
        private string[,] results;

        public void answerChecker()
        {
            answers = null;
            key = null;
            results = null;
        }

        public void readAnswers(string fileName)
        {
            char z = ';';
            string[] words;
            string check;
            int count = 0;
            int k = 1;

            StreamReader reader = new StreamReader("..\\..\\..\\Files\\" + fileName);

            //przeliczenie liczby studentow
            do
            {
                check = reader.ReadLine();
                count++;
                if (check == null)
                {
                    k = 0;
                }

            } while (k == 1);

            //odjęcie pierwszej i ostatniej linii
            count--;
            count--;

            //powrot do poczatku pliku
            reader.DiscardBufferedData();
            reader.BaseStream.Seek(0, System.IO.SeekOrigin.Begin);

            //odczyt pierwszej linii, przeliczenie odpowiedzi
            check = reader.ReadLine();
            words = check.Split(z);
            //Console.WriteLine(check);
            int questionNumber = words.Count() - 3;


            //deklaracja rozmiaru tablicy odpowiedzi
            answers = new string[count, questionNumber+1];

            //wpisywanie odczytanych odpowiedzi do tabeli
            for (int o = 0; o < count; o++)
            {
                check = reader.ReadLine();
                words = check.Split(z);

                for (int i = 0; i < questionNumber+1; i++)
                {
                    answers[o, i] = words[i + 2];
                    Console.Write(" " + answers[o, i]);
                }
                Console.WriteLine(" ");
            }
            reader.Close();
        }
        public void readKey(string fileName, int ansNumber)
        {
            char z = ';';
            string[] words;
            string check;

            StreamReader reader = new StreamReader("..\\..\\..\\Files\\" + fileName);

            //odczyt pierwszej linii, przeliczenie odpowiedzi
            check = reader.ReadLine();
            words = check.Split(z);
            int questionNumber = words.Count() - 2;

            reader.DiscardBufferedData();
            reader.BaseStream.Seek(0, System.IO.SeekOrigin.Begin);

            //deklaracja rozmiaru tablicy klucza
            key = new string[questionNumber];

            //wpisywanie odczytanego klucza do tabeli
            check = reader.ReadLine();
            words = check.Split(z);

            for (int i = 0; i < questionNumber; i++)
            {
                key[i] = words[i + 2];
                if (key[i].Length < ansNumber)
                {
                    int dif = ansNumber - key[i].Length;
                    while (dif != 0)
                    {
                        key[i] = "0" + key[i];
                        dif--;
                    }

                }

                Console.Write(" " + key[i]);
            }
            Console.WriteLine(" ");
            reader.Close();
        }

        public bool checkTest(string testid, double[] partition)
        {
            if (answers == null || key == null)
            {
                return false;
            }
            else
            {
                //przeliczanie tablic
                int questionNumber;
                int studentsNumber;
                int points;
                double percentage;
                double[] mark;
                int[] marks;
                double[] marks_percentage;
                int count_marks;
                questionNumber = key.Count();
                studentsNumber = answers.GetLength(0);

                marks = new int[7];
                marks_percentage = new double[7];
                //deklaracja rozmiarów tablicy wyników
                results = new string[studentsNumber, questionNumber];
                mark = new double[studentsNumber];

                for (int o = 0; o < studentsNumber; o++)
                {
                    //przepisanie indeksów studentów
                    results[o, 0] = answers[o, 0];
                    //zerowanie punktow przy zmianie studenta
                    points = 0;

                    //liczenie punktow studenta
                    for (int i = 0; i < questionNumber; i++)
                    {
                        if(answers[o,i+1] == key[i])
                        {
                            points++;
                        }
                    }

                    //wypełnienie tablicy wyników
                    percentage = (float)points / (float)questionNumber;
                    percentage = (Math.Round(percentage, 2)*100);
                    results[o, 1] = points.ToString();
                    results[o, 2] = percentage.ToString()+"%";

                    if (percentage >= partition[0])
                    {
                        mark[o] = 5.5;
                        marks[0]++;
                    }
                    else if (percentage >= partition[1])
                    {
                        mark[o] = 5;
                        marks[1]++;
                    }
                    else if (percentage >= partition[2])
                    {
                        mark[o] = 4.5;
                        marks[2]++;
                    }
                    else if (percentage >= partition[3])
                    {
                        mark[o] = 4;
                        marks[3]++;
                    }
                    else if (percentage >= partition[4])
                    {
                        mark[o] = 3.5;
                        marks[4]++;
                    }
                    else if (percentage >= partition[5])
                    {
                        mark[o] = 3;
                        marks[5]++;
                    }
                    else
                    {
                        mark[o] = 2;
                        marks[6]++;
                    }
                }

                count_marks = marks[0] + marks[1] + marks[2] + marks[3] + marks[4] + marks[5] + marks[6];
                //wpisanie wyników do pliku
                StreamWriter writer = new StreamWriter("..\\..\\..\\Files\\Results_" + testid + ".csv");

                writer.WriteLine("Test: " + testid +";Max: "+questionNumber.ToString());
                writer.Write("Student_index;Points;Percentage;");
                writer.WriteLine("Mark");

                for (int o = 0; o < studentsNumber; o++)
                {
                    writer.Write(results[o, 0] + ";" + results[o, 1] + ";" + results[o, 2].ToString() +";");
                    writer.WriteLine(mark[o]);
                }

                writer.WriteLine(" ");
                writer.WriteLine("Statistics");
                writer.WriteLine("Mark;Quantity;Percentage");
                for (int i=0; i<7; i++)
                {
                    marks_percentage[i] = (float)marks[i] / (float)count_marks;
                    marks_percentage[i] = (Math.Round(marks_percentage[i], 2) * 100);
                    if (i != 6)
                    {
                        writer.WriteLine((5.5 - i * 0.5) + ";" + marks[i] + ";" + marks_percentage[i] + "%");
                    }
                    else
                    {
                        writer.WriteLine("2" + ";" + marks[i] + ";" + marks_percentage[i] + "%");
                    }
                }

                writer.Close();
                return true;
            }
        }
    }
}
