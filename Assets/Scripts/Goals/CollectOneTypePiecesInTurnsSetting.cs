using System;
using Tactile.TactileMatch3Challenge.Level;
using UnityEngine;

namespace Tactile.TactileMatch3Challenge.Goals
{
    [Serializable]
    public class CollectOneTypePiecesInTurnsSetting : IGoalSetting
    {
        [SerializeField] private int maxTurns;
        [SerializeField] private int pieceType;
        [SerializeField] private int collectPieces;
        [SerializeField][TextArea(2, 20)] private string description;

        public IGoal GetGoal()
        {
            return new CollectOneTypePiecesInTurnsGoal(maxTurns, pieceType, collectPieces, description);
        }
    }
}