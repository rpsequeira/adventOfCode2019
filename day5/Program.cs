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
            Console.WriteLine("Day 5 of AoC");
            string input = args[0];
            Console.WriteLine($"Loading data from " + input + "...");
            var lines = Tools.Tools.ReadAlllLinesAndSeparateComas(input).ToList();
            Console.WriteLine("STEP 1");
            var input1 = new List<int>(lines);
            // Set initial state
            Console.Write("Input:");
            var inputParam = Int32.Parse(Console.ReadLine());
            var res = GetIntegers(input1, inputParam);
            Console.WriteLine("Result: '" + res[0] + "'");
            Console.WriteLine("STEP 2");
            // int expected = 19690720;
            // Console.WriteLine("Result: '" + GetOptimalInputs(lines, expected) + "'");
            Console.WriteLine("END");
        }

        // private static int GetOptimalInputs(List<int> lines, int expected)
        // {
        //     for (int i = 0; i <= 99; i++)
        //     {
        //         for (int j = 0; j <= 99; j++)
        //         {
        //             var input = GetFreshList(lines);
        //             input[1] = i;
        //             input[2] = j;
        //             if (expected == GetIntegers(input)[0])
        //             {
        //                 return 100 * i + j;
        //             }
        //         }

        //     }
        //     return -1;
        // }

        private static List<int> GetFreshList(List<int> lines)
        {
            return new List<int>(lines);
        }

        private static List<int> GetIntegers(List<int> lines, int input)
        {
            var i = 0;
            do
            {
                int numberParameters = 0;
                var modes = DecodeOpCode(lines[i], out int op);
                switch (op)
                {
                    case 1:
                        numberParameters = 4;
                        var v1Op1 = modes[0] == 0 ? lines[lines[i + 1]] : lines[i + 1];
                        var v2Op1 = modes[1] == 0 ? lines[lines[i + 2]] : lines[i + 2];
                        var targetPosOp1 = lines[i + 3];
                        if (targetPosOp1 > lines.Count())
                        {
                            break;
                        }
                        lines[targetPosOp1] = v1Op1 + v2Op1;
                        break;
                    case 2:
                        numberParameters = 4;
                        var v1Op2 = modes[0] == 0 ? lines[lines[i + 1]] : lines[i + 1];
                        var v2Op2 = modes[1] == 0 ? lines[lines[i + 2]] : lines[i + 2];
                        var targetPosOp2 = lines[i + 3];
                        if (targetPosOp2 > lines.Count())
                        {
                            break;
                        }
                        lines[targetPosOp2] = v1Op2 * v2Op2;
                        break;
                    case 3:
                        numberParameters = 2;
                        var pos1 = lines[i + 1];
                        lines[pos1] = input;
                        break;
                    case 4:
                        numberParameters = 2;
                        var outputPos = lines[i + 1];
                        Console.WriteLine($"Operation:{i}|Output:{lines[outputPos]}");
                        break;
                }
                i = GetNextOpCode(lines, i, numberParameters);
            } while (i >= 0);
            return lines;
        }

        private static List<int> DecodeOpCode(int opCode, out int code)
        {
            var s = opCode.ToString().Reverse().ToList();
            if (s.Count() == 1)
            {
                code = opCode;
                return new List<int>() { 0, 0, 0 };
            }
            else
            {
                var x = new string(new char[] { s[1], s[0] });
                code = Int32.Parse(x);
            }
            var res = new List<int>();
            for (int i = 2; i < 5; i++)
            {
                if (i < s.Count())
                {
                    res.Add(Int32.Parse(s[i].ToString()));
                }
                else
                {
                    res.Add(0);
                }
            }
            return res;
        }

        private static int GetNextOpCode(List<int> lines, int currentPosition, int nParameters)
        {
            currentPosition += nParameters;
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
    }
}
