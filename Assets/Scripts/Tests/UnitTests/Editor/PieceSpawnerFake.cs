using Tactile.TactileMatch3Challenge.Model.PieceGenerators;

namespace Tactile.TactileMatch3Challenge.Tests.UnitTests
{
    public class PieceGeneratorFake : IPieceGenerator
    {
        private readonly int value;
        private readonly int startIndex;
        private readonly int count;

        public PieceGeneratorFake(int value, int startIndex, int count)
        {
            this.value = value;
            this.startIndex = startIndex;
            this.count = count;
        }

        public int GetRandomPiece()
        {
            return value;
        }

        public bool GetValidIndex(int type, out int index)
        {
            var isValid = type >= startIndex && type < startIndex + count;
            index = isValid ? MapPieceType(type) : -1;
            return isValid;
        }

        private int MapPieceType(int type)
        {
            var index = type - startIndex;
            return index;
        }
    }
}