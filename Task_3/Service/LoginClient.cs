using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Task_3.Service
{
    class LoginClient
    {
        public string Login(string login, string password)
        {
            Random rndChance = new Random();
            int chance = rndChance.Next(1,101);

            Random rndChanceTime = new Random();

            Thread.Sleep(rndChanceTime.Next(0, 1000));
            if (chance <= 50)
            {
                return Guid.NewGuid().ToString();
            }
            return null;
        }
    }
}
