using System.Collections.Generic;
using Tactile.TactileMatch3Challenge.Model;

namespace Tactile.TactileMatch3Challenge.Model.Solvers
{
    public interface ISolver
    {
        void Solve(int x, int y, IBoard board, Dictionary<Piece, ChangeInfo> result);
    }
}