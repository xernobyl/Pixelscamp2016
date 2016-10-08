using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text.RegularExpressions;

namespace Kardashit
{
    class Trainer
    {
        static void Main(string[] args)
        {
            var lArgs = args.OfType<string>().ToList();

            /*if (args.Length != 2)
            {
                Console.WriteLine("You forgot the files!");

                return;
            }    

            var trainData = ReadInputFile(args[0]);
            Console.WriteLine("Train data: " + args[0] + " (" + trainData.Length + " entries)");

            var testData = ReadInputFile(args[1]);
            Console.WriteLine("Test data: " + args[1] + " (" + testData.Length + " entries)");*/

            //var allData = ReadInputFile("F:\\learning\\flower_data_2.csv", true);
            //var allData = ReadInputCSV("F:\\learning\\filtered.csv");
            //MakeClothesFileLists("C:\\Users\\xerno\\Documents\\learning\\Sonae-Challenge-Rage-Against-the-Machine\\1_pixelcamp_train_test.csv");
            int n_lines;
            var allData = Common.ParseClothesFile("C:\\Users\\xerno\\Documents\\learning\\Sonae-Challenge-Rage-Against-the-Machine\\1_pixelcamp_train_test.csv", out n_lines);
            Console.WriteLine("Input data read, " + allData.Length + " lines.");

            double[][] trainData = null;
            double[][] testData = null;
            MakeTrainTest(allData, out trainData, out testData, n_lines, 0);

            //Console.WriteLine("\nFirst 5 rows of training data:");
            //ShowMatrix(trainData, 5, 1, true);
            //Console.WriteLine("First 3 rows of test data:");
            //ShowMatrix(testData, 3, 1, true);

            //Normalize(trainData, new int[] { 0, 1, 2, 3 });
            //Normalize(testData, new int[] { 0, 1, 2, 3 });
            //Common.Normalize(trainData, new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16 });
            //Common.Normalize(testData, new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16 });

            //Console.WriteLine("\nFirst 5 rows of normalized training data:");
            //ShowMatrix(trainData, 5, 1, true);
            //Console.WriteLine("First 3 rows of normalized test data:");
            //ShowMatrix(testData, 3, 1, true);

            int numOutput = 2;
            int numInput = allData[0].Length - numOutput;

            int i = lArgs.IndexOf("-h");
            int numHidden = i >= 0 ? int.Parse(lArgs[i + 1]) : ((numOutput + numInput) * 4 + 4) / 6;

            i = lArgs.IndexOf("-e");
            int maxEpochs = i >= 0 ? int.Parse(lArgs[i + 1]) : 2000;

            i = lArgs.IndexOf("-l");
            double learnRate = i >= 0 ? double.Parse(lArgs[i + 1], CultureInfo.InvariantCulture) : 0.00125;

            i = lArgs.IndexOf("-m");
            double momentum = i >= 0 ? double.Parse(lArgs[i + 1], CultureInfo.InvariantCulture) : 0.001;

            Console.WriteLine("\nCreating a {0}-input, {1}-hidden, {2}-output neural network", numInput, numHidden, numOutput);
            Console.WriteLine("Hard-coded tanh function for input-to-hidden and softmax for hidden-to-output activations");

            NeuralNetwork nn = new NeuralNetwork(numInput, numHidden, numOutput);

            nn.InitializeWeights();

            i = lArgs.IndexOf("-d");
            double weightDecay = i >= 0 ? double.Parse(lArgs[i + 1], CultureInfo.InvariantCulture) : 0.0001;

            double minMse = double.NegativeInfinity;    // 0.00001;

            Console.WriteLine("Setting maxEpochs = {0}, learnRate = {1}, momentum = {2}, weightDecay = {3}, error = {4}", maxEpochs, learnRate, momentum, weightDecay, minMse);

            Console.WriteLine("\nBeginning training using incremental back-propagation\n");
            nn.Train(trainData, maxEpochs, learnRate, momentum, weightDecay, minMse);
            Console.WriteLine("Training complete");

            double[] weights = nn.GetWeights();
            i = lArgs.IndexOf("-w");
            Common.WriteWeightFile(weights, i >= 0 ? lArgs[i + 1] : "kardashit.weights");

            Console.WriteLine("Final neural network weights and bias values:");
            Common.ShowVector(weights, 10, 8, true);

            double trainAcc = nn.Accuracy(trainData);
            Console.WriteLine("\nAccuracy on training data = " + trainAcc.ToString("F4"));

            double testAcc = nn.Accuracy(testData);
            Console.WriteLine("\nAccuracy on test data = " + testAcc.ToString("F4"));

            Console.WriteLine("\nDone\n");
            Console.ReadLine();
        }

        static void MakeTrainTest(double[][] allData, out double[][] trainData, out double[][] testData, int totRows = -1, int testRows = -1)
        {
            // split allData into 80% trainData and 20% testData
            Random rnd = new Random(0);
            if (totRows == -1)
                totRows = allData.Length;
            int numCols = allData[0].Length;

            int trainRows;
            if (testRows == -1)
            {
                trainRows = (int)(totRows * 0.8);
                testRows = totRows - trainRows;
            }
            else
            {
                trainRows = totRows - testRows;
            }

            trainData = new double[trainRows][];
            testData = new double[testRows][];

            int[] sequence = new int[totRows]; // create a random sequence of indexes
            for (int i = 0; i < sequence.Length; ++i)
                sequence[i] = i;

            for (int i = 0; i < sequence.Length; ++i)
            {
                int r = rnd.Next(i, sequence.Length);
                int tmp = sequence[r];
                sequence[r] = sequence[i];
                sequence[i] = tmp;
            }

            int si = 0; // index into sequence[]
            int j = 0; // index into trainData or testData

            for (; si < trainRows; ++si) // first rows to train data
            {
                trainData[j] = new double[numCols];
                int idx = sequence[si];
                Array.Copy(allData[idx], trainData[j], numCols);
                ++j;
            }

            j = 0; // reset to start of test data
            for (; si < totRows; ++si) // remainder to test data
            {
                testData[j] = new double[numCols];
                int idx = sequence[si];
                Array.Copy(allData[idx], testData[j], numCols);
                ++j;
            }
        }
    }    
}
