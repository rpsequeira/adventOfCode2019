﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

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

        public static void PrintArray(IEnumerable<int> input){
            Console.Write("Printing array:");
            foreach (var item in input)
            {
                Console.Write(item.ToString()+",");
            }
            Console.WriteLine("|END");
        }
    }
}
