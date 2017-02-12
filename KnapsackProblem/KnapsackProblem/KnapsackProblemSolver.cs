namespace KnapsackProblem
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class KnapsackProblemSolver
    {
        private const int GenerationsLimit = 10000;
        private const int PopulationSize = 100;
        private const double MutationProbability = 0.05;

        private List<string> population;

        private Knapsack knapsack;

        private Random random;

        public KnapsackProblemSolver(Knapsack knapsack)
        {
            this.knapsack = knapsack;
            this.population = new List<string>();
            this.random = new Random();
        }        

        public void Solve()
        {
            int generations = 0;
            this.GeneratePopulation();
            this.SortByFitnessFunction();
            while (true)
            {
                if (generations >= GenerationsLimit)
                {
                    if (this.GetBestFitness() == 0)
                    {
                        generations = 0;
                    }
                    else {
                        Console.WriteLine(this.GetBestSolution());
                        return;
                    }
                }

                if (generations == 10 || generations == 1000 || generations == 2500 || generations == 5000)
                {
                    if (this.GetBestFitness() == 0)
                    {
                        Console.WriteLine("No good solution yet!");
                    }
                    else {
                        Console.WriteLine(this.GetBestSolution());
                    }
                }

                this.ApplySelection();
                this.ApplyCrossover();
                this.SortByFitnessFunction();

                generations++;
            }
        }

        private int GetBestFitness()
        {
            string bestChromosome = this.population[this.population.Count - 1];
            int bestFitnessValue = this.CalculateFitnessForChromosome(bestChromosome);
            return bestFitnessValue;
        }

        private int GetBestSolution()
        {
            int bestCost = 0;
            string bestChromosome = this.population[this.population.Count - 1];

            for (int i = 0; i < this.knapsack.Items.Count; i++)
            {
                char gene = bestChromosome[i];
                if (gene == '1')
                {
                    bestCost += this.knapsack.Items[i].Cost;
                }
            }

            return bestCost;
        }

        private int CalculateFitnessForChromosome(string chromosome)
        {
            int totalWeight = 0;
            int totalCost = 0;
            int totalCount = 0;
            int fitnessValue = 0;
            for (int i = 0; i < this.knapsack.Items.Count; i++)
            {
                char gene = chromosome[i];

                if (gene == '0')
                {
                    continue;
                }

                totalCount++;
                totalWeight += this.knapsack.Items[i].Weight;
                totalCost += this.knapsack.Items[i].Cost;
            }

            if (totalWeight <= this.knapsack.Capacity && totalCount <= this.knapsack.MaxNumberOfItems)
            {
                fitnessValue = totalCost;
            }

            return fitnessValue;
        }

        private void SortByFitnessFunction()
        {
            this.population.Sort((ch1, ch2) => this.CalculateFitnessForChromosome(ch1).CompareTo(this.CalculateFitnessForChromosome(ch2)));
        }

        private void ApplySelection()
        {
            for (int i = 0; i < (int)(0.2 * PopulationSize); i++)
            {
                this.population.RemoveAt(i);
            }
        }

        private void ApplyCrossover()
        {
            int randomIndex = this.random.Next(0, this.knapsack.Items.Count - 1) + 1;

            while (this.population.Count < PopulationSize)
            {
                string firstParent = this.SelectChromosome();
                string secondParent = this.SelectChromosome();

                while (secondParent == firstParent)
                {
                    secondParent = this.SelectChromosome();
                }

                string firstChild = firstParent.Substring(0, randomIndex) + secondParent.Substring(randomIndex);
                firstChild = this.Mutate(firstChild);
                this.population.Add(firstChild);
                string secondChild = secondParent.Substring(0, randomIndex) + firstParent.Substring(randomIndex);
                secondChild = this.Mutate(secondChild);
                if (this.population.Count < PopulationSize)
                {
                    this.population.Add(secondChild);
                }
            }
        }

        private string SelectChromosome()
        {
            return this.population[this.random.Next(0, this.population.Count)];
        }

        private string Mutate(string chromosome)
        {
            string newChromosome;
            double probability = this.random.NextDouble();

            if (probability <= MutationProbability)
            {
                int randomIndex = this.random.Next(0, this.knapsack.Items.Count);
                char gene = chromosome[randomIndex];
                char newGene = gene == '0' ? '1' : '0';
                newChromosome = chromosome.Substring(0, randomIndex) + newGene + chromosome.Substring(randomIndex + 1);
                return newChromosome;
            }

            return chromosome;
        }

        private void GeneratePopulation()
        {
            for (int i = 0; i < PopulationSize; i++)
            {
                string chromosome = this.GenerateChromosome();
                this.population.Add(chromosome);
            }
        }

        private string GenerateChromosome()
        {
            StringBuilder chromosome = new StringBuilder(this.knapsack.Items.Count);

            char gene;
            for (int i = 0; i < this.knapsack.Items.Count; i++)
            {
                double r = this.random.NextDouble();
                gene = r <= 0.8 ? '0' : '1'; // greater P for '0' in the chromosome
                chromosome.Append(gene);
            }

            return chromosome.ToString();
        }
    }
}
