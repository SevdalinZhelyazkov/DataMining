namespace NQueens
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class NQueensSolver
    {
        private const char Queen = '*';
        private const char EmptyCell = '-';

        private int size;
        private int[] board;
        private int maxSteps;

        private Random random;

        private List<int> queensWithMaxConflicts;
        private List<int> rowsWithMinConflicts;

        public NQueensSolver(int size)
        {
            this.size = size;
            this.board = new int[size];
            this.maxSteps = 2 * size;

            this.random = new Random();

            this.queensWithMaxConflicts = new List<int>();
            this.rowsWithMinConflicts = new List<int>();
            this.InitializeRandomBoard();
        }

        public void Solve()
        {
            int steps = 0;
            while (true)
            {
                int maxConflicts = this.GetMaxConflicts();
                if (maxConflicts == 0)
                {
                    //this.PrintSolution();
                    return;
                }

                int col = this.queensWithMaxConflicts[this.random.Next(this.queensWithMaxConflicts.Count)];
                this.GetMinConflictsRows(col);

                if (this.rowsWithMinConflicts.Count > 0)
                {
                    int row = this.rowsWithMinConflicts[this.random.Next(this.rowsWithMinConflicts.Count)];
                    this.board[col] = row;
                }

                if (steps >= this.maxSteps)
                {
                    this.InitializeRandomBoard();
                    steps = 0;
                }

                steps++;
            }
        }

        private void PrintSolution()
        {
            for (int row = 0; row < this.size; row++)
            {
                for (int col = 0; col < this.size; col++)
                {
                    if (this.board[col] == row)
                    {
                        Console.Write("{0, 2}", Queen);
                    }
                    else
                    {
                        Console.Write("{0, 2}", EmptyCell);
                    }
                }

                Console.WriteLine();
            }            
        }

        private void GetMinConflictsRows(int col)
        {
            int minConflicts = this.size;
            this.rowsWithMinConflicts.Clear();
            for (int row = 0; row < this.size; row++)
            {
                int conflicts = this.GetConflictsForQueen(row, col);
                if (conflicts < minConflicts)
                {
                    minConflicts = conflicts;
                    this.rowsWithMinConflicts.Clear();
                    this.rowsWithMinConflicts.Add(row);
                }
                else if (conflicts == minConflicts)
                {
                    this.rowsWithMinConflicts.Add(row);
                }
            }
        }

        private int GetMaxConflicts()
        {
            int maxConflicts = 0;
            queensWithMaxConflicts.Clear();
            for (int col = 0; col < this.size; col++)
            {
                int conflicts = this.GetConflictsForQueen(this.board[col], col);
                if (conflicts > maxConflicts)
                {
                    maxConflicts = conflicts;
                    queensWithMaxConflicts.Clear();
                    queensWithMaxConflicts.Add(col);
                }
                else if (conflicts == maxConflicts)
                {
                    queensWithMaxConflicts.Add(col);
                }
            }

            return maxConflicts;
        }

        private int GetConflictsForQueen(int queenRow, int queenCol)
        {
            int sum = 0;
            for (int col = 0; col < this.board.Length; col++)
            {
                if (col == queenCol)
                {
                    continue;
                }

                if (this.board[col] == queenRow || 
                    Math.Abs(this.board[col] - queenRow) == Math.Abs(col - queenCol))
                {
                    sum++;
                }
            }

            return sum;
        }

        private void InitializeRandomBoard()
        {
            this.board = Enumerable.Range(0, this.size).OrderBy(r => this.random.Next()).ToArray();
        }
    }
}
