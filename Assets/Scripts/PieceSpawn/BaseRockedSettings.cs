using System.Collections.Generic;
using Tactile.TactileMatch3Challenge.Solvers;
using UnityEngine;

namespace Tactile.TactileMatch3Challenge.PieceSpawn
{
    public abstract class BaseRockedSettings : ScriptableObject
    {
        [SerializeField] private Sprite spritesPerPieceTypeId;

        public Sprite Sprite => spritesPerPieceTypeId;

        public ISolver Solver => GetSolver();

        protected virtual ISolver GetSolver()
        {
            return null;
        }
    }
}
