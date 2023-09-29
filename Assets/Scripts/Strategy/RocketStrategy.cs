using System.Collections.Generic;
using System.Linq;
using Tactile.TactileMatch3Challenge.Model;
using Tactile.TactileMatch3Challenge.PieceSpawn;

namespace Tactile.TactileMatch3Challenge.Strategy
{
    public class RocketStrategy : BaseStrategy, IStrategy
    {
        public RocketStrategy(IPieceSpawner pieceSpawner) : base(pieceSpawner)
        {
        }

        public void Fill(IBoard board, Dictionary<Piece, ChangeInfo> result)
        {
            var removed = GetRemovedPieces(result);
            if (removed.Count < 5)
            {
                return;
            }

            var firstPiece = removed.First();
            if (!removed.All(x => x.Key.type.Equals(firstPiece.Key.type)))
            {
                return;
            }

            var pos = firstPiece.Value.CurrPos;
            var piece = board.CreatePiece(pieceSpawner.GetRandomPiece(), pos.x, pos.y);
            result[piece] = new ChangeInfo()
            {
                Change = ChangeType.Created,
                CurrPos = pos
            };
        }

        public bool Solve(int x, int y, IBoard board, Dictionary<Piece, ChangeInfo> result)
        {
            var piece = board.GetAt(x, y);
            if (!pieceSpawner.IsValid(piece.type))
            {
                return false;
            }

            var solver = pieceSpawner.GetSolver(piece.type);
            solver.Solve(x, y, board, result);

            return result.Count > 0;
        }

        private Dictionary<Piece, ChangeInfo> GetRemovedPieces(Dictionary<Piece, ChangeInfo> changeSequance)
        {
            var result = new Dictionary<Piece, ChangeInfo>();
            foreach (var pieceInfo in changeSequance)
            {
                if (pieceInfo.Value.Change == ChangeType.Removed)
                {
                    result[pieceInfo.Key] = pieceInfo.Value;
                }
            }

            return result;
        }
    }
}