using System.Collections.Generic;
using Tactile.TactileMatch3Challenge.Model;

namespace Tactile.TactileMatch3Challenge.Level
{
    public interface IUpdateLevelStats
    {
        void UpdateLevelStats(Dictionary<Piece, ChangeInfo> solvedData);
    }
}