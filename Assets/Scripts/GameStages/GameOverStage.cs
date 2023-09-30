using System;
using Tactile.TactileMatch3Challenge.Application;
using Tactile.TactileMatch3Challenge.Level;
using Tactile.TactileMatch3Challenge.ViewComponents;

namespace Tactile.TactileMatch3Challenge.GameStages
{
    public class GameOverStage : IGameStage
    {
        private readonly IGameOver gameOver;
        private readonly IIsAchieved isAchieved;

        public GameOverStage(IAppContext ctx)
        {
            gameOver = ctx.Resolve<IGameOver>();
            isAchieved = ctx.Resolve<IIsAchieved>();
        }

        public event Action OnFinish;

        public void Off()
        {
            gameOver.RestartAction -= OnRestart;
            gameOver.Disable();
        }

        public void On()
        {
            gameOver.RestartAction += OnRestart;
            gameOver.SetWinState(isAchieved.IsAchieved);
        }

        private void OnRestart()
        {
            OnFinish?.Invoke();
        }
    }
}