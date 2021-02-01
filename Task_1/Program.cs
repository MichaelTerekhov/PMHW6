using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Task_1
{
    class Program
    {
        static void Main(string[] args)
        {
            Hello();
            while (true)
            {
                MenuList();
                Console.Write("Select -> ");
                bool input = int.TryParse(Console.ReadLine(), out int menuChoose);
                if (!input)
                {
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine("Unsupported command! Try again");
                    Console.ResetColor();
                    continue;
                }
                else
                {
                    switch (menuChoose)
                    {
                        case 1:
                            SequentialSearch();
                            break;
                        case 2:
                            ParallelSearch();
                            break;
                        case 3:
                            Console.WriteLine();
                            return;
                    }
                }
            }
        }

        private static void ParallelSearch()
        {
            int from;
            int to;
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("\n[PARALLEL SEARCH]\n"+"Please enter the range in which you want to search for prime numbers");
            Console.ResetColor();
            while (true)
            {
                Console.Write("Range From -> ");
                bool correctFrom = int.TryParse(Console.ReadLine(), out from);
                if (!correctFrom)
                {
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine("Wrong input of from param");
                    Console.ResetColor();
                    continue;
                }
                while (true)
                {
                    Console.Write("Range To -> ");
                    bool input = int.TryParse(Console.ReadLine(), out to);
                    if (!input)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.WriteLine("Wrong input of to param");
                        Console.ResetColor();
                        continue;
                    }
                    break;
                }
                break;
            }
            Console.WriteLine();
            var time = new Stopwatch();
            time.Start();
            List<int> primes;
            int fromLinq = from;
            if ((from > to) && from > 0)
            {
                primes = new List<int>();
                time.Stop();
                ShowResuts(from, to, primes, time);
                return;
            }
            else if (from < 0 && to < 0)
            {
                primes = new List<int>();
                time.Stop();
                ShowResuts(from, to, primes, time);
                return;
            }
            else if (from <= 2 && to > 0)
            {
                fromLinq = 2;
            }
            primes = (from x in Enumerable.Range(@fromLinq, to - @fromLinq).AsParallel()
                      where Enumerable.Range(2, x - 2).All(j => x % j != 0)
                      select x).ToList();
            time.Stop();
            ShowResuts(from, to, primes, time);
        }

        private static void SequentialSearch()
        {
            int from;
            int to;
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\n[SEQUANTIAL SEARCH]\n"+"Please enter the range in which you want to search for prime numbers");
            Console.ResetColor();
            while (true)
            {
                Console.Write("Range From -> ");
                bool correctFrom = int.TryParse(Console.ReadLine(), out from);
                if (!correctFrom)
                {
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine("Wrong input of from param");
                    Console.ResetColor();
                    continue;
                }
                while (true)
                {
                    Console.Write("Range To -> ");
                    bool input = int.TryParse(Console.ReadLine(), out to);
                    if (!input)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.WriteLine("Wrong input of to param");
                        Console.ResetColor();
                        continue;
                    }
                    break;
                }
                break;
            }
            Console.WriteLine();
            var time = new Stopwatch();
            time.Start();
            List<int> primes;
            int fromLinq = from;
            if ((from > to) && from > 0)
            {
                primes = new List<int>();
                time.Stop();
                ShowResuts(from, to, primes, time);
                return;
            }
            else if (from < 0 && to < 0)
            {
                primes = new List<int>();
                time.Stop();
                ShowResuts(from, to, primes, time);
                return;
            }
            else if (from <= 2 && to > 0)
            {
                fromLinq = 2;
            }
            primes = (from x in Enumerable.Range(@fromLinq, to - @fromLinq)
                                      where Enumerable.Range(2, x - 2).All(j => x % j != 0)
                                      select x).ToList();
            time.Stop();
            ShowResuts(from, to, primes, time);
        }


        private static void ShowResuts(int from, int to,List<int> primes, Stopwatch time)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"Primes in range from [{from} , {to}): {primes.Count}\n" +
                $"Elapsed time: {time.Elapsed}\n");
            Console.ResetColor();
        }

        private static void Hello()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("1. Welcome to the program that allows you to find\n" +
                "all the prime numbers in the range you specify.\n" +
                "To search for primes, you just need to select a search technology\n" +
                "(c)Michael Terekhov\n\n");
        }
        private static void MenuList()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("1.\tLINQ(sequential search).\n" +
                "2.\tPLINQ(parallel search).\n" +
                "3.\tExit\n\n" +
                "Example:\tSelect-> 1");
            Console.ResetColor();
        }
    }
}
