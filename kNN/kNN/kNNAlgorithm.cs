namespace kNN
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Globalization;
    using System.Threading;
    public class kNNAlgorithm
    {

        private const int TestSetCount = 20;

        private int k;

        private IList<Instance> dataset;
        private IList<Instance> testSet;
        private IList<Instance> trainingSet;

        private Random random;

        public kNNAlgorithm(int k)
        {
            this.k = k;
            this.dataset = new List<Instance>();
            this.testSet = new List<Instance>();
            this.trainingSet = new List<Instance>();
            this.random = new Random();
        }

        public void Classify()
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
            this.LoadDataset("../../iris-data.txt");
            this.InitalizeTestAndTrainingSet();

            foreach (var testInstance in this.testSet)
            {
                string predictedClass = Classify(testInstance);
                testInstance.PredictedClass = predictedClass;
                Console.WriteLine("Predicted class - {0}; Actual class - {1}", predictedClass, testInstance.ActualClass);
            }

            Console.WriteLine("Accuracy - {0}%", this.CalculateAccuracy());
        }

        private string Classify(Instance testInstance)
        {
            var kNearestNeighbors = this.GetKNearestNeighbors(testInstance);

            var votesCounter = new Dictionary<string, int>();

            foreach (var pair in kNearestNeighbors)
            {
                string vote = this.trainingSet[pair.Key].ActualClass;
                if (votesCounter.ContainsKey(vote))
                {
                    votesCounter[vote]++;
                }
                else
                {
                    votesCounter[vote] = 1;
                }
            }

            int maxVotes = 0;
            string predictedClass = "";
            foreach (var pair in votesCounter)
            {
                int votes = pair.Value;
                if (votes > maxVotes)
                {
                    maxVotes = votes;
                    predictedClass = pair.Key;
                }
            }

            return predictedClass;
        }

        private Dictionary<int, double> GetKNearestNeighbors(Instance testInstance)
        {
            var distances = new Dictionary<int, double>();
            for (int i = 0; i < this.trainingSet.Count; i++)
            {
                Instance trainingInstance = this.trainingSet[i];
                double distance = this.EuclideanDistance(testInstance, trainingInstance);
                distances[i] = distance;
            }

            var kNearestNeighbors = distances.OrderBy(p => p.Value).Take(this.k).ToDictionary(p => p.Key, p => p.Value);
            return kNearestNeighbors;
        }

        private double EuclideanDistance(Instance first, Instance second)
        {
            double distance = Math.Sqrt(Math.Pow(second.SepalLength - first.SepalLength, 2) +
                Math.Pow(second.SepalWidth - first.SepalWidth, 2) +
                Math.Pow(second.PetalLength - first.PetalLength, 2) +
                Math.Pow(second.PetalWidth - first.PetalWidth, 2));
            return distance;

        }

        private double CalculateAccuracy()
        {
            int accuratePredictionsCount = 0;
            foreach (var testInstance in this.testSet)
            {
                if (testInstance.PredictedClass == testInstance.ActualClass)
                {
                    accuratePredictionsCount++;
                }
            }

            double accuracy = ((double)accuratePredictionsCount / this.testSet.Count) * 100;
            return accuracy;
        }

        private void LoadDataset(string filename)
        {
            HashSet<string> normalizedDataset = new HashSet<string>();
            string[] lines = File.ReadAllLines(filename);
            foreach (var line in lines)
            {
                if (!normalizedDataset.Contains(line))
                {
                    normalizedDataset.Add(line);
                    string[] splittedLine = line.Split(',');
                    Instance instance = new Instance(double.Parse(splittedLine[0]),
                        double.Parse(splittedLine[1]),
                        double.Parse(splittedLine[2]),
                        double.Parse(splittedLine[3]),
                        splittedLine[4]);
                    this.dataset.Add(instance);
                }
            }
        }

        private void InitalizeTestAndTrainingSet()
        {
            HashSet<int> randomIndexes = new HashSet<int>();
            for (int i = 0; i < TestSetCount; i++)
            {
                int randomIndex = random.Next(0, this.dataset.Count);
                while (randomIndexes.Contains(randomIndex))
                {
                    randomIndex = random.Next(0, this.dataset.Count);
                }

                randomIndexes.Add(randomIndex);
            }

            for (int i = 0; i < this.dataset.Count; i++)
            {
                if (randomIndexes.Contains(i))
                {
                    this.testSet.Add(this.dataset[i]);
                }
                else
                {
                    this.trainingSet.Add(this.dataset[i]);
                }
            }
        }
    }
}
