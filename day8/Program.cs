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
            Console.WriteLine("Day 8 of AoC");
            string input = args[0];
            Console.WriteLine($"Loading data from " + input + "...");
            var lines = Tools.Tools.ReadAllLines(input).First();
            Console.WriteLine("STEP 1");
            var layers = GetLayers(6, 25, lines);
            // var layers = GetLayers(2, 2, "0222112222120000");
            var layer = GetLayerWithLessErrors(layers);
            var res = GetNumber(layer);
            Console.WriteLine("Result: '" + res + "'");
            Console.WriteLine("STEP 2");
            var res2 = GetFinalImage(layers);
            // Console.WriteLine("Result: '" + res2 + "'");
            PrintImage(res2, 25, 6);
            Console.WriteLine("");
            Console.WriteLine("END");
        }

        private static List<List<int>> GetLayers(int height, int width, string input)
        {
            var res = new List<List<int>>();
            var totalCharsPerLayer = height * width;
            var numberOfLayers = input.Length / totalCharsPerLayer;

            for (int i = 0; i < numberOfLayers; i++)
            {
                var layer = new List<int>();
                for (int j = 0; j < totalCharsPerLayer; j++)
                {
                    var pos = i * totalCharsPerLayer + j;
                    layer.Add(Int32.Parse(input[pos].ToString()));
                }
                res.Add(layer);
            }
            return res;
        }

        private static List<int> GetLayerWithLessErrors(List<List<int>> layers)
        {
            int res = 0;
            int counter = 9000;
            for (int i = 0; i < layers.Count(); i++)
            {
                var tempCounter = 0;
                foreach (var pixel in layers[i])
                {
                    if (pixel == 0)
                    {
                        tempCounter++;
                    }
                }
                if (tempCounter < counter)
                {
                    res = i;
                    counter = tempCounter;
                }
            }
            return layers[res];
        }

        private static int GetNumber(List<int> layer)
        {
            int onesCounter = 0;
            int twosCounter = 0;

            foreach (var item in layer)
            {
                if (item == 1)
                {
                    onesCounter++;
                }
                if (item == 2)
                {
                    twosCounter++;
                }
            }

            return onesCounter * twosCounter;
        }

        private static List<int> GetFinalImage(List<List<int>> layers)
        {
            var res = new List<int>(layers.First());
            // for (int i = 1; i < layers.Count(); i++)
            // {
            //     for (int j = 0; j < layers[i].Count(); j++)
            //     {
            //         if (res[j] == 2)
            //         {
            //             res[j] = layers[i][j];
            //         }
            //     }
            // }

            for (int i = 0; i < res.Count(); i++)
            {
                for (int j = 0; j < layers.Count(); j++)
                {
                    if (layers[j][i] != 2)
                    {
                        res[i] = layers[j][i];
                        break;
                    }
                }
            }
            return res;
        }

        private static void PrintImage(List<int> image, int width, int height)
        {
            for (int i = 0; i < image.Count; i++)
            {
                if (i > 0 && i % width == 0)
                {
                    Console.WriteLine("");
                }
                if (image[i] == 1)
                {
                    Console.Write("*");
                }
                else
                {
                    Console.Write(" ");
                }   
            }
        }
    }
}
