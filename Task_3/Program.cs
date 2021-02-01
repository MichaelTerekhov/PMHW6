using System;
using System.Collections.Concurrent;
using System.IO;
using System.Linq;
using Task_3.Models;
using Task_3.Service;
using System.Collections.Generic;
using System.Threading;
using System.Text.Json;

namespace Task_3
{
    class Program
    {
        private static ConcurrentQueue<User> users = new ConcurrentQueue<User>();
        private static LoginClient platform = new LoginClient();

        private static int success = 0;
        private static int failed = 0;
        static void Main(string[] args)
        {
            Hello();
            users = DataServiceCsv.GetDataFromCsv();
            Tester(TryToChooseThreads());
            SerializeResults();
        }

        private static void SerializeResults()
        {
            var result = new Result()
            {
                Successful = success,
                Failed = failed
            };
            var options = new JsonSerializerOptions
            {
                WriteIndented = true,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };
            string jsonOutput;
            jsonOutput = JsonSerializer.Serialize(result, options);
            File.WriteAllText("result.json", jsonOutput);
        }

        private static void Tester(int numberOfThreads)
        {
            CountdownEvent countdown = new CountdownEvent(numberOfThreads);
            for (var i = 0; i < numberOfThreads; i++)
            {
                var thread = new Thread(() => { ThreadHandlerFunc();countdown.Signal();});
                thread.Start();
            }
            countdown.Wait();
        }
        private static void ThreadHandlerFunc()
        {
            while (users.TryDequeue(out User user))
            {
                Console.WriteLine($"{user.Login}    {user.Password}");
                if (platform.Login(user.Login, user.Password) == null)
                    Interlocked.Increment(ref failed);
                else
                    Interlocked.Increment(ref success);
            }
        }

        private static void Hello()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"3. Welcome to the testing platform of your authentication service\n" +
                $"You are given the opportunity to test the load by setting\n" +
                $"your own number of threads for processing personal data\n" +
                $"(c)Michael Terekhov");
            Console.ResetColor();
        }
        private static int TryToChooseThreads()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("\nPlease enter the number of threads for testing\n");
            Console.ResetColor();
            int threadNumber = 0;
            while (true)
            {
                Console.Write("Number of threads -> ");
                bool input = int.TryParse(Console.ReadLine(), out threadNumber);
                if (!input || threadNumber <= 0)
                {
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine("Unsupported number! Try again");
                    Console.ResetColor();
                    continue;
                }
                break;
            }
            return threadNumber;
        }
    }
}
