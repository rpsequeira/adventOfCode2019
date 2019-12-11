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
            Console.WriteLine("Day 4 of AoC");
            var lines = "402328-864247".Split("-"); ;
            Console.WriteLine("STEP 1");
            var res = GetPossiblePasswordsNumber(lines.First(), lines.Last(), ValidatePassword);
            Console.WriteLine("Result: '" + res + "'");
            Console.WriteLine("STEP 2");
            var res2 = GetPossiblePasswordsNumber(lines.First(), lines.Last(), ValidatePasswordPart2);
            Console.WriteLine("Result: '" + res2 + "'");
            Console.WriteLine("END");
        }

        private static int GetPossiblePasswordsNumber(string start, string end, Func<List<int>, bool> validator)
        {
            Console.WriteLine(start + " - " + end);
            List<int> first = start.Select(s => Int32.Parse(s.ToString())).ToList();
            List<int> last = end.Select(s => Int32.Parse(s.ToString())).ToList();
            List<int> password = new List<int>(first);
            int startNumber = Int32.Parse(start);
            int endNumber = Int32.Parse(end);
            int res = 0;

            for (int i = first[0]; i < last[0]; i++)
            {
                password[0] = i;
                for (int j = 0; j <= 9; j++)
                {
                    password[1] = j;
                    for (int k = j; k <= 9; k++)
                    {
                        password[2] = k;
                        for (int l = k; l <= 9; l++)
                        {
                            password[3] = l;
                            for (int m = l; m <= 9; m++)
                            {
                                password[4] = m;
                                for (int n = m; n <= 9; n++)
                                {
                                    password[5] = n;
                                    int a = Int32.Parse(string.Join("", password.Select(s => s.ToString())));
                                    if (a > endNumber || a < startNumber)
                                    {
                                        continue;
                                    }
                                    if (validator(password))
                                    {
                                        res++;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            return res;
        }


        private static bool ValidatePassword(List<int> password)
        {
            Dictionary<int, int> pairs = new Dictionary<int, int>();
            var lastDigit = -1;
            for (int i = 0; i < password.Count(); i++)
            {
                if (password[i] < lastDigit)
                {
                    return false;
                }
                if (pairs.ContainsKey(password[i]))
                {
                    pairs[password[i]]++;
                }
                else
                {
                    pairs.Add(password[i], 1);
                }
                lastDigit = password[i];
            }
            return pairs.Any(p => p.Value >= 2);
        }

        private static bool ValidatePasswordPart2(List<int> password)
        {
            Dictionary<int, int> pairs = new Dictionary<int, int>();
            var lastDigit = -1;
            for (int i = 0; i < password.Count(); i++)
            {
                if (password[i] < lastDigit)
                {
                    return false;
                }
                if (pairs.ContainsKey(password[i]))
                {
                    pairs[password[i]]++;
                }
                else
                {
                    pairs.Add(password[i], 1);
                }
                lastDigit = password[i];
            }
            return pairs.Any(p => p.Value == 2);
        }
    }
}
