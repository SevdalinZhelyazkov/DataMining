namespace NQueens
{
    using System;
    using System.Diagnostics;

    public class Startup
    {
        public static void Main()
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            NQueensSolver solver = new NQueensSolver(1000);
            solver.Solve();

            stopwatch.Stop();
            Console.WriteLine("Execution time: {0}", stopwatch.Elapsed);
        }
    }
}
