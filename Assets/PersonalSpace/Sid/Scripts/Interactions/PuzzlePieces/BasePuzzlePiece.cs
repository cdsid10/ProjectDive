using UnityEngine;

namespace PersonalSpace.Sid.Scripts.Interactions.PuzzlePieces
{
    public abstract class BasePuzzlePiece : MonoBehaviour, IPuzzlePieces
    {
        public abstract bool IsPuzzlePieceEnabled { get; set; }
        public abstract void PerformAction();

    }
}