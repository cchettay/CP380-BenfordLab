using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace BenfordLab
{
    public class BenfordData
    {
        public int Digit { get; set; }
        public int Count { get; set; }

        public BenfordData() { }
    }

    public class Benford
    {

        public static BenfordData[] calculateBenford(string csvFilePath)
        {
            // load the data
            var data = File.ReadAllLines(csvFilePath)
                .Skip(1) // For header
                .Select(s => Regex.Match(s, @"^(.*?),(.*?)$"))
                .Select(data => new
                {
                    Country = data.Groups[1].Value,
                    Population = int.Parse(data.Groups[2].Value)
                });

            var m = data
                   .Select(cn => new
                   {
                       Country = cn.Country,
                       Digit = FirstDigit.getFirstDigit(cn.Population)
                   })
                   .GroupBy(g => g.Digit)
                   .Select(b => new BenfordData { Digit = b.Key, Count = b.Count() })
                   .OrderBy(o => o.Digit)
                   ;
            return m.ToArray();
        }
    }
}
