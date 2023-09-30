using Tactile.TactileMatch3Challenge.ViewComponents;

namespace Tactile.TactileMatch3Challenge.PieceSpawn
{
    public interface ISpriteDatabase
    {
        void SetSpriteAt(int index, ISetSprite setSprite);

        int Size { get; }
    }
}