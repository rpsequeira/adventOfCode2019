using System;
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
            Parallel.For(0, input.Count(), (i, state) => action(i));
        }

        public static void ParallelForEach(IEnumerable<string> input, Action<string> action)
        {
            Parallel.ForEach(input, (item, state) => action(item));
        }
    }

    public class Point{
        public int X { get; set; }
        public int Y { get; set; }
        
        public Point(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }

        public override bool Equals(object obj){
            if(obj is Point point){
                return this.X == point.X && this.Y == point.Y;
            }
            return false;
        }

        public override int GetHashCode(){
            return base.GetHashCode();
        }

        public override string ToString(){
            return $"({this.X}, {this.Y})";
        }
    }
}
