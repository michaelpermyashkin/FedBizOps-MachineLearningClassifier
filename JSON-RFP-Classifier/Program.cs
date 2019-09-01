using Microsoft.ML;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;


namespace JSON_RFP_Classifier
{
    public static class Program
    {
        private static string _appPath => Path.GetDirectoryName(Environment.GetCommandLineArgs()[0]);
        private static string _modelPath => Path.Combine(_appPath, "..", "..", "..", "Models", "model.zip");

        public static void Main(string[] args)
        {
            /* Load directory containing training data
               iterate through all files and save model */
            Console.WriteLine("Loading training data...");

            TermCompiler TermsList = new TermCompiler(); // This instantiates class that can compile list of data for NER Tagging
            string[] FileEntries = Directory.GetFiles("../../../Data"); // The Data folder in the solution directory

            // These values will print as training occures to show how many files have been processed of total
            int counter = 1;
            int totalFiles = FileEntries.Length;

            foreach (string Filename in FileEntries)
            {
                // Uncomment the following to train the model on the data in the Data directory
                // TrainModel(Filename, counter, totalFiles);

                // Will run through all the data and create a file with unique entries found in the data set (i.e. it will only write something once)
                // 'TermCompiler' class can be modified to output whichever fields you want a list of (Agencies, Offices, Naics...)
                TermsList.TermListCompiler(Filename); 

                counter++;
            }

            // This is an example of how you can test the model's accuracy with predictions
            var labelService = new RFPClassifierLabel();
            labelService.LoadModel(_modelPath);
            Console.WriteLine("------------------------------------------------------");
            Console.WriteLine("Predict some Offices based on other fields provided...");
            Console.WriteLine("------------------------------------------------------");
            Console.WriteLine();

            // Should be --> DLA Acquisition Locations
            MakePrediction(labelService, "Defense Logistics Agency", "15", "336413", "1560; PANEL STRUCTURAL, AI; T-38 ACFT; WSDC: 42(F); WSIC: T", "SPE4A719R0987", "N/A");

            //I'm working on writing some code that can use built in algorithms + a unique testing dataset to show some accuracy metrics
        }

        private static void TrainModel(string fileName, int counter, int totalFiles)
        {
            Console.WriteLine("Training the model...\n\n");
            Console.WriteLine("Processing... " + counter + " / " + totalFiles);

            List<RFPData> trainingData = GetTrainingData(fileName);

            var trainingService = new RFPClassifierTraining();
            trainingService.Train(trainingData, _modelPath);            
        }

        private static List<RFPData> GetTrainingData(string path)
        {
            return JsonConvert.DeserializeObject<List<RFPData>>(File.ReadAllText(path));
        }

        private static void MakePrediction(RFPClassifierLabel labelService, string Agency, string ClassCode, string Naics, string Subject, string SolNbr, string SetAside)
        {
            string prediction = labelService.PredictCategory(new RFPData
            {
                Agency = Agency,
                ClassCode = ClassCode,
                Naics = Naics,
                Subject = Subject,
                SolNbr = SolNbr,
                SetAside = SetAside
            });

            Console.WriteLine($"Given Agency: {Agency} => Predicted: {prediction}");
        }
    }
}
