using System.Collections.Generic;

namespace Tactile.TactileMatch3Challenge.Model.Board
{
    public interface IBoard : IIsWithinBounds
    {
        int Width { get; }
        int Height { get; }
        void SetState(int[,] definition);
        Piece CreatePiece(int pieceType, int x, int y);
        Piece GetAt(int x, int y);
        void MovePiece(int fromX, int fromY, int toX, int toY);
        void RemovePieceAt(int x, int y);
        bool TryGetPiecePos(Piece piece, out int px, out int py);
        IEnumerable<PiecePosition> IteratePieces();
    }
}