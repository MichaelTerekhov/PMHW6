using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Task_3.Models;
namespace Task_3.Service
{
    static class DataServiceCsv
    {
        public static ConcurrentQueue<User> GetDataFromCsv()
        {
            List<User> records = new List<User>();
            var res = new ConcurrentQueue<User>();
            try
            {
                var inp = File.ReadAllLines("logins.csv");
                records.AddRange(inp.ToList()
                    .Select(x =>
                    {
                        var val = x.Split(new char[] { ';' });
                        var gd = new User(val[0], val[1]);
                        return gd;
                    }).ToList());
                records.ForEach(x => res.Enqueue(x));
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("File wasnt found!");
            }
            return res;
        }
    }
}
