using System;

namespace SlidingBlocks
{
    public class PuzzleState : IComparable<PuzzleState>
    {
        public PuzzleState(string position)
        {
            this.Position = position;
        }

        public string Position { get; set; }

        public double F { get; set; }

        public double G { get; set; }

        public double H { get; set; }

        public string Direction { get; set; }

        public PuzzleState Parent { get; set; }

        public int CompareTo(PuzzleState other)
        {
            return this.F.CompareTo(other.F);
        }

        public override int GetHashCode()
        {
            return this.Position.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            PuzzleState other = obj as PuzzleState;
            return this.Position.Equals(other.Position);
        }    
    }
}
