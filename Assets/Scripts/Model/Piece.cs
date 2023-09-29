namespace Tactile.TactileMatch3Challenge.Model
{
    public class Piece
    {
        public int Type { get; private set; }

        public Piece(int type)
        {
            Type = type;
        }

        public override string ToString()
        {
            return string.Format("(type:{0})", Type);
        }
    }
}