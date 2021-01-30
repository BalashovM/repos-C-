using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using System;
using System.Collections.Generic;

namespace Lesson_04_Search_in_HashSet_and_Array
{
    class Program
    {
        static void Main(string[] args)
        {
           /* BenchmarkClass ccc = new BenchmarkClass();
            Console.WriteLine($"{ccc.SearchWord}");
            ccc.SearchInArray();
            ccc.SearchInHashSet();*/
            BenchmarkSwitcher.FromAssembly(typeof(Program).Assembly).Run(args);

        }
        [MinColumn, MedianColumn, MaxColumn]
        public class BenchmarkClass
        {

            public static HashSet<string> hashSet;
            public static string[] array;
            public static int qtyLetters = 10;
            public static int qtyWords = 100;
            public string SearchWord { get; set; }

            public BenchmarkClass() 
            { 
               hashSet = new HashSet<string>();
               array = new string[qtyWords];
               SetsFill();
            }

            public void SetsFill()
            {
                char[] letters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();

                Random rand = new Random();

                for (int i = 0; i < qtyWords; i++)
                {
                    string word = "";
                    for (int j = 1; j <= qtyLetters; j++)
                    {
                        int letterNum = rand.Next(0, letters.Length - 1);
                        word += letters[letterNum];
                    }

                    array[i] = word;
                    hashSet.Add(word);
                }

                SearchWord = array[rand.Next(0, qtyWords)];
            }
            [Benchmark]
            public void SearchInHashSet() 
            {
                for(int i =0; i < array.Length-1; i++)
                {
                    if (array[i] == SearchWord)
                        break;
                }
            }
            [Benchmark]
            public void SearchInArray() 
            {
                foreach (var word in hashSet)
                {
                    if (word.Equals(SearchWord))
                        break;
                }
            }

        }
        /*
        static void SetsFill(string[] array, HashSet<string> hash, int qtyLetters, int qtyWords)
        {
            // Создаем массив букв, которые мы будем использовать.
            char[] letters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();

            Random rand = new Random();

            for (int i = 1; i <= qtyWords; i++)
            {
                string word = "";
                for (int j = 1; j <= qtyLetters; j++)
                {
                    int letterNum = rand.Next(0, letters.Length - 1);
                    word += letters[letterNum];
                }

                array[i] = word;
                hash.Add(word);
            }
        }*/
    }
}
