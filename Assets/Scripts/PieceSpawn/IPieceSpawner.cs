using Tactile.TactileMatch3Challenge.Solvers;
using UnityEngine;

namespace Tactile.TactileMatch3Challenge.PieceSpawn
{
    public interface IPieceSpawner : IValidateAndVisualPiece
    {
        ISolver GetSolver(int type);
        int GetRandomPiece();
        void Clear();
    }

    public interface IValidateAndVisualPiece
    {
        bool IsValid(int type);
        GameObject GetVisualPiece(int type);
    }
}