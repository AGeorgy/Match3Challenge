using System;
using Tactile.TactileMatch3Challenge.GameStages;

namespace Tactile.TactileMatch3Challenge.Application
{
    public class App : IDisposable
    {
        private int activStageIndex;
        private IGameStage activeStage;
        private IGameStage[] gameStages;

        public void InitGameStages(AppContext ctx)
        {
            gameStages = new IGameStage[]
            {
                new InitStage(ctx),
                new GamePlayStage(ctx),
                new GameOverStage(ctx),
                new ResetStage(ctx)
            };

            SubscribeOnFinish();

            activStageIndex = 0;
            TurnStageOn();
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