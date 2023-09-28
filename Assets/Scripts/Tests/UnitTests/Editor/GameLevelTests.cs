using NUnit.Framework;
using System.Collections.Generic;
using Tactile.TactileMatch3Challenge.Model;
using Tactile.TactileMatch3Challenge.Level;

namespace Tactile.TactileMatch3Challenge.Tests.UnitTests
{
    public class GameLevelTests
    {
        [TestCase(GoalCondition.Achieved, ExpectedResult = GoalCondition.Achieved, TestName = "Achieved")]
        [TestCase(GoalCondition.Failed, ExpectedResult = GoalCondition.Failed, TestName = "Failed")]
        [TestCase(GoalCondition.NotAchieved, ExpectedResult = GoalCondition.NotAchieved, TestName = "NotAchieved")]
        public GoalCondition UpdateLevelStats_AllGoalsAchieved_AchievedEventFired(GoalCondition condition)
        {
            // Arrange
            var level = new GameLevel(new GoalFake(condition, ""));
            var solvedData = new Dictionary<Piece, ChangeInfo>
            {
                { new Piece { type = 0 }, new ChangeInfo { } }
            };

            var achievedEventFired = GoalCondition.NotAchieved;
            level.Achieved += (isAchieved) => achievedEventFired = isAchieved ? GoalCondition.Achieved : GoalCondition.Failed;

            // Act
            level.UpdateLevelStats(solvedData);

            // Assert
            return achievedEventFired;
        }

        [Test]
        public void GetGoalsSummary_ReturnsCorrectSummary()
        {
            // Arrange
            var summary = "Summary of goals";
            var level = new GameLevel(new GoalFake(GoalCondition.Achieved, summary));

            // Act
            var summaryResult = level.GetGoalsSummary();

            // Assert
            Assert.AreEqual(summaryResult, summary);
        }
    }
}