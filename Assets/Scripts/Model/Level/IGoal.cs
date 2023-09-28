
using System.Collections.Generic;
using System.Text;
using Tactile.TactileMatch3Challenge.Model;

namespace Tactile.TactileMatch3Challenge.Level
{
    public interface IGoal
    {
        StringBuilder GetSummary();
        void Reset();
        GoalCondition Update(Dictionary<Piece, ChangeInfo> solvedData);
    }
}