using System;
using System.Collections.Generic;
using System.IO;

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
    }
}
