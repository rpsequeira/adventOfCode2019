using System;
using Tools;
using System.Collections.Generic;
using System.Linq;

namespace adventofcode
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Day 2 of AoC");
            string input = args[0];
            Console.WriteLine($"Loading data from " + input + "...");
            var lines = Tools.Tools.ReadAllLines(input);
            Console.WriteLine("STEP 1");
            var res = GetFuel(lines);
            Console.WriteLine("Result: '" + res + "'");
            Console.WriteLine("STEP 2");
            var resWithFuel = GetFuel2(lines);
            Console.WriteLine("Result: '" + resWithFuel + "'");
            Console.WriteLine("END");
        }

        private static int GetFuel(IEnumerable<string> lines)
        {
            int res = 0;
            foreach (var item in lines)
            {
                var rocketFuel = Math.Floor(Decimal.Parse(item) / 3) - 2;
                res += Convert.ToInt32(rocketFuel);
            }            
            return res;
        }
        private static int GetFuel2(IEnumerable<string> lines)
        {
            int res = 0;
            List<int> fuel = new List<int>();
            foreach (var item in lines)
            {
                var rocketFuel = Math.Floor(Decimal.Parse(item) / 3) - 2;
                res += Convert.ToInt32(rocketFuel);
                fuel.Add(GetFuelForFuel(Convert.ToInt32(rocketFuel)));
            }
            return res+fuel.Sum();
        }
        private static int GetFuelForFuel(int fuel)
        {
            int res = 0;
            decimal rocketFuel = fuel;
            do
            {
                var o = (rocketFuel / 3) - 2;
                rocketFuel = Math.Floor(o);
                if (rocketFuel > 0)
                {
                    res += Convert.ToInt32(rocketFuel);
                }
            }
            while (rocketFuel > 0);

            return res;
        }
    }
}
