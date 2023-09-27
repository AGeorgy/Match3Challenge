using Tactile.TactileMatch3Challenge.Solvers;
using UnityEngine;

namespace Tactile.TactileMatch3Challenge.PieceSpawn
{
    [CreateAssetMenu(fileName = "VerticalRockedSettings", menuName = "Tactile/VerticalRockedSettings")]
    public class VerticalRockedSettings : BaseRockedSettings
    {
        override protected ISolver GetSolver()
        {
            return new VerticalLineSolver();
        }
    }
}
