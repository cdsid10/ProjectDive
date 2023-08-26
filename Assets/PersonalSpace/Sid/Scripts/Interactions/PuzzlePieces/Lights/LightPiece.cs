using System;
using UnityEngine;

namespace PersonalSpace.Sid.Scripts.Interactions.PuzzlePieces.Lights
{
    public class LightPiece : BasePuzzlePiece
    {
        [field: SerializeField] public override bool IsPuzzlePieceEnabled { get; set; }

        private Light lightPiece;

        private void Awake()
        {
            lightPiece = gameObject.GetComponent<Light>();
        }

        private void Start()
        {
            lightPiece.enabled = IsPuzzlePieceEnabled;
        }

        public override void PerformAction()
        {
            lightPiece.enabled = IsPuzzlePieceEnabled;

            IsPuzzlePieceEnabled = !IsPuzzlePieceEnabled;
        }
    }
}