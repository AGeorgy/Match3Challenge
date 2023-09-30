using System;
using Tactile.TactileMatch3Challenge.Application;
using Tactile.TactileMatch3Challenge.Level;
using Tactile.TactileMatch3Challenge.Model.Game;
using Tactile.TactileMatch3Challenge.ViewComponents;
using Tactile.TactileMatch3Challenge.Views.Animation;

namespace Tactile.TactileMatch3Challenge.GameStages
{
    public class ResetStage : IGameStage
    {
        private readonly ILevelInfo levelInfo;
        private readonly IGetGoalsSummary getGoalsSummary;
        private readonly IAnimatorClear animator;
        private readonly IGameLevelReset gameLevelReset;
        private readonly IGameReset gameReset;

        public ResetStage(IAppContext ctx)
        {
            levelInfo = ctx.Resolve<ILevelInfo>();
            getGoalsSummary = ctx.Resolve<IGetGoalsSummary>();
            animator = ctx.Resolve<IAnimatorClear>();
            gameLevelReset = ctx.Resolve<IGameLevelReset>();
            gameReset = ctx.Resolve<IGameReset>();

        }

        public event Action OnFinish;

        public void Off()
        {

        }

        public void On()
        {
            animator.Clear();
            gameReset.Reset();
            gameLevelReset.Reset();
            levelInfo.SetLevelInfo(getGoalsSummary.GetGoalsSummary());

            OnFinish?.Invoke();
        }
    }
}