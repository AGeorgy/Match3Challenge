using System;
using System.Collections.Generic;
using System.Text;
using Tactile.TactileMatch3Challenge.Model;

namespace Tactile.TactileMatch3Challenge.Level
{
    public class GameLevel : IUpdateLevelStats, IGetGoalsSummary, IGameLevelAchieved, IIsAchieved, IGameLevelReset
    {
        public event Action Achieved;

        private readonly IGoal[] goals;
        private bool isAchieved;

        public bool IsAchieved => isAchieved;

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

        public void Reset()
        {
            foreach (var goal in goals)
            {
                goal.Reset();
            }
        }

        private void UpdateInfoAndAchieved(bool isAchieved)
        {
            this.isAchieved = isAchieved;
            Achieved?.Invoke();
        }
    }
}