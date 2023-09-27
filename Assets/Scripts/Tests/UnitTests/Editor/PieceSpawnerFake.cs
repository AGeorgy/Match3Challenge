using Tactile.TactileMatch3Challenge.PieceSpawn;
using Tactile.TactileMatch3Challenge.Solvers;
using UnityEngine;

namespace Tactile.TactileMatch3Challenge.Tests.UnitTests
{
    public class PieceSpawnerFake : IPieceSpawner
    {
        private readonly int value;
        private readonly ISolver solver;

        public PieceSpawnerFake(int value, ISolver solver = null)
        {
            this.value = value;
            this.solver = solver;
        }

        public int GetRandomPiece()
        {
            return value;
        }

        public ISolver GetSolver(int type)
        {
            return solver;
        }

        public GameObject GetVisualPiece(int type)
        {
            throw new System.NotImplementedException();
        }

        public bool IsRelevant(int type)
        {
            return true;
        }
    }
}