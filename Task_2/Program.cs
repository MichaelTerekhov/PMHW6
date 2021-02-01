using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text.Json;
using System.Threading;

namespace Task_2
{
    class Program
    {
        private static ThreadSafeList resList = new ThreadSafeList();
        static void Main(string[] args)
        {
            Stopwatch time = new Stopwatch();
            var res = new Result();
            string jsonSettings;
            
            try
            {
                jsonSettings = File.ReadAllText("settings.json");
                var settings = JsonSerializer.Deserialize<List<Settings>>(jsonSettings);
                time.Start();
                ThreadPart(settings, time,res);
            }
            catch (FileNotFoundException)
            {
                time.Stop();
                res.Success = false;
                res.Duration = TimeParser(time);
                res.Error = "settings.json are missing";
                Serialization(res);
            }
            catch (JsonException)
            {
                time.Stop();
                res.Success = false;
                res.Duration = TimeParser(time);
                res.Error = "settings.json are corrupted";
                Serialization(res);
            }
            catch (Exception)
            {
                time.Stop();
                res.Success = false;
                res.Duration = TimeParser(time);
                res.Error = "Smth went wrong";
                Serialization(res);
            }
        }

        private static void ThreadPart(List<Settings> settings, Stopwatch time, Result res)
        {
            var threadList = new List<Thread>();
            foreach(var m in settings)
            {
                if (m == null)
                    continue;
                var thread = new Thread(() => resList.Add(m));
                thread.Start();
                threadList.Add(thread);
            }
            foreach (var m in threadList)
            {
                m.Join();
            }
            time.Stop();
            res.Success = true;
            res.Duration = TimeParser(time);
            res.Error = null;
            res.Primes = resList.List;
            Serialization(res);
        }

        private static void Serialization(Result res)
        {
            var options = new JsonSerializerOptions
            {
                WriteIndented = true,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };
            string jsonOutput;
            jsonOutput = JsonSerializer.Serialize(res, options);
            File.WriteAllText("result.json", jsonOutput);
        }

        private static string TimeParser(Stopwatch time)
        {
            TimeSpan ts = time.Elapsed;
            string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
            ts.Hours, ts.Minutes, ts.Seconds,
                    ts.Milliseconds / 10);
            return elapsedTime;
        }
    }
}
