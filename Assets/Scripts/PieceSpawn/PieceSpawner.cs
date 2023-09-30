using Tactile.TactileMatch3Challenge.ViewComponents;
using UnityEngine;

namespace Tactile.TactileMatch3Challenge.PieceSpawn
{
    public class PieceSpawner : MonoBehaviour, IPieceSpawner
    {
        [SerializeField] private VisualPiece visualPiecePrefab;

        private ISpriteDatabase spriteDatabase;

        public void SetSpriteDatabase(ISpriteDatabase spriteDatabase)
        {
            this.spriteDatabase = spriteDatabase;
        }

        public GameObject GetVisualPiece(int index)
        {
            var pieceObject = Instantiate(visualPiecePrefab, transform, true);
            spriteDatabase.SetSpriteAt(index, pieceObject);
            return pieceObject.gameObject;
        }

        public void Clear()
        {
            int childs = transform.childCount;

            for (int i = childs - 1; i >= 0; i--)
            {
                Destroy(transform.GetChild(i).gameObject);
            }
        }
    }
}