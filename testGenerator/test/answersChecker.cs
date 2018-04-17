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

            StreamReader reader = new StreamReader(fileName);

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
            answers = new string[count, questionNumber];

            //wpisywanie odczytanych odpowiedzi do tabeli
            for (int o = 0; o < count; o++)
            {
                check = reader.ReadLine();
                words = check.Split(z);

                for (int i = 0; i < questionNumber; i++)
                {
                    answers[o, i] = words[i + 2];
                    //Console.Write(" " + answers[o, i]);
                }
                //Console.WriteLine(" ");
            }
            reader.Close();
        }
        public void readKey(string fileName, int ansNumber)
        {
            char z = ';';
            string[] words;
            string check;

            StreamReader reader = new StreamReader(fileName);

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

        public bool checkTest(string testid)
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
                questionNumber = key.Count();
                studentsNumber = answers.GetLength(0);

                //deklaracja rozmiarów tablicy wyników
                results = new string[studentsNumber, questionNumber];

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
                }

                //wpisanie wyników do pliku
                StreamWriter writer = new StreamWriter("Results_" + testid + ".csv");

                writer.WriteLine("Test: " + testid +";Max: "+questionNumber.ToString());
                writer.Write("Student_index;Points;");
                writer.WriteLine("Percentage");

                for (int o = 0; o < studentsNumber; o++)
                {
                    writer.Write(results[o, 0] + ";" + results[o, 1] + ";");
                    writer.WriteLine(results[o, 2].ToString());
                }

                writer.Close();
                return true;
            }
        }
    }
}
