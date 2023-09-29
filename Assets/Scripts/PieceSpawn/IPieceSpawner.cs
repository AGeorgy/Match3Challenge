using UnityEngine;

namespace Tactile.TactileMatch3Challenge.PieceSpawn
{
    public interface IPieceSpawner : IGetVisualPiece
    {
        void Clear();
    }

    public interface IGetVisualPiece
    {
        GameObject GetVisualPiece(int index);
    }
}