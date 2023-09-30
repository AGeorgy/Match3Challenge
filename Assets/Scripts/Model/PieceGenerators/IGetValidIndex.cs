namespace Tactile.TactileMatch3Challenge.Model.PieceGenerators
{
    public interface IGetValidIndex
    {
        bool GetValidIndex(int type, out int index);
    }
}