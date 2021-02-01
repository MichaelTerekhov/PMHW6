using System;
using System.Collections.Generic;
using System.Text;

namespace Task_2
{
    static class PrimesFinder
    {
        public static List<int> FindPrimes(Settings st)
        {
            List<int> result = new List<int>();
            if ((st.PrimesFrom > st.PrimesTo) && st.PrimesFrom > 0)
            {
                return new List<int>();
            }
            if (st.PrimesFrom < 0 && st.PrimesTo < 0)
            {
                return new List<int>();
            }

            for (var i = st.PrimesFrom; i < st.PrimesTo; i++)
            {
                if (i <= 1) continue;
                var isPrime = true;
                for (var j = 2; j < i; j++)
                {
                    if (i % j == 0)
                    {
                        isPrime = false;
                        break;
                    }
                }
                if (!isPrime) continue;
                result.Add(i);
            }
            return result;
        }
    }
}
