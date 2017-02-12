namespace SlidingBlocks
{
    using System;

    public class Program
    {
        public static void Main()
        {
            SlidingBlocksPuzzleSolver solver = new SlidingBlocksPuzzleSolver(8, "861307245");
            solver.Solve();

            Console.WriteLine();

            //SlidingBlocksPuzzleSolver solver2 = new SlidingBlocksPuzzleSolver(8, "653248701");
            //solver2.Solve();

            //Console.WriteLine();

            //SlidingBlocksPuzzleSolver solver3 = new SlidingBlocksPuzzleSolver(8, "236158470");
            //solver3.Solve();
        }
    }
}
