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
using Tactile.TactileMatch3Challenge.Application;
using Tactile.TactileMatch3Challenge.Views.Animation;
using Tactile.TactileMatch3Challenge.Model.Game;
using Tactile.TactileMatch3Challenge.Model.Board;

namespace Tactile.TactileMatch3Challenge
{
    public class Boot : MonoBehaviour, IDestroy
    {
        [SerializeField] private GoalProvider goalProvider;
        [SerializeField] private SimpleUnityInputSystem inputSystem;
        [SerializeField] private ShiftDownAnimator animator; [Space]
        [Space]
        [Header("View")]
        [SerializeField] private LevelInfo levelInfo;
        [SerializeField] private GameOver gameOver;
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

        private App app;

        void Start()
        {
            app = new App();

            var ctx = new AppContext();
            ctx.Register<IDestroy>(this);
            ctx.Register<IInputSystem>(inputSystem);
            ctx.Register<ILevelInfo>(levelInfo);
            ctx.Register<IGameOver>(gameOver);
            ctx.Register<IAddVisualPiece, IAnimatorClear, IAnimateSequance>(animator);
            ctx.Register<IBoard, IIsWithinBounds>(new Board());
            ctx.Register<IUpdateLevelStats, IGetGoalsSummary, IGameLevelAchieved, IIsAchieved, IGameLevelReset>(new GameLevel(goalProvider.GetAll()));
            ctx.Register<IGetVisualForPiece, IResolve, IGameReset>(new Game(ctx, GetStrategies()));

            app.InitGameStages(ctx);
        }

        void OnDestroy()
        {
            app.Dispose();
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

        public void Destroy(GameObject gameObject)
        {
            Object.Destroy(gameObject);
        }
    }
}
