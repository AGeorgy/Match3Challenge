using System;

namespace Tactile.TactileMatch3Challenge.GameStages
{
    public interface IGameStage
    {
        event Action OnFinish;

        void Off();
        void On();
    }
}