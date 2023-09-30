using Tactile.TactileMatch3Challenge.ViewComponents;
using UnityEngine;

namespace Tactile.TactileMatch3Challenge.PieceSpawn
{
    [CreateAssetMenu(fileName = "SpriteDatabase", menuName = "Tactile/SpriteDatabase")]
    public class SpriteDatabase : ScriptableObject, ISpriteDatabase
    {
        [SerializeField] private Sprite[] sprites;

        public void SetSpriteAt(int index, ISetSprite setSprite)
        {
            setSprite.SetSprite(sprites[index]);
        }

        public int Size => sprites.Length;
    }
}