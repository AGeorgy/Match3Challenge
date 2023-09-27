using System.Collections.Generic;
using Tactile.TactileMatch3Challenge.Solvers;
using UnityEngine;

namespace Tactile.TactileMatch3Challenge.PieceSpawn
{
    [CreateAssetMenu(fileName = "RegularSpawnerSettings", menuName = "Tactile/RegularSpawnerSettings")]
    public class RegularSpawnerSettings : BaseSpawnerSettings
    {
        [SerializeField] private List<Sprite> spritesPerPieceTypeId;

        public int GetPieceTypeCount()
        {
            return spritesPerPieceTypeId.Count;
        }

        public Sprite GetSpriteForPieceType(int pieceType)
        {
            if (pieceType >= 0 && pieceType < spritesPerPieceTypeId.Count)
            {
                return spritesPerPieceTypeId[pieceType];
            }
            return null;
        }

        public ISolver GetSolver()
        {
            return new ConnectedSameTypeSolver();
        }
    }
}