namespace FrogsPuzzle
{
    public class PuzzleState
    {
        public PuzzleState(string state)
        {
            this.State = state;
        }

        public string State { get; set; }

        public PuzzleState PreviousState { get; set; }

        public override string ToString()
        {
            return this.State;
        }

        public override int GetHashCode()
        {
            return this.State.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            PuzzleState other = obj as PuzzleState;
            return this.State.Equals(other.State);
        }
    }
}
