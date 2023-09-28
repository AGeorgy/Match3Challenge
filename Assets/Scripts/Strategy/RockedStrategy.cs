using System.Collections.Generic;
using System.Linq;
using Tactile.TactileMatch3Challenge.Model;
using Tactile.TactileMatch3Challenge.PieceSpawn;
using UnityEngine;

namespace Tactile.TactileMatch3Challenge.Strategy
{
    public class RockedStrategy : IStrategy
    {
        private readonly IPieceSpawner pieceSpawner;

        public RockedStrategy(IPieceSpawner pieceSpawner)
        {
            this.pieceSpawner = pieceSpawner;
        }

        public GameObject CreateVisualPiece(Piece piece)
        {
            return pieceSpawner.GetVisualPiece(piece.type);
        }

        public void Fill(IBoard board, Dictionary<Piece, ChangeInfo> result)
        {
            if (result.Count < 5)
            {
                return;
            }

            if (!result.All(x => x.Key.type.Equals(result.First().Key.type)))
            {
                return;
            }

            var firstPiece = result.Values.First();
            var pos = firstPiece.CurrPos;
            var piece = board.CreatePiece(pieceSpawner.GetRandomPiece(), pos.x, pos.y);
            result[piece] = new ChangeInfo()
            {
                Change = ChangeType.Created,
                CurrPos = pos
            };
        }

        public void Reset()
        {
            pieceSpawner.Clear();
        }

        public bool Solve(int x, int y, IBoard board, Dictionary<Piece, ChangeInfo> result)
        {
            var piece = board.GetAt(x, y);
            if (!pieceSpawner.IsRelevant(piece.type))
            {
                return false;
            }

            var solver = pieceSpawner.GetSolver(piece.type);
            solver.Solve(x, y, board, result);

            return result.Count > 0;
        }
    }
}