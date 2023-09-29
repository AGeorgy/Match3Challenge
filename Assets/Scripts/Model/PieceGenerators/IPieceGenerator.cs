namespace Tactile.TactileMatch3Challenge.Model.PieceGenerators
{
    public interface IPieceGenerator : IGetValidIndex, IGetRandomPiece
    {
    }

    public interface IGetValidIndex
    {
        bool GetValidIndex(int type, out int index);
    }

    public interface IGetRandomPiece
    {
        int GetRandomPiece();
    }
}