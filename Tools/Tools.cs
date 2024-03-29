﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Tools
{
    public static class Tools
    {
        public static string GetHelloWorld()
        {
            return "Hello world!(Tools)";
        }

        public static IEnumerable<string> ReadAllLines(string filename)
        {
            string line = null;
            System.IO.StreamReader file = new System.IO.StreamReader(filename);
            while ((line = file.ReadLine()) != null)
            {
                yield return line;
            }
            file.Close();
        }

        public static IEnumerable<int> ReadAlllLinesAndSeparateComas(string filename)
        {
            string line = null;
            System.IO.StreamReader file = new System.IO.StreamReader(filename);
            List<int> res = new List<int>();
            while ((line = file.ReadLine()) != null)
            {
                res.AddRange(line.Split(',').Select(Int32.Parse));
            }
            file.Close();
            return res;
        }

        public static void PrintArray(IEnumerable<int> input)
        {
            Console.Write("Printing array:");
            foreach (var item in input)
            {
                Console.Write(item.ToString() + ",");
            }
            Console.WriteLine("|END");
        }

        public static void ParallelFor(IEnumerable<string> input, Action<int> action)
        {
            Parallel.For(0, input.Count(), new ParallelOptions { MaxDegreeOfParallelism = 4 }, (i, state) => action(i));
        }

        public static void ParallelForEach(IEnumerable<string> input, Action<string> action)
        {
            Parallel.ForEach(input, new ParallelOptions { MaxDegreeOfParallelism = 4 }, (item, state) => action(item));
        }

        public static void MeasureActionTime(string desc, Action action)
        {
            var stopwatch = System.Diagnostics.Stopwatch.StartNew();
            action();
            Console.WriteLine($"{desc} took {stopwatch.ElapsedMilliseconds} ms");
        }
    }

    public struct Point
    {
        public int X { get { return _x; } }
        private int _x;
        public int Y { get { return _y; } }
        private int _y;
        public Point(int x, int y)
        {
            this._x = x;
            this._y = y;
        }

        public override bool Equals(object obj)
        {
            if ((obj == null) || !this.GetType().Equals(obj.GetType()))
            {
                return false;
            }
            else
            {
                Point p = (Point)obj;
                return (_x == p.X) && (_y == p.Y);
            }
        }

        public override int GetHashCode()
        {
            return (_x << 2) ^ _y;
        }

        public override string ToString()
        {
            return $"({this.X}, {this.Y})";
        }
    }
}
