using System;
using System.Collections.Generic;
using Tactile.TactileMatch3Challenge.Model;

namespace Tactile.TactileMatch3Challenge.Level
{
    public interface IGameLevel
    {
        event Action<bool> Achieved;
        event Action<string> InfoUpdated;
        void UpdateLevelStats(Dictionary<Piece, ChangeInfo> solvedData);
        string GetGoalsSummary();
    }
}