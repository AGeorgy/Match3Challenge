using Tactile.TactileMatch3Challenge.Solvers;
using UnityEngine;

namespace Tactile.TactileMatch3Challenge.PieceSpawn
{
    public class RockedPieceSpawner : BasePieceSpawner
    {
        [SerializeField] private RockedSpawnerSettings pieceSettings;

        protected override (int, int) GetFromToIndex()
        {
            return (pieceSettings.IndexFrom, pieceSettings.IndexTo);
        }

        protected override ISolver GetSolverBase(int type)
        {
            return pieceSettings.GetSolver(MapPieceType(type));
        }

        protected override Sprite GetSpriteForPieceTypeBase(int type)
        {
            return pieceSettings.GetSpriteForPieceType(MapPieceType(type));
        }
    }
}