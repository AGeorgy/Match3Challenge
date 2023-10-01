using System.Collections.Generic;
using Tactile.TactileMatch3Challenge.Model.Board;

namespace Tactile.TactileMatch3Challenge.Model.Solvers
{
    public class ConnectedSameTypeSolver : BaseSolver
    {
        override protected List<Piece> SearchForConnected(IBoard board, Piece piece, List<Piece> searched)
        {
            if (!board.TryGetPiecePos(piece, out int x, out int y))
            {
                return searched;
            }

            searched.Add(piece);
            var neighbors = GetNeighbors(board, x, y);

            if (neighbors.Length == 0)
            {
                return searched;
            }

            for (int i = 0; i < neighbors.Length; i++)
            {

                var neighbor = neighbors[i];
                if (!searched.Contains(neighbor) && neighbor.Type.Equals(piece.Type))
                {
                    SearchForConnected(board, neighbor, searched);
                }
            }

            return searched;
        }

        private Piece[] GetNeighbors(IBoard board, int x, int y)
        {
            var neighbors = new List<Piece>(4);

            AddNeighbor(board, x - 1, y, neighbors); // Left
            AddNeighbor(board, x, y - 1, neighbors); // Top
            AddNeighbor(board, x + 1, y, neighbors); // Right
            AddNeighbor(board, x, y + 1, neighbors); // Bottom

            return neighbors.ToArray();
        }

        private void AddNeighbor(IBoard board, int x, int y, List<Piece> neighbors)
        {
            if (!board.IsWithinBounds(x, y)) return;

            neighbors.Add(board.GetAt(x, y));
        }
    }
}