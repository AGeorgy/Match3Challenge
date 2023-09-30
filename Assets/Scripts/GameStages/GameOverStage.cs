using System;
using Tactile.TactileMatch3Challenge.Application;

namespace Tactile.TactileMatch3Challenge.GameStages
{
    public class GameOverStage : IGameStage
    {
        private readonly IAppContext ctx;

        public GameOverStage(IAppContext ctx)
        {
            this.ctx = ctx;
        }

        public event Action OnFinish;

        public void Off()
        {
            throw new NotImplementedException();
        }

        public void On()
        {
            throw new NotImplementedException();
        }
    }
}