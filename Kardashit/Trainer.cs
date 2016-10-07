using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text.RegularExpressions;

namespace Kardashit
{
    class Trainer
    {
        

        static double[][] ReadInputCSV(string file_path, bool ignore_first_line = false)
        {
            var lines = File.ReadAllLines(file_path);
            var list = new double[ignore_first_line ? lines.Length - 1 : lines.Length][];
            int j = 0;

            foreach (var line in lines)
            {
                if (ignore_first_line)
                {
                    ignore_first_line = false;
                    continue;
                }

                var t = Regex.Split(line, ",");



                var data = new double[t.Length];

                for (int i = 0; i < t.Length; ++i)
                    data[i] = double.Parse(t[i], System.Globalization.CultureInfo.InvariantCulture);

                list[j++] = data;
            }

            return list;
        }

        static void WriteWeightFile(double[] weights, string file_path)
        {
            using (Stream stream = File.Open(file_path, FileMode.Create))
            {
                BinaryFormatter bformatter = new BinaryFormatter();
                bformatter.Serialize(stream, weights);
            }
        }

        static void Main(string[] args)
        {
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
            var allData = Common.ParseClothesFile("C:\\Users\\xerno\\Documents\\learning\\Sonae-Challenge-Rage-Against-the-Machine\\1_pixelcamp_train_test.csv");
            Console.WriteLine("Input data read, " + allData.Length + " lines.");

            double[][] trainData = null;
            double[][] testData = null;
            MakeTrainTest(allData, out trainData, out testData);//, 50);

            //Console.WriteLine("\nFirst 5 rows of training data:");
            //ShowMatrix(trainData, 5, 1, true);
            //Console.WriteLine("First 3 rows of test data:");
            //ShowMatrix(testData, 3, 1, true);

            //Normalize(trainData, new int[] { 0, 1, 2, 3 });
            //Normalize(testData, new int[] { 0, 1, 2, 3 });
            Common.Normalize(trainData, new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16 });
            Common.Normalize(testData, new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16 });

            //Console.WriteLine("\nFirst 5 rows of normalized training data:");
            //ShowMatrix(trainData, 5, 1, true);
            //Console.WriteLine("First 3 rows of normalized test data:");
            //ShowMatrix(testData, 3, 1, true);

            int numOutput = 2;
            int numHidden = 23;
            int numInput = allData[0].Length - numOutput;

            Console.WriteLine("\nCreating a {0}-input, {1}-hidden, {2}-output neural network", numInput, numHidden, numOutput);
            Console.WriteLine("Hard-coded tanh function for input-to-hidden and softmax for hidden-to-output activations");

            NeuralNetwork nn = new NeuralNetwork(numInput, numHidden, numOutput);

            Console.WriteLine("\nInitializing weights and bias to small random values");
            nn.InitializeWeights();

            int maxEpochs = 2000;
            double learnRate = 0.025;
            double momentum = 0.01;
            double weightDecay = 0.0001;
            double minMse = 0.00001;// double.NegativeInfinity;
            Console.WriteLine("Setting maxEpochs = {0}, learnRate = {1}, momentum = {2}, weightDecay = {3}, error = {4}", maxEpochs, learnRate, momentum, weightDecay, minMse);

            Console.WriteLine("\nBeginning training using incremental back-propagation\n");
            nn.Train(trainData, maxEpochs, learnRate, momentum, weightDecay, minMse);
            Console.WriteLine("Training complete");

            double[] weights = nn.GetWeights();
            WriteWeightFile(weights, "kardashit.weights");

            Console.WriteLine("Final neural network weights and bias values:");
            Common.ShowVector(weights, 10, 8, true);

            double trainAcc = nn.Accuracy(trainData);
            Console.WriteLine("\nAccuracy on training data = " + trainAcc.ToString("F4"));

            double testAcc = nn.Accuracy(testData);
            Console.WriteLine("\nAccuracy on test data = " + testAcc.ToString("F4"));

            Console.WriteLine("\nDone\n");
            Console.ReadLine();
        }

        static void MakeTrainTest(double[][] allData, out double[][] trainData, out double[][] testData)
        {
            // split allData into 80% trainData and 20% testData
            Random rnd = new Random(0);
            int totRows = allData.Length;
            int numCols = allData[0].Length;

            int trainRows = (int)(totRows * 0.95); // hard-coded 80-20 split
            int testRows = totRows - trainRows;

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

        /*static void MakeTrainTest(double[][] allData, out double[][] trainData, out double[][] testData, int testRows = 1)
        {
            Random rnd = new Random(0);
            int totRows = allData.Length;
            int numCols = allData[0].Length;

            Console.WriteLine("Rows: " + totRows);
            Console.WriteLine("Cols: " + numCols);

            int trainRows = allData.Length - testRows;
            int trainRows = (int)(totRows * 0.80); // hard-coded 80-20 split

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
        }*/
    }    
}
