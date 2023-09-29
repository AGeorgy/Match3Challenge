using System.Collections.Generic;
using Tactile.TactileMatch3Challenge.Model;

namespace Tactile.TactileMatch3Challenge.Model.Solvers
{
    public abstract class BaseSolver : ISolver
    {
        public void Solve(int x, int y, IBoard board, Dictionary<Piece, ChangeInfo> result)
        {
            var connections = GetConnected(board, x, y);
            if (connections.Count > 1)
            {
                RemovePieces(board, connections, result);
            }
        }

        protected void RemovePieces(IBoard board, List<Piece> connections, in Dictionary<Piece, ChangeInfo> removed)
        {
            foreach (var piece in connections)
            {
                if (board.TryGetPiecePos(piece, out int x, out int y))
                {
                    board.RemovePieceAt(x, y);
                    removed.Add(piece, new ChangeInfo()
                    {
                        Change = ChangeType.Removed,
                        CurrPos = new BoardPos(x, y)
                    });
                }
            }
        }

        private List<Piece> GetConnected(IBoard board, int x, int y)
        {
            var start = board.GetAt(x, y);
            return SearchForConnected(board, start, new List<Piece>());
        }

        protected virtual List<Piece> SearchForConnected(IBoard board, Piece piece, List<Piece> searched)
        {
            return new List<Piece>();
        }
    }
}