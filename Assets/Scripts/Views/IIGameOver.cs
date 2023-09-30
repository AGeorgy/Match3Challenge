using System;

namespace Tactile.TactileMatch3Challenge.ViewComponents
{
    public interface IGameOver
    {
        event Action RestartAction;
        public void Disable();
        public void SetWinState(bool isAchieved);
    }
}