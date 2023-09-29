using Tactile.TactileMatch3Challenge.PieceSpawn;

namespace Tactile.TactileMatch3Challenge.Strategy
{
    public class BaseStrategy : IBaseStrategy
    {
        protected readonly IPieceSpawner pieceSpawner;

        public IValidateAndVisualPiece ValidateAndGetVisualPiece => pieceSpawner;

        public BaseStrategy(IPieceSpawner pieceSpawner)
        {
            this.pieceSpawner = pieceSpawner;
        }

        public void Reset()
        {
            pieceSpawner.Clear();
        }
    }
}