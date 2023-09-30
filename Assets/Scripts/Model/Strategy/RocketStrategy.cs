using System.Collections.Generic;
using System.Linq;
using Tactile.TactileMatch3Challenge.Model.PieceGenerators;
using Tactile.TactileMatch3Challenge.PieceSpawn;
using Tactile.TactileMatch3Challenge.Model.Solvers;
using Tactile.TactileMatch3Challenge.Model.Board;

namespace Tactile.TactileMatch3Challenge.Model.Strategy
{
    public class RocketStrategy : BaseStrategy, IStrategy
    {
        public RocketStrategy(IPieceSpawner pieceSpawner, IPieceGenerator pieceGenerator, ISolverProvider solverProvider) :
            base(pieceSpawner, pieceGenerator, solverProvider)
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
            if (!removed.All(x => x.Key.Type.Equals(firstPiece.Key.Type)))
            {
                return;
            }

            var pos = firstPiece.Value.CurrPos;
            var piece = board.CreatePiece(pieceGenerator.GetRandomPiece(), pos.x, pos.y);
            result[piece] = new ChangeInfo()
            {
                Change = ChangeType.Created,
                CurrPos = pos
            };
        }

        public bool Solve(int x, int y, IBoard board, Dictionary<Piece, ChangeInfo> result)
        {
            var piece = board.GetAt(x, y);
            if (pieceGenerator.GetValidIndex(piece.Type, out var index))
            {
                var solver = solverProvider.GetSolver(index);
                solver.Solve(x, y, board, result);

                return result.Count > 0;
            }

            return false;
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