using UnityEngine;

namespace Tactile.TactileMatch3Challenge.Level
{
    [CreateAssetMenu(fileName = "CollectOneTypePiecesInTurnsSetting", menuName = "Tactile/Match3Challenge/CollectOneTypePiecesInTurnsSetting")]
    public class CollectOneTypePiecesInTurnsSetting : ScriptableObject
    {
        [SerializeField] private int maxTurns;
        [SerializeField] private int pieceType;
        [SerializeField] private int collectPieces;
        [SerializeField] private string description;

        public int MaxTurns => maxTurns;
        public int PieceType => pieceType;
        public string Description => description;
        public int CollectPieces => collectPieces;
    }
}