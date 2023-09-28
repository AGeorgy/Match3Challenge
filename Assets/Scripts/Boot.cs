using System;
using Tactile.TactileMatch3Challenge.Level;
using Tactile.TactileMatch3Challenge.Model;
using Tactile.TactileMatch3Challenge.PieceSpawn;
using Tactile.TactileMatch3Challenge.Strategy;
using Tactile.TactileMatch3Challenge.ViewComponents;
using UnityEngine;

namespace Tactile.TactileMatch3Challenge
{
    public class Boot : MonoBehaviour
    {
        [SerializeField] private BoardRenderer boardRenderer;
        [SerializeField] private RockedPieceSpawner rockedPieceSpawner;
        [SerializeField] private RegularPieceSpawner regularPieceSpawner;
        [SerializeField] private CollectOneTypePiecesInTurnsSetting goalSetting;
        [SerializeField] private LevelInfo levelInfo;

        private Board board;
        private GameLevel gameLevel;

        void Start()
        {
            board = new Board();
            UpdateBoard();
            CreateGameLevel();
            var game = new Game(board, gameLevel,
            new RockedStrategy(rockedPieceSpawner), new SameTypeStrategy(regularPieceSpawner));

            boardRenderer.Initialize(board, game);


            levelInfo.RestartAction = RestartLevelInfo;
            levelInfo.ResetView(gameLevel.GetGoalsSummary());
        }

        void OnDestroy()
        {
            gameLevel.Achieved -= OnGameGoalAchieved;
            gameLevel.InfoUpdated -= OnGameLevelInfoUpdated;
        }

        private void UpdateBoard()
        {
            int[,] boardDefinition = {
                {3, 3, 1, 2, 3, 3},
                {2, 2, 1, 2, 3, 3},
                {1, 1, 0, 0, 2, 2},
                {2, 2, 0, 0, 1, 1},
                {1, 1, 2, 2, 1, 1},
                {1, 1, 2, 2, 1, 4},
            };

            board.Update(boardDefinition);
        }

        private void CreateGameLevel()
        {
            var goal = new CollectOneTypePiecesInTurnsGoal(goalSetting);
            gameLevel = new GameLevel(goal);
            gameLevel.Achieved += OnGameGoalAchieved;
            gameLevel.InfoUpdated += OnGameLevelInfoUpdated;
        }

        private void OnGameLevelInfoUpdated(string levelinfo)
        {
            levelInfo.SetLevelInfo(levelinfo);
        }

        private void OnGameGoalAchieved(bool isWin)
        {
            boardRenderer.BlockInput();
            levelInfo.SetWinState(isWin);
        }

        private void RestartLevelInfo()
        {
            UpdateBoard();
            gameLevel.Reset();
            levelInfo.ResetView(gameLevel.GetGoalsSummary());
            boardRenderer.Reset();
        }
    }
}
