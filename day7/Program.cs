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
            Console.WriteLine("Day 7 of AoC");
            string input = args[0];
            Console.WriteLine($"Loading data from " + input + "...");
            var lines = Tools.Tools.ReadAllLines(input).ToList();
            Console.WriteLine("STEP 1");
            //var res = GetOptimalPath(lines);
            //var res = GetOptimalPath(lines);
            //Console.WriteLine("Result: '" + res + "'");
            Console.WriteLine("STEP 2");
            var res2 = GetShortestPath(lines);
            Console.WriteLine("Result: '" + res2 + "'");
            Console.WriteLine("END");
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
