using System.Collections.Generic;
using Tactile.TactileMatch3Challenge.Model;

namespace Tactile.TactileMatch3Challenge.Solvers
{
    public class HorizontalLineSolver : LineSolver
    {
        protected override void AddLine(IBoard board, int x, int y, List<Piece> searched)
        {
            for (int i = 0; i < board.Width; i++)
            {
                searched.Add(board.GetAt(i, y));
            }
        }
    }
}