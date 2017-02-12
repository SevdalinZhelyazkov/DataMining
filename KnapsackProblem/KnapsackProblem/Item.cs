namespace KnapsackProblem
{
    public class Item
    {
        public Item(int cost, int weight)
        {
            this.Cost = cost;
            this.Weight = weight;
        }

        public int Cost { get; set; }

        public int Weight { get; set; }
    }
}
