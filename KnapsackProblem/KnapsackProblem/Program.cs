namespace KnapsackProblem
{
    using System;

    public class Program
    {
        public static void Main()
        {
            Console.WriteLine("Enter M and N");
            string[] knapsackParts = Console.ReadLine().Split(' ');
            int M = int.Parse(knapsackParts[0]);
            int N = int.Parse(knapsackParts[1]);

            Knapsack knapsack = new Knapsack(M, N);

            Console.WriteLine("Enter {0} items with cost and weight", N);
            for (int i = 0; i < N; i++)
            {
                string[] itemParts = Console.ReadLine().Split(' ');
                int cost = int.Parse(itemParts[0]);
                int weight = int.Parse(itemParts[1]);
                Item item = new Item(cost, weight);
                knapsack.AddItem(item);
            }

            Console.WriteLine();

            KnapsackProblemSolver solver = new KnapsackProblemSolver(knapsack);
            solver.Solve();
        }
    }
}
