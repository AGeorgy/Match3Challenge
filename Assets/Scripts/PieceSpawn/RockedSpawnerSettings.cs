using System.Collections.Generic;
using Tactile.TactileMatch3Challenge.Solvers;
using UnityEngine;

namespace Tactile.TactileMatch3Challenge.PieceSpawn
{
    [CreateAssetMenu(fileName = "RockedSpawnerSettings", menuName = "Tactile/RockedSpawnerSettings")]
    public class RockedSpawnerSettings : BaseSpawnerSettings
    {
        [SerializeField] private List<BaseRockedSettings> settings;

        public int GetPieceTypeCount()
        {
            return settings.Count;
        }

        public Sprite GetSpriteForPieceType(int pieceType)
        {
            if (pieceType >= 0 && pieceType < settings.Count)
            {
                return settings[pieceType].Sprite;
            }
            return null;
        }

        public ISolver GetSolver(int type)
        {
            return settings[type].Solver;
        }
    }
}