namespace SlidingBlocks
{
    using System;
    using System.Collections.Generic;

    public class SlidingBlocksPuzzleSolver
    {
        private const string DirectionLeft = "left";
        private const string DirectionRight = "right";
        private const string DirectionUp = "up";
        private const string DirectionDown = "down";

        private PuzzleState initialState;
        private PuzzleState targetState;
        private int size;

        public SlidingBlocksPuzzleSolver(int n, string initialPosition)
        {
            this.initialState = new PuzzleState(initialPosition);
            this.targetState = new PuzzleState(this.InitializeTargetState(n));
            this.size = (int)Math.Sqrt(n + 1);
        }

        public void Solve()
        {
            Console.WriteLine("Started solving the puzzle...");

            HashSet<PuzzleState> exploredStates = new HashSet<PuzzleState>();
            PriorityQueue<PuzzleState> unexploredStatesQueue = new PriorityQueue<PuzzleState>();

            this.initialState.G = 0;
            this.initialState.H = this.Heuristic(this.initialState);
            this.initialState.F = this.initialState.H;
            unexploredStatesQueue.Enqueue(this.initialState);

            while (unexploredStatesQueue.Count != 0)
            {
                PuzzleState currentState = unexploredStatesQueue.Dequeue();

                if (currentState.Position == this.targetState.Position)
                {
                    this.PrintSolution(currentState);
                    break;
                }

                exploredStates.Add(currentState);
                var nextStates = this.GenerateNextStates(currentState);
                foreach (var childState in nextStates)
                {
                    if (exploredStates.Contains(childState))
                    {
                        continue;
                    }

                    childState.G = currentState.G + 1;
                    childState.H = this.Heuristic(childState);
                    childState.F = childState.G + childState.H;
                    childState.Parent = currentState;
                    unexploredStatesQueue.Enqueue(childState);
                }
            }
        }

        private void PrintSolution(PuzzleState puzzleState)
        {
            Stack<PuzzleState> result = new Stack<PuzzleState>();
            result.Push(puzzleState);
            while (puzzleState.Parent != null)
            {
                puzzleState = puzzleState.Parent;
                result.Push(puzzleState);
            }

            int steps = 0;
            result.Pop();
            while (result.Count != 0)
            {
                Console.WriteLine(result.Pop().Direction);
                steps++;
            }

            Console.WriteLine("Total steps: {0}", steps);
        }

        private List<PuzzleState> GenerateNextStates(PuzzleState puzzleState)
        {
            var nextStates = new List<PuzzleState>();

            int zeroIndex = puzzleState.Position.IndexOf('0');
            int zeroX = zeroIndex / this.size;
            int zeroY = zeroIndex % this.size;

            if (zeroX > 0)
            {
                int targetIndex = zeroIndex - this.size;
                PuzzleState nextState = this.MakeMove(puzzleState, zeroIndex, targetIndex);
                nextState.Direction = DirectionDown;

                nextStates.Add(nextState);
            }

            if (zeroX < this.size - 1)
            {
                int targetIndex = zeroIndex + this.size;
                PuzzleState nextState = this.MakeMove(puzzleState, zeroIndex, targetIndex);
                nextState.Direction = DirectionUp;

                nextStates.Add(nextState);
            }

            if (zeroY > 0)
            {
                int targetIndex = zeroIndex - 1;
                PuzzleState nextState = this.MakeMove(puzzleState, zeroIndex, targetIndex);
                nextState.Direction = DirectionRight;

                nextStates.Add(nextState);
            }

            if (zeroY < this.size - 1)
            {
                int targetIndex = zeroIndex + 1;
                PuzzleState nextState = this.MakeMove(puzzleState, zeroIndex, targetIndex);
                nextState.Direction = DirectionLeft;

                nextStates.Add(nextState);
            }

            return nextStates;
        }

        private PuzzleState MakeMove(PuzzleState puzzleState, int i, int j)
        {
            char[] stateArr = puzzleState.Position.ToCharArray();
            char tempValue = stateArr[i];
            stateArr[i] = stateArr[j];
            stateArr[j] = tempValue;
            return new PuzzleState(new string(stateArr));
        }

        private int Heuristic(PuzzleState state)
        {
            int sum = 0;
            for (int i = 0; i < this.targetState.Position.Length; i++)
            {
                char block = this.targetState.Position[i];
                int x = state.Position.IndexOf(block) / this.size;
                int y = state.Position.IndexOf(block) % this.size;
                int targetX = this.targetState.Position.IndexOf(block) / this.size;
                int targetY = this.targetState.Position.IndexOf(block) % this.size;

                sum += this.ManhattanDistance(x, y, targetX, targetY);
            }

            return sum;
        }

        private int ManhattanDistance(int x1, int y1, int x2, int y2)
        {
            int distance = Math.Abs(x1 - x2) + Math.Abs(y1 - y2);
            return distance;
        }

        private string InitializeTargetState(int n)
        {
            char[] targetPositionArr = new char[n + 1];
            for (int i = 0; i < n + 1; i++)
            {
                if (i != n)
                {
                    targetPositionArr[i] = (char)(48 + i + 1);
                }
                else
                {
                    targetPositionArr[i] = '0';
                }
            }

            return new string(targetPositionArr);
        }
    }
}
