using System.Collections.Generic;
using Tactile.TactileMatch3Challenge.Model;

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

            neighbors = AddNeighbor(board, x - 1, y, neighbors); // Left
            neighbors = AddNeighbor(board, x, y - 1, neighbors); // Top
            neighbors = AddNeighbor(board, x + 1, y, neighbors); // Right
            neighbors = AddNeighbor(board, x, y + 1, neighbors); // Bottom

            return neighbors.ToArray();
        }

        private List<Piece> AddNeighbor(IBoard board, int x, int y, List<Piece> neighbors)
        {
            if (!board.IsWithinBounds(x, y)) return neighbors;

            neighbors.Add(board.GetAt(x, y));
            return neighbors;
        }
    }
}