using Tactile.TactileMatch3Challenge.Model.PieceGenerators;
using Tactile.TactileMatch3Challenge.PieceSpawn;
using Tactile.TactileMatch3Challenge.Model.Solvers;

namespace Tactile.TactileMatch3Challenge.Model.Strategy
{
    public class BaseStrategy : IBaseStrategy
    {
        protected readonly IPieceSpawner pieceSpawner;
        protected readonly IPieceGenerator pieceGenerator;
        protected readonly ISolverProvider solverProvider;

        public BaseStrategy(IPieceSpawner pieceSpawner, IPieceGenerator pieceGenerator, ISolverProvider solverProvider)
        {
            this.pieceSpawner = pieceSpawner;
            this.pieceGenerator = pieceGenerator;
            this.solverProvider = solverProvider;
        }

        public IGetValidIndex IndexValidator => pieceGenerator;
        public IGetVisualPiece VisualPieceProvider => pieceSpawner;

        public void Reset()
        {
            pieceSpawner.Clear();
        }
    }
}