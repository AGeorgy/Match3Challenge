using System;
using Tactile.TactileMatch3Challenge.Application;
using Tactile.TactileMatch3Challenge.InputSystem;
using Tactile.TactileMatch3Challenge.Level;
using Tactile.TactileMatch3Challenge.Model;
using Tactile.TactileMatch3Challenge.Model.Board;
using Tactile.TactileMatch3Challenge.Model.Game;
using Tactile.TactileMatch3Challenge.ViewComponents;
using Tactile.TactileMatch3Challenge.Views.Animation;
using UnityEngine;

namespace Tactile.TactileMatch3Challenge.GameStages
{
    public class GamePlayStage : IGameStage
    {
        private readonly IInputSystem inputSystem;
        private readonly IIsWithinBounds board;
        private readonly IAnimateSequance animateSequance;
        private readonly IResolve resolve;
        private readonly IDestroy destroy;
        private readonly IAddVisualPiece addVisualPiece;
        private readonly IGetVisualForPiece getVisualForPiece;
        private readonly ILevelInfo levelInfo;
        private readonly IGetGoalsSummary getGoalsSummary;
        private readonly IGameLevelAchieved gameLevelAchieved;

        public GamePlayStage(IAppContext ctx)
        {
            inputSystem = ctx.Resolve<IInputSystem>();
            board = ctx.Resolve<IIsWithinBounds>();
            animateSequance = ctx.Resolve<IAnimateSequance>();
            resolve = ctx.Resolve<IResolve>();
            destroy = ctx.Resolve<IDestroy>();
            addVisualPiece = ctx.Resolve<IAddVisualPiece>();
            getVisualForPiece = ctx.Resolve<IGetVisualForPiece>();
            levelInfo = ctx.Resolve<ILevelInfo>();
            getGoalsSummary = ctx.Resolve<IGetGoalsSummary>();
            gameLevelAchieved = ctx.Resolve<IGameLevelAchieved>();
        }

        public event Action OnFinish;

        public void Off()
        {
            inputSystem.Click -= OnClick;
            gameLevelAchieved.Achieved -= OnGameLevelAchieved;
        }

        public void On()
        {
            inputSystem.Click += OnClick;
            gameLevelAchieved.Achieved += OnGameLevelAchieved;
        }

        private void OnGameLevelAchieved()
        {
            levelInfo.SetLevelInfo(getGoalsSummary.GetGoalsSummary());
            OnFinish?.Invoke();
        }

        private void OnClick(object sender, InputEventArgs e)
        {
            var pos = new BoardPos()
            {
                x = Mathf.RoundToInt(e.X),
                y = -Mathf.RoundToInt(e.Y)
            };

            if (board.IsWithinBounds(pos.x, pos.y))
            {
                var resolved = resolve.Resolve(pos.x, pos.y);

                animateSequance.AnimateSequance(resolved, (go) =>
                {
                    destroy.Destroy(go);
                }, (piece) => CreateVisualPieceAndAddToAnimator(piece));

                levelInfo.SetLevelInfo(getGoalsSummary.GetGoalsSummary());
            }
        }

        private GameObject CreateVisualPieceAndAddToAnimator(Piece piece)
        {
            var visualPieceGO = getVisualForPiece.GetVisualForPiece(piece);
            addVisualPiece.Add(piece, visualPieceGO);
            return visualPieceGO;
        }
    }
}