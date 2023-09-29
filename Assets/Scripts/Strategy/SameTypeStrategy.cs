using System.Collections.Generic;
using Tactile.TactileMatch3Challenge.Model;
using Tactile.TactileMatch3Challenge.PieceSpawn;

namespace Tactile.TactileMatch3Challenge.Strategy
{
    public class SameTypeStrategy : BaseStrategy, IStrategy
    {
        public SameTypeStrategy(IPieceSpawner pieceSpawner) : base(pieceSpawner)
        {
        }

        public void Fill(IBoard board, Dictionary<Piece, ChangeInfo> changed)
        {
            int resolveStep = 0;
            bool moreToResolve = true;

            while (moreToResolve)
            {
                moreToResolve = MovePiecesOneDownIfAble(board, changed);
                moreToResolve |= CreatePiecesAtTop(board, changed, resolveStep);
                resolveStep++;
            }
        }

        public bool Solve(int x, int y, IBoard board, Dictionary<Piece, ChangeInfo> result)
        {
            var piece = board.GetAt(x, y);
            if (!pieceSpawner.IsValid(piece.Type))
            {
                return false;
            }

            var solver = pieceSpawner.GetSolver(piece.Type);
            solver.Solve(x, y, board, result);

            return result.Count > 0;
        }

        private bool MovePiecesOneDownIfAble(IBoard board, Dictionary<Piece, ChangeInfo> moved)
        {
            bool movedAny = false;

            for (int y = board.Height - 1; y >= 1; y--)
            {
                for (int x = 0; x < board.Width; x++)
                {

                    var dest = board.GetAt(x, y);
                    if (dest != null)
                    {
                        continue;
                    }

                    var pieceToMove = board.GetAt(x, y - 1);
                    if (pieceToMove == null)
                    {
                        continue;
                    }

                    var fromX = x;
                    var fromY = y - 1;
                    board.MovePiece(fromX, fromY, x, y);
                    movedAny = true;

                    if (!moved.ContainsKey(pieceToMove))
                    {
                        moved[pieceToMove] = new ChangeInfo
                        {
                            CurrPos = new BoardPos(fromX, fromY),
                            Change = ChangeType.Moved,
                        };
                    };
                    var info = moved[pieceToMove];
                    info.ToPos = new BoardPos(x, y);
                    info.Change |= ChangeType.Moved;
                }
            }

            return movedAny;
        }

        private bool CreatePiecesAtTop(IBoard board, Dictionary<Piece, ChangeInfo> created, int resolveStep)
        {
            var createdAnyPieces = false;
            var y = 0;
            for (int x = 0; x < board.Width; x++)
            {
                if (board.GetAt(x, y) == null)
                {
                    var piece = board.CreatePiece(pieceSpawner.GetRandomPiece(), x, y);
                    createdAnyPieces = true;

                    created[piece] = new ChangeInfo()
                    {
                        CreationTime = resolveStep,
                        Change = ChangeType.CreatedAndMoved,
                        ToPos = new BoardPos(x, y),
                        CurrPos = new BoardPos(x, y - 1)
                    };
                }
            }

            return createdAnyPieces;
        }
    }
}