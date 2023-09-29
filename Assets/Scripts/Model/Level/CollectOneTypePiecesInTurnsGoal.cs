using System.Collections.Generic;
using System.Text;
using Tactile.TactileMatch3Challenge.Model;

namespace Tactile.TactileMatch3Challenge.Level
{
    public class CollectOneTypePiecesInTurnsGoal : IGoal
    {
        private readonly CollectOneTypePiecesInTurnsSetting setting;
        private int turns;
        private int collectedPiecesCounter;

        public CollectOneTypePiecesInTurnsGoal(CollectOneTypePiecesInTurnsSetting setting)
        {
            this.setting = setting;

            Reset();
        }

        public StringBuilder GetSummary()
        {
            StringBuilder summary = new();
            summary.AppendLine($"Turns: {turns} / {setting.MaxTurns}");
            summary.AppendLine($"Collected: {collectedPiecesCounter} / {setting.CollectPieces}");
            summary.AppendLine(setting.Description);
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
                if (pieceInfo.Key.Type.Equals(setting.PieceType) && pieceInfo.Value.Change == ChangeType.Removed)
                {
                    collectedPiecesCounter++;
                }
            }

            if (collectedPiecesCounter >= setting.CollectPieces)
            {
                return GoalCondition.Achieved;
            }
            else if (turns >= setting.MaxTurns)
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