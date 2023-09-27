using Tactile.TactileMatch3Challenge.Solvers;
using UnityEngine;

namespace Tactile.TactileMatch3Challenge.PieceSpawn
{
    [CreateAssetMenu(fileName = "HorizontalRockedSettings", menuName = "Tactile/HorizontalRockedSettings")]
    public class HorizontalRockedSettings : BaseRockedSettings
    {
        override protected ISolver GetSolver()
        {
            return new HorizontalLineSolver();
        }
    }
}
