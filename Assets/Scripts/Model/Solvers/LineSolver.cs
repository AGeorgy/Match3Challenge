using System.Collections.Generic;

namespace Tactile.TactileMatch3Challenge.Model.Solvers
{
    public abstract class LineSolver : BaseSolver
    {
        protected override List<Piece> SearchForConnected(IBoard board, Piece piece, List<Piece> searched)
        {
            if (!board.TryGetPiecePos(piece, out int x, out int y))
            {
                return searched;
            }

            AddLine(board, x, y, searched);

            return searched;
        }

        protected virtual void AddLine(IBoard board, int x, int y, List<Piece> searched)
        {
        }
    }
}