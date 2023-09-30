using System;
using Tactile.TactileMatch3Challenge.GameStages;

namespace Tactile.TactileMatch3Challenge
{
    public class App : IDisposable
    {
        private int activStageIndex;
        private IGameStage activeStage;
        private IGameStage[] gameStages;

        public App()
        {
            gameStages = new IGameStage[]
            {
                new InitStage(),
                new GamePlayStage(),
                new GameOverStage(),
                new ResetStage()
            };

            SubscribeOnFinish();
        }

        public void Dispose()
        {
            UnsubscribeOnFinish();
        }

        private void SubscribeOnFinish()
        {
            foreach (var stage in gameStages)
            {
                stage.OnFinish += OnStageFinish;
            }
        }

        private void UnsubscribeOnFinish()
        {
            foreach (var stage in gameStages)
            {
                stage.OnFinish -= OnStageFinish;
            }
        }

        private void OnStageFinish()
        {
            activStageIndex++;
            activStageIndex %= gameStages.Length;

            TurnStageOn();
        }

        private void TurnStageOn()
        {
            activeStage?.Off();
            activeStage = gameStages[activStageIndex];
            activeStage.On();
        }
    }
}