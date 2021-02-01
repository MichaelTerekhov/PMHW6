using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Task_2
{
    public class ThreadSafeList
    {
        public List<int> List { get; private set; }
        private static object marker = new object();
        
        public ThreadSafeList()
        {
            List = new List<int>();
        }
        public void Add(Settings st)
        {
            lock (marker)
            {
                var primesList = PrimesFinder.FindPrimes(st);
                primesList.ForEach(x => List.Add(x));
                List = List.Distinct().OrderBy(x => x).ToList();
            }
        }
    }
}
