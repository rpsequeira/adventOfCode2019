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
            var res = GetPossiblePasswordsNumber(lines.First(), lines.Last());
            Console.WriteLine("Result: '" + res + "'");
            Console.WriteLine("STEP 2");
            //Console.WriteLine("Result: '" + res2 + "'");
            Console.WriteLine("END");
        }

        private static int GetPossiblePasswordsNumber(string start, string end)
        {
            Console.WriteLine(start + " - " + end);
            List<int> first = start.Select(s => Int32.Parse(s.ToString())).ToList();
            List<int> last = end.Select(s => Int32.Parse(s.ToString())).ToList();
            List<int> password = new List<int>(first);
            int endNumber = Int32.Parse(end);
            int res = 0;
            for (int i = first[0]; i <= last[0]; i++)
            {
                password[0] = i;
                for (int j = 0; j <= 9; j++)
                {
                    password[1] = j;
                    for (int k = 0; k <= 9; k++)
                    {
                        password[2] = k;
                        for (int l = 0; l <= 9; l++)
                        {
                            password[3] = l;
                            for (int m = 0; m <= 9; m++)
                            {
                                password[4] = m;
                                for (int n = 0; n <= 9; n++)
                                {
                                    password[5] = n;
                                    int a = Int32.Parse(string.Join("", password.Select(s => s.ToString())));
                                    if (a > endNumber)
                                    {
                                        continue;
                                    }
                                    if (a < endNumber)
                                    {
                                        continue;
                                    }
                                    if (ValidatePassword(password))
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
            var pairs = 0;
            var pairPossible = false;
            var lastPair = -99;
            for (int i = 0; i < password.Count(); i++)
            {
                var lastDigit = -1;
                if (password[i] < lastDigit)
                {
                    return false;
                }
                if (password[i] == lastDigit)
                {
                    if (pairPossible)
                    {
                        pairPossible = false;
                        pairs++;
                        lastPair = password[i];
                    }
                    else
                    {
                        pairPossible = true;
                    }
                }
                else
                {
                    if (pairPossible)
                    {
                        if (lastPair == lastDigit)
                        {
                            return false;
                        }
                    }
                }

                lastDigit = password[i];
            }
            return true;
        }



        private static int GetShortestPath(List<string> lines)
        {
            var paths = new List<List<Tools.Point>>();
            Tools.Tools.ParallelForEach(lines, (item) =>
            {
                paths.Add(GetPath(item));
            });
            var distances = GetCrossingPointsSize(paths);
            return distances.Min();
        }

        private static int GetOptimalPath(List<string> lines)
        {
            var paths = new List<List<Tools.Point>>();
            Tools.Tools.ParallelForEach(lines, (item) =>
            {
                paths.Add(GetPath(item));
            });
            var crossingPoints = GetCrossingPoints(paths);
            var distances = crossingPoints.Select(p => Math.Abs(p.X) + Math.Abs(p.Y));
            return distances.Min();
        }

        private static List<Tools.Point> GetPath(string line)
        {
            var path = new List<Tools.Point>();
            var instructions = line.Split(",");
            Tools.Point refPoint = new Tools.Point(0, 0);
            foreach (var item in instructions)
            {
                path.AddRange(GetPoints(item, refPoint));
                refPoint = path.Last();
            }
            return path;
        }

        private static List<Tools.Point> GetPoints(string instruction, Tools.Point refPoint)
        {
            var direction = instruction[0];
            var steps = Int32.Parse(instruction.Substring(1));
            var res = new List<Tools.Point>();
            switch (direction)
            {
                case 'R':
                    for (int i = 1; i <= steps; i++)
                    {
                        var newPoint = new Tools.Point(refPoint.X + i, refPoint.Y);
                        res.Add(newPoint);
                    }
                    break;
                case 'L':
                    for (int i = 1; i <= steps; i++)
                    {
                        var newPoint = new Tools.Point(refPoint.X - i, refPoint.Y);
                        res.Add(newPoint);
                    }
                    break;
                case 'U':
                    for (int i = 1; i <= steps; i++)
                    {
                        var newPoint = new Tools.Point(refPoint.X, refPoint.Y + i);
                        res.Add(newPoint);
                    }
                    break;
                case 'D':
                    for (int i = 1; i <= steps; i++)
                    {
                        var newPoint = new Tools.Point(refPoint.X, refPoint.Y - i);
                        res.Add(newPoint);
                    }
                    break;
            }
            return res;
        }

        private static IEnumerable<Tools.Point> GetCrossingPoints(List<List<Tools.Point>> paths, int pathIndex = 0)
        {
            foreach (var item in paths.First())
            {
                if (paths.Last().Contains(item))
                {
                    yield return item;
                }
            }
        }

        private static IEnumerable<int> GetCrossingPointsSize(List<List<Tools.Point>> paths, int pathIndex = 0)
        {
            foreach (var item in paths.First())
            {
                if (paths.Last().Contains(item))
                {
                    yield return paths.First().IndexOf(item) + paths.Last().IndexOf(item) + 2;
                }
            }
        }
    }
}
