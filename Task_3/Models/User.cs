using CsvHelper.Configuration.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Task_3.Models
{
    class User
    {
        [Index(0)]
        public string Login { get; set; }
        [Index(1)]
        public string Password { get; set; }
        public User(string login, string password)
        {
            Login = login;
            Password = password;
        }
        public override string ToString()
        {
            return $"Login: {Login}   Password: {Password}";
        }
    }
}
