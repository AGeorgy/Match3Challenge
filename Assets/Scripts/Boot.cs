using Tactile.TactileMatch3Challenge.Level;
using Tactile.TactileMatch3Challenge.Model;
using Tactile.TactileMatch3Challenge.Model.PieceGenerators;
using Tactile.TactileMatch3Challenge.PieceSpawn;
using Tactile.TactileMatch3Challenge.Model.Solvers;
using Tactile.TactileMatch3Challenge.Model.Strategy;
using Tactile.TactileMatch3Challenge.ViewComponents;
using UnityEngine;
using Tactile.TactileMatch3Challenge.Goals;

namespace Tactile.TactileMatch3Challenge
{
    public class Boot : MonoBehaviour
    {
        [SerializeField] private BoardRenderer boardRenderer;
        [SerializeField] private SpriteDatabase rockedSpriteDatabase;
        [SerializeField] private PieceSpawner rockedPieceSpawner;
        [SerializeField] private SpriteDatabase regularSpriteDatabase;
        [SerializeField] private PieceSpawner regularPieceSpawner;
        [SerializeField] private GoalProvider goalProvider;
        [SerializeField] private LevelInfo levelInfo;


        private Board board;
        private GameLevel gameLevel;

        void Start()
        {
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
            rockedPieceSpawner.SetSpriteDatabase(rockedSpriteDatabase);
            regularPieceSpawner.SetSpriteDatabase(regularSpriteDatabase);
            var regularGenerator = new PieceGenerator(0, regularSpriteDatabase.Size);
            var rockedGenerator = new PieceGenerator(regularSpriteDatabase.Size, rockedSpriteDatabase.Size);

            var strategies = new IStrategy[] {
                new RocketStrategy(rockedPieceSpawner, rockedGenerator, new SolverProvider(new HorizontalLineSolver(),new VerticalLineSolver())),
                new SameTypeStrategy(regularPieceSpawner, regularGenerator, new SolverProvider(new ConnectedSameTypeSolver())),
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
            gameLevel = new GameLevel(goalProvider.GetGoals());
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
