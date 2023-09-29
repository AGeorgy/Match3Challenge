using System;

namespace Tactile.TactileMatch3Challenge.Model.PieceGenerators
{
    public class PieceGenerator : IPieceGenerator
    {
        readonly Random random = new();
        private readonly int startIndex;
        private readonly int count;

        public PieceGenerator(int startIndex, int count)
        {
            this.startIndex = startIndex;
            this.count = count;
        }

        public bool GetValidIndex(int type, out int index)
        {
            var isValid = type >= startIndex && type < startIndex + count;
            index = isValid ? MapPieceType(type) : -1;
            return isValid;
        }

        public int GetRandomPiece()
        {
            return random.Next(startIndex, startIndex + count);
        }

        private int MapPieceType(int type)
        {
            var index = type - startIndex;
            return index;
        }
    }
}