using System.Collections.Generic;
using Tactile.TactileMatch3Challenge.Model;
using Tactile.TactileMatch3Challenge.Level;
using System.Text;

namespace Tactile.TactileMatch3Challenge.Tests.UnitTests
{
    public class GoalFake : IGoal
    {
        private readonly GoalCondition condition;
        private readonly string summary;

        public GoalFake(GoalCondition condition, string summary)
        {
            this.condition = condition;
            this.summary = summary;
        }

        public GoalCondition Update(Dictionary<Piece, ChangeInfo> solvedData)
        {
            return condition;
        }

        public StringBuilder GetSummary()
        {
            return new StringBuilder(summary);
        }

        public void Reset()
        {
        }
    }
}