namespace KnapsackProblem
{
    using System.Collections.Generic;

    public class Knapsack
    {
        public Knapsack(int M, int N)
        {
            this.Capacity = M;
            this.MaxNumberOfItems = N;
            this.Items = new List<Item>();
        }

        public int Capacity { get; private set; }

        public int MaxNumberOfItems { get; private set; }

        public List<Item> Items { get; private set; }

        public void AddItem(Item item)
        {
            this.Items.Add(item);
        }
    }
}
