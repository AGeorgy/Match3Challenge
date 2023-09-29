using System.Collections.Generic;
using System.Text;
using Tactile.TactileMatch3Challenge.Model;

namespace Tactile.TactileMatch3Challenge.Level
{
    public class CollectOneTypePiecesInTurnsGoal : IGoal
    {
        private readonly int maxTurns;
        private readonly int pieceType;
        private readonly int collectPieces;
        private readonly string description;

        private int turns;
        private int collectedPiecesCounter;

        public CollectOneTypePiecesInTurnsGoal(int maxTurns, int pieceType, int collectPieces, string description)
        {
            this.maxTurns = maxTurns;
            this.pieceType = pieceType;
            this.collectPieces = collectPieces;
            this.description = description;

            Reset();
        }

        public StringBuilder GetSummary()
        {
            StringBuilder summary = new();
            summary.AppendLine($"Turns: {turns} / {maxTurns}");
            summary.AppendLine($"Collected: {collectedPiecesCounter} / {collectPieces}");
            summary.AppendLine(description);
            return summary;
        }

        public void Reset()
        {
            turns = 0;
            collectedPiecesCounter = 0;
        }

        public GoalCondition Update(Dictionary<Piece, ChangeInfo> solvedData)
        {
            if (solvedData.Count == 0)
                return GoalCondition.NotAchieved;

            turns++;

            foreach (var pieceInfo in solvedData)
            {
                if (pieceInfo.Key.Type.Equals(pieceType) && pieceInfo.Value.Change == ChangeType.Removed)
                {
                    collectedPiecesCounter++;
                }
            }

            if (collectedPiecesCounter >= collectPieces)
            {
                return GoalCondition.Achieved;
            }
            else if (turns >= maxTurns)
            {
                return GoalCondition.Failed;
            }
            else
            {
                return GoalCondition.NotAchieved;
            }
        }
    }
}