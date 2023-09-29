using System.Collections.Generic;
using Tactile.TactileMatch3Challenge.Level;
using Tactile.TactileMatch3Challenge.Model.Strategy;
using UnityEngine;

namespace Tactile.TactileMatch3Challenge.Model
{
    public class Game
    {
        private readonly IBoard board;
        private readonly IGameLevel gameLevel;
        private readonly IStrategy[] strategies;

        public Game(IBoard board, IGameLevel gameLevel, params IStrategy[] strategies)
        {
            this.board = board;
            this.gameLevel = gameLevel;
            this.strategies = strategies;
        }

        public GameObject GetVisualForPiece(Piece piece)
        {
            foreach (var strategy in strategies)
            {
                if (strategy.IndexValidator.GetValidIndex(piece.Type, out var index))
                {
                    return strategy.VisualPieceProvider.GetVisualPiece(index);
                }
            }

            return null;
        }

        public ResolveResult Resolve(int x, int y)
        {
            var result = new Dictionary<Piece, ChangeInfo>();

            FindAndRemoveConnectedAt(x, y, result);
            MoveAndCreatePiecesUntilFull(result);
            gameLevel.UpdateLevelStats(result);

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

        public void Reset()
        {
            foreach (var strategy in strategies)
            {
                strategy.Reset();
            }
        }
    }
}