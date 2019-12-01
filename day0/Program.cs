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
            Console.WriteLine("Day 0 of AoC");
            string input = args[0];
            Console.WriteLine($"Loading data from "+input+"...");
            var lines = Tools.Tools.ReadAllLines(input);
            int initFreq = 0;
            Console.WriteLine("STEP 1");
            foreach (var item in lines)
            {
                var operation = item[0];
                switch (operation)
                {
                    case '+':
                        initFreq += Int32.Parse(item.Substring(1));    
                        break;                
                    case '-':
                        initFreq -= Int32.Parse(item.Substring(1));
                        break;
                }
            }
            Console.WriteLine("Result: '"+initFreq+"'");
            Console.WriteLine("STEP 2");
            initFreq = 0;
            List<int> freqHistory = new List<int>();
            bool firstRun = true;
            while (!GetFrequency(lines, initFreq, freqHistory, firstRun, out List<int> newFreqHistory))
            {
                firstRun = false;
                freqHistory = newFreqHistory;
                initFreq = newFreqHistory.Last();
            }
            Console.WriteLine("END");
        }

        private static bool GetFrequency(IEnumerable<string>lines, int initFreq, List<int> inHistory, bool firstRun, out List<int> outHistory){
            foreach (var item in lines)
            {
                var operation = item[0];
                switch (operation)
                {
                    case '+':
                        initFreq += Int32.Parse(item.Substring(1));    
                        break;                
                    case '-':
                        initFreq -= Int32.Parse(item.Substring(1));
                        break;
                }
                if(inHistory.Contains(initFreq) && !firstRun){
                    Console.WriteLine("Result: '"+initFreq+"'");
                    outHistory = inHistory;   
                    return true;
                }
                inHistory.Add(initFreq);
            }
            outHistory = inHistory;            
            return false;
        }
    }
}
