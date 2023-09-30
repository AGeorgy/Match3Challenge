using System;
using Tactile.TactileMatch3Challenge.Application;

namespace Tactile.TactileMatch3Challenge.GameStages
{
    public class InitStage : IGameStage
    {
        private readonly IAppContext ctx;

        public InitStage(IAppContext ctx)
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