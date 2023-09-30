using Tactile.TactileMatch3Challenge.Level;
using Tactile.TactileMatch3Challenge.Model;
using Tactile.TactileMatch3Challenge.Model.PieceGenerators;
using Tactile.TactileMatch3Challenge.PieceSpawn;
using Tactile.TactileMatch3Challenge.Model.Solvers;
using Tactile.TactileMatch3Challenge.Model.Strategy;
using Tactile.TactileMatch3Challenge.ViewComponents;
using UnityEngine;
using Tactile.TactileMatch3Challenge.Settings;
using Tactile.TactileMatch3Challenge.InputSystem;

namespace Tactile.TactileMatch3Challenge
{
    public class Boot : MonoBehaviour
    {
        [SerializeField] private SimpleUnityInputSystem inputSystem;
        [SerializeField] private LevelInfo levelInfo;
        [SerializeField] private BoardRenderer boardRenderer;
        [Space]
        [Header("Rocket")]
        [SerializeField] private SolverSettingProvider rocketSolverSettingProvider;
        [SerializeField] private SpriteDatabase rocketSpriteDatabase;
        [SerializeField] private PieceSpawner rocketPieceSpawner;
        [Space]
        [Header("Regular")]
        [SerializeField] private SolverSettingProvider regularSolverSettingProvider;
        [SerializeField] private SpriteDatabase regularSpriteDatabase;
        [SerializeField] private PieceSpawner regularPieceSpawner;
        [SerializeField] private GoalProvider goalProvider;

        private Board board;
        private GameLevel gameLevel;
        private App app;

        void Start()
        {
            app = new App();
            board = new Board();
            UpdateBoard();
            CreateGameLevel();

            var game = new Game(board, gameLevel, GetStrategies());

            boardRenderer.Initialize(board, game);


            levelInfo.RestartAction = RestartLevelInfo;
            levelInfo.ResetView(gameLevel.GetGoalsSummary());
        }

        void OnDestroy()
        {
            gameLevel.Achieved -= OnGameGoalAchieved;
            gameLevel.InfoUpdated -= OnGameLevelInfoUpdated;
        }

        private IStrategy[] GetStrategies()
        {
            rocketPieceSpawner.SetSpriteDatabase(rocketSpriteDatabase);
            regularPieceSpawner.SetSpriteDatabase(regularSpriteDatabase);

            var regularGenerator = new PieceGenerator(0, regularSpriteDatabase.Size);
            var rockedGenerator = new PieceGenerator(regularSpriteDatabase.Size, rocketSpriteDatabase.Size);

            var rocketSolver = new SolverProvider(rocketSolverSettingProvider.GetAll());
            var regularSolver = new SolverProvider(regularSolverSettingProvider.GetAll());

            var strategies = new IStrategy[] {
                new RocketStrategy(rocketPieceSpawner, rockedGenerator, rocketSolver),
                new SameTypeStrategy(regularPieceSpawner, regularGenerator, regularSolver),
            };

            return strategies;
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
            gameLevel = new GameLevel(goalProvider.GetAll());
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
