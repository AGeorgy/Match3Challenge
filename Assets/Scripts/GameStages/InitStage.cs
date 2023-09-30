using System;
using Tactile.TactileMatch3Challenge.Application;
using Tactile.TactileMatch3Challenge.Level;
using Tactile.TactileMatch3Challenge.Model;
using Tactile.TactileMatch3Challenge.Model.Board;
using Tactile.TactileMatch3Challenge.Model.Game;
using Tactile.TactileMatch3Challenge.ViewComponents;
using Tactile.TactileMatch3Challenge.Views.Animation;
using UnityEngine;

namespace Tactile.TactileMatch3Challenge.GameStages
{
    public class InitStage : IGameStage
    {
        private readonly IBoard board;
        private readonly ILevelInfo levelInfo;
        private readonly IAddVisualPiece animator;
        private readonly IGetVisualForPiece game;
        private readonly IGetGoalsSummary gameLevel;

        public InitStage(IAppContext ctx)
        {
            board = ctx.Resolve<IBoard>();
            levelInfo = ctx.Resolve<ILevelInfo>();
            animator = ctx.Resolve<IAddVisualPiece>();
            game = ctx.Resolve<IGetVisualForPiece>();
            gameLevel = ctx.Resolve<IGetGoalsSummary>();
        }

        public event Action OnFinish;

        public void Off()
        {

        }

        public void On()
        {
            SetupBoard();
            SetupGameLevel();
            CreateVisualPiecesFromBoardState();
            CenterCamera();

            OnFinish?.Invoke();
        }

        private void SetupBoard()
        {
            int[,] boardDefinition = {
                {3, 3, 1, 2, 3, 3},
                {2, 2, 1, 2, 3, 3},
                {1, 1, 0, 0, 2, 2},
                {2, 2, 0, 0, 1, 1},
                {1, 1, 2, 2, 1, 1},
                {1, 1, 2, 2, 1, 4},
            };

            board.SetState(boardDefinition);
        }

        private void SetupGameLevel()
        {
            levelInfo.SetLevelInfo(gameLevel.GetGoalsSummary());
        }

        private void CreateVisualPiecesFromBoardState()
        {
            foreach (var pieceInfo in board.IteratePieces())
            {
                var visualPieceGO = CreateVisualPieceAndAddToAnimator(pieceInfo.piece);
                visualPieceGO.transform.localPosition = ViewUtils.LogicPosToVisualPos(pieceInfo.pos.x, pieceInfo.pos.y);
            }
        }

        private GameObject CreateVisualPieceAndAddToAnimator(Piece piece)
        {
            var visualPieceGO = game.GetVisualForPiece(piece);
            animator.Add(piece, visualPieceGO);
            return visualPieceGO;
        }

        private void CenterCamera()
        {
            Camera.main.transform.position = new Vector3((board.Width - 1) * 0.5f, -(board.Height - 1) * 0.5f);
        }
    }
}