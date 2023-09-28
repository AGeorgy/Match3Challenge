using System;
using System.Collections.Generic;
using System.Text;
using Tactile.TactileMatch3Challenge.Model;

namespace Tactile.TactileMatch3Challenge.Level
{
    public class GameLevel : IGameLevel
    {
        public event Action<bool> Achieved;
        public event Action<string> InfoUpdated;

        private readonly IGoal[] goals;

        public GameLevel(params IGoal[] goals)
        {
            this.goals = goals;
        }

        public void UpdateLevelStats(Dictionary<Piece, ChangeInfo> solvedData)
        {
            var goalCounter = 0;
            foreach (var goal in goals)
            {
                var achieved = goal.Update(solvedData);
                if (achieved == GoalCondition.Failed)
                {
                    UpdateInfoAndAchieved(false);
                    return;
                }
                else if (achieved == GoalCondition.Achieved)
                {
                    goalCounter++;
                }
            }

            if (goalCounter == goals.Length)
            {
                UpdateInfoAndAchieved(true);
            }

            InfoUpdated?.Invoke(GetGoalsSummary());
        }

        public string GetGoalsSummary()
        {
            StringBuilder summary = new();
            foreach (var goal in goals)
            {
                summary.Append(goal.GetSummary());
            }
            return summary.ToString();
        }

        private void UpdateInfoAndAchieved(bool isAchieved)
        {
            InfoUpdated?.Invoke(GetGoalsSummary());
            Achieved?.Invoke(isAchieved);
        }

        internal void Reset()
        {
            foreach (var goal in goals)
            {
                goal.Reset();
            }
        }
    }
}