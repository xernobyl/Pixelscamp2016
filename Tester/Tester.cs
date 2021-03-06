﻿using Kardashit;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Kardashit
{
    class Tester
    {
        static void Main(string[] args)
        {
            var lArgs = args.OfType<string>().ToList();

            string weights_file;
            string test_file;

            var i = lArgs.IndexOf("-t");
            if (i >= 0)
                test_file = lArgs[i + 1];
            else
            {
                Console.WriteLine("No test file specified! (-t)");
                return;
            }

            i = lArgs.IndexOf("-w");
            if (i >= 0)
                weights_file = lArgs[i + 1];
            else
                weights_file = "kardashit.weights";

            var weights = Common.ReadWeightFile(weights_file);
            int n_lines;
            var testData = Common.ParseClothesFile(test_file, out n_lines, false);

            int numOutput = 2;
            int numInput = 16;
            i = lArgs.IndexOf("-h");
            int numHidden = i >= 0 ? int.Parse(lArgs[i + 1]) : ((numOutput + numInput) * 4 + 4) / 6;            

            NeuralNetwork nn = new NeuralNetwork(numInput, numHidden, numOutput);
            nn.SetWeights(weights);

            i = lArgs.IndexOf("-o");

            if (i < 0)
            {
                double testAcc = nn.Accuracy(testData);
                Console.WriteLine("\nAccuracy on test data: " + testAcc);
            }
            else
            {
                string output_file = lArgs[i + 1];

                int fails = 0;

                using (System.IO.StreamWriter file = new System.IO.StreamWriter(output_file))
                {
                    bool header_line = true;
                    var lines = File.ReadAllLines(test_file);
                    int line_number = 0;

                    foreach (string line in lines)
                    {
                        bool flop;

                        if (header_line)
                        {
                            header_line = false;
                            file.WriteLine(line);
                            continue;
                        }

                        if (double.IsNaN(testData[line_number][11]))
                        {
                            flop = true;
                        }
                        else
                        {
                            var results = nn.ComputeOutputs(testData[line_number]);
                            flop = results[0] >= 0.5;
                        }

                        file.WriteLine(line + (flop ? ",1" : ",0"));

                        if (flop)
                            ++fails;

                        ++line_number;
                    }

                    Console.WriteLine("Flops: " + (double)fails / line_number * 100.0 + "%");
                }

                Console.ReadKey();
            }
        }
    }
}
