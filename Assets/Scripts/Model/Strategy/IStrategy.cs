using System.Collections.Generic;
using Tactile.TactileMatch3Challenge.Model.PieceGenerators;
using Tactile.TactileMatch3Challenge.PieceSpawn;

namespace Tactile.TactileMatch3Challenge.Model.Strategy
{
    public interface IStrategy : IBaseStrategy
    {
        void Fill(IBoard board, Dictionary<Piece, ChangeInfo> result);
        bool Solve(int x, int y, IBoard board, Dictionary<Piece, ChangeInfo> result);
    }

    public interface IBaseStrategy
    {
        IGetValidIndex IndexValidator { get; }
        IGetVisualPiece VisualPieceProvider { get; }
        void Reset();
    }
}