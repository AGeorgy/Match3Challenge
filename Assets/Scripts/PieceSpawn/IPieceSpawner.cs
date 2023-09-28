using Tactile.TactileMatch3Challenge.Solvers;
using UnityEngine;

namespace Tactile.TactileMatch3Challenge.PieceSpawn
{
    public interface IPieceSpawner
    {
        ISolver GetSolver(int type);
        bool IsRelevant(int type);
        int GetRandomPiece();
        GameObject GetVisualPiece(int type);
        void Clear();
    }
}