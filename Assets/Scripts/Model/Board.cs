using System.Collections.Generic;

namespace Tactile.TactileMatch3Challenge.Model
{
    public class Board : IBoard
    {
        private Piece[,] boardState;

        public static Board Create(int[,] definition)
        {
            return new Board(definition);
        }

        public int Width => boardState.GetLength(0);

        public int Height => boardState.GetLength(1);

        public Board(int[,] definition)
        {
            var transposed = ArrayUtility.TransposeArray(definition);
            CreatePieces(transposed);
        }

        private void CreatePieces(int[,] array)
        {
            var defWidth = array.GetLength(0);
            var defHeight = array.GetLength(1);
            boardState = new Piece[defWidth, defHeight];

            for (int y = 0; y < defHeight; y++)
            {
                for (int x = 0; x < defWidth; x++)
                {
                    CreatePiece(array[x, y], x, y);
                }
            }
        }

        public Piece CreatePiece(int pieceType, int x, int y)
        {
            var piece = new Piece() { type = pieceType };
            boardState[x, y] = piece;
            return piece;
        }

        public int[,] GetBoardStateAsArrayWithTypes()
        {
            var result = new int[Width, Height];

            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    var p = boardState[x, y];
                    result[x, y] = p != null ? p.type : -1;
                }
            }

            return ArrayUtility.TransposeArray(result);
        }

        public Piece GetAt(int x, int y) => boardState[x, y];

        public IEnumerable<PiecePosition> IteratePieces()
        {
            for (var y = 0; y < Height; y++)
            {
                for (var x = 0; x < Width; x++)
                {
                    yield return new PiecePosition()
                    {
                        piece = boardState[x, y],
                        pos = new BoardPos(x, y)
                    };
                }
            }
        }

        public void MovePiece(int fromX, int fromY, int toX, int toY)
        {
            boardState[toX, toY] = boardState[fromX, fromY];
            boardState[fromX, fromY] = null;
        }

        public bool IsWithinBounds(int x, int y) => x < Width && y < Height && x >= 0 && y >= 0;

        public void RemovePieceAt(int x, int y) => boardState[x, y] = null;

        public bool TryGetPiecePos(Piece piece, out int px, out int py)
        {
            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    if (boardState[x, y] == piece)
                    {
                        px = x;
                        py = y;
                        return true;
                    }
                }
            }

            px = -1;
            py = -1;
            return false;
        }
    }
}