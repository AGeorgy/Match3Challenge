using System.Collections.Generic;
using Tactile.TactileMatch3Challenge.Model;

namespace Tactile.TactileMatch3Challenge.Solvers
{
    public class VerticalLineSolver : LineSolver
    {
        protected override void AddLine(IBoard board, int x, int y, List<Piece> searched)
        {
            for (int i = 0; i < board.Height; i++)
            {
                searched.Add(board.GetAt(x, i));
            }
        }
    }
}