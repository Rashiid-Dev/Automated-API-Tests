using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Tests
{
    public class CommonMethods
    {
        public string NameRandomizer()
        {
            string randomString = new string(Enumerable.Repeat("ABCDEFGHIJKLMNOPQRSTUVWXYZ", 10)
            .Select(s => s[new Random().Next(s.Length)]).ToArray());

            return randomString;
        }
    }
}
