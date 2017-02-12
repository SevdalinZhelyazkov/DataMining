namespace FrogsPuzzle
{
    using System;
    using System.Collections.Generic;

    public class FrogsPuzzleSolver
    {
        private const char RightFrog = '>';
        private const char LeftFrog = '<';
        private const char EmptyCell = '-';

        private int n;
        private PuzzleState initialState;
        private PuzzleState targetState;

        public FrogsPuzzleSolver(int n)
        {
            this.n = n;
            this.initialState = new PuzzleState(new string(RightFrog, n) + EmptyCell + new string(LeftFrog, n));
            this.targetState = new PuzzleState(new string(LeftFrog, n) + EmptyCell + new string(RightFrog, n));
        }

        public void Solve()
        {
            Stack<PuzzleState> stack = new Stack<PuzzleState>();
            stack.Push(this.initialState);
            HashSet<PuzzleState> visited = new HashSet<PuzzleState>();
            visited.Add(this.initialState);

            while (stack.Count != 0)
            {
                PuzzleState currentState = stack.Pop();
                if (currentState.State == targetState.State)
                {
                    this.PrintSolution(currentState);
                    break;
                }

                List<PuzzleState> nextStates = GenerateNextStates(currentState);
                foreach (var childState in nextStates)
                {
                    if (!visited.Contains(childState))
                    {
                        childState.PreviousState = currentState;
                        stack.Push(childState);
                        visited.Add(childState);
                    }
                }
            }
        }

        private void PrintSolution(PuzzleState puzzleState)
        {
            Stack<PuzzleState> result = new Stack<PuzzleState>();
            result.Push(puzzleState);
            while (puzzleState.PreviousState != null)
            {
                puzzleState = puzzleState.PreviousState;
                result.Push(puzzleState);
            }

            while (result.Count != 0) 
            {
                Console.WriteLine(result.Pop());
            }
        }

        private List<PuzzleState> GenerateNextStates(PuzzleState state)
        {
            var nextStates = new List<PuzzleState>();
            int emptyIndex = state.State.IndexOf(EmptyCell);

            if ((emptyIndex - 1 >= 0) && (state.State[emptyIndex - 1] == RightFrog))
            {
                PuzzleState nextState = MakeMove(state, emptyIndex - 1, emptyIndex);
                nextStates.Add(nextState);
            }

            if ((emptyIndex - 2 >= 0) && (state.State[emptyIndex - 2] == RightFrog))
            {
                PuzzleState nextState = MakeMove(state, emptyIndex - 2, emptyIndex);
                nextStates.Add(nextState);
            }

            if ((emptyIndex + 1 < state.State.Length) && (state.State[emptyIndex + 1] == LeftFrog))
            {
                PuzzleState nextState = MakeMove(state, emptyIndex + 1, emptyIndex);
                nextStates.Add(nextState);
            }

            if ((emptyIndex + 2 < state.State.Length) && (state.State[emptyIndex + 2] == LeftFrog))
            {
                PuzzleState nextState = MakeMove(state, emptyIndex + 2, emptyIndex);
                nextStates.Add(nextState);
            }

            return nextStates;
        }

        private PuzzleState MakeMove(PuzzleState state, int i, int j)
        {
            char[] stateArr = state.State.ToCharArray();
            char tempValue = stateArr[i];
            stateArr[i] = stateArr[j];
            stateArr[j] = tempValue;
            return new PuzzleState(new string(stateArr));
        }

    }
}
