using Tactile.TactileMatch3Challenge.PieceSpawn;
using Tactile.TactileMatch3Challenge.Model.Solvers;
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

        public void Clear()
        {
            throw new System.NotImplementedException();
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

        public bool IsValid(int type)
        {
            return true;
        }
    }
}