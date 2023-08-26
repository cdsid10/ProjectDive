namespace PersonalSpace.Sid.Scripts.Interactions.PuzzlePieces
{
    public interface IPuzzlePieces
    {
        public bool IsPuzzlePieceEnabled { get; set; }
        public void PerformAction();
    }
}