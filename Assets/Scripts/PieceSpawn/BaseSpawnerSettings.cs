using UnityEngine;

namespace Tactile.TactileMatch3Challenge.PieceSpawn
{
    public abstract class BaseSpawnerSettings : ScriptableObject
    {
        [SerializeField] private int indexFrom;
        [SerializeField] private int indexTo;

        public int IndexFrom => indexFrom;
        public int IndexTo => indexTo;
    }
}