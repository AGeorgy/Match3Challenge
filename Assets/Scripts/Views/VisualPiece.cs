using UnityEngine;

namespace Tactile.TactileMatch3Challenge.ViewComponents
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class VisualPiece : MonoBehaviour, ISetSprite
    {
        public void SetSprite(Sprite sprite)
        {
            var spriteRenderer = GetComponent<SpriteRenderer>();
            spriteRenderer.sprite = sprite;
        }
    }
}