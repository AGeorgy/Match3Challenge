using System.Collections.Generic;
using Tactile.TactileMatch3Challenge.Strategy;
using UnityEngine;

namespace Tactile.TactileMatch3Challenge.Model
{
    public class StrategyResolver
    {
        private readonly IBoard board;
        private readonly IStrategy[] strategies;

        public StrategyResolver(IBoard board, params IStrategy[] strategies)
        {
            this.board = board;
            this.strategies = strategies;
        }

        public GameObject CreatePiece(Piece piece)
        {
            foreach (var strategy in strategies)
            {
                var visualPiece = strategy.CreateVisualPiece(piece);
                if (visualPiece != null)
                {
                    return visualPiece;
                }
            }

            return null;
        }

        public ResolveResult Resolve(int x, int y)
        {
            var result = new Dictionary<Piece, ChangeInfo>();

            FindAndRemoveConnectedAt(x, y, result);
            MoveAndCreatePiecesUntilFull(result);

            return new ResolveResult(result);
        }

        private void MoveAndCreatePiecesUntilFull(in Dictionary<Piece, ChangeInfo> result)
        {
            foreach (var strategy in strategies)
            {
                strategy.Fill(board, result);
            }
        }

        private void FindAndRemoveConnectedAt(int x, int y, in Dictionary<Piece, ChangeInfo> result)
        {
            foreach (var strategy in strategies)
            {
                if (strategy.Solve(x, y, board, result))
                {
                    return;
                }
            }
        }
    }
}