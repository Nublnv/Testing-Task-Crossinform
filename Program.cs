using System;
using System.Collections.Generic;
using System.IO;
using System.Diagnostics;
using System.Threading;

namespace Testing_Task_Crossinform
{
    class Program
    {
        
        static void Main(string[] args)
        {
            Top top = new Top();
            string result = "Результат: ";
            List<NumberOfOccurrences> top10;
            string filePath = "";
            bool fileOpened = false;
            int unitLength = 0;
            Stopwatch stopWatch = new Stopwatch();
            StreamReader streamReader;
            string fileString = "";
            Thread thread1;
            Thread thread2;
            Thread thread3;

            Console.WriteLine("Введите путь к файлу:");



            while (!fileOpened)
            {
                filePath = Console.ReadLine();
                if (File.Exists(filePath))
                {
                    fileOpened = true;
                    stopWatch.Start();
                    streamReader = new StreamReader(filePath);
                    fileString = streamReader.ReadToEnd();
                    unitLength = fileString.Length / 3;
                }
                else
                {
                    Console.WriteLine("Файл не найден. Укажите другой путь к файлу.");
                }
            }

            thread1 = new Thread(
                delegate ()
                {

                    string str = fileString.Substring(0, unitLength + 1);
                    string triplet;
                    for (int j = 0; j < str.Length - 2; j++)
                    {
                        if (char.IsLetter(str[j]) && char.IsLetter(str[j + 1]) && char.IsLetter(str[j + 2]))
                        {
                            triplet = String.Concat(str[j], str[j + 1], str[j + 2]).ToLower();
                            top.Add(triplet);
                        }
                    }
                });

            thread2 = new Thread(
                delegate ()
                {

                    string str = fileString.Substring(unitLength - 1, unitLength + 1);
                    string triplet;
                    for (int j = 0; j < str.Length - 2; j++)
                    {
                        if (char.IsLetter(str[j]) && char.IsLetter(str[j + 1]) && char.IsLetter(str[j + 2]))
                        {
                            triplet = String.Concat(str[j], str[j + 1], str[j + 2]).ToLower();
                            top.Add(triplet);
                        }
                    }
                });

            thread3 = new Thread(
                delegate ()
                {

                    string str = fileString.Substring(unitLength * 2 - 1);
                    string triplet;
                    for (int j = 0; j < str.Length - 2; j++)
                    {
                        if (char.IsLetter(str[j]) && char.IsLetter(str[j + 1]) && char.IsLetter(str[j + 2]))
                        {
                            triplet = String.Concat(str[j], str[j + 1], str[j + 2]).ToLower();
                            top.Add(triplet);
                        }
                    }
                });

            thread1.Start();
            thread2.Start();
            thread3.Start();


            while (thread1.IsAlive || thread2.IsAlive || thread3.IsAlive)
            {
                continue;
            }

            
            top10 = top.Top10();
            foreach (var item in top10)
            {
                result += item.nameOfTriplet + ", ";
            }
            Console.WriteLine(result);
            stopWatch.Stop();
            Console.WriteLine(stopWatch.Elapsed);
        }
    }
}
