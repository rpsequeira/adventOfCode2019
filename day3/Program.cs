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
            var lines = Tools.Tools.ReadAlllLinesAndSeparateComas(input).ToList();
            Console.WriteLine("STEP 1");
            var input1 = new List<int>(lines);
            // Set initial state
            input1[1] = 12;
            input1[2] = 2;
            var res = GetIntegers(input1);
            Console.WriteLine("Result: '" + res[0] + "'");
            Console.WriteLine("STEP 2");
            int expected = 19690720;
            Console.WriteLine("Result: '" + GetOptimalInputs(lines, expected) + "'");
            Console.WriteLine("END");
        }


        private static int GetOptimalInputs(List<int> lines, int expected)
        {
            for (int i = 0; i <= 99; i++)
            {
                for (int j = 0; j <= 99; j++)
                {
                    var input = GetFreshList(lines);
                    input[1] = i;
                    input[2] = j;
                    if (expected == GetIntegers(input)[0])
                    {
                        return 100 * i + j;
                    }
                }

            }
            return -1;
        }

        private static List<int> GetFreshList(List<int> lines)
        {
            return new List<int>(lines);
        }
        private static List<int> GetIntegers(List<int> lines)
        {
            var i = 0;
            do
            {
                var op = lines[i];
                switch (op)
                {
                    case 1:
                        var input1PosOp1 = lines[i + 1];
                        var input2PosOp1 = lines[i + 2];
                        var targetPosOp1 = lines[i + 3];
                        if (targetPosOp1 > lines.Count())
                        {
                            break;
                        }
                        lines[targetPosOp1] = lines[input1PosOp1] + lines[input2PosOp1];
                        break;
                    case 2:
                        var input1PosOp2 = lines[i + 1];
                        var input2PosOp2 = lines[i + 2];
                        var targetPosOp2 = lines[i + 3];
                        if (targetPosOp2 > lines.Count())
                        {
                            break;
                        }
                        lines[targetPosOp2] = lines[input1PosOp2] * lines[input2PosOp2];
                        break;
                }
                i = GetNextOpCode(lines, i);
            } while (i >= 0);
            return lines;
        }


        private static int GetNextOpCode(List<int> lines, int currentPosition)
        {
            currentPosition += 4;
            if (lines[currentPosition] == 99)
            {
                return -1;
            }
            else
            {
                if (currentPosition > lines.Count())
                {
                    return -1;
                }
                return currentPosition;
            }
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
            return res + fuel.Sum();
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
