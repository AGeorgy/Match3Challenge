using Tactile.TactileMatch3Challenge.Model;
using Tactile.TactileMatch3Challenge.Model.Board;
using Tactile.TactileMatch3Challenge.Model.Game;
using Tactile.TactileMatch3Challenge.Views.Animation;
using UnityEngine;

namespace Tactile.TactileMatch3Challenge.ViewComponents
{
    public class BoardRenderer : MonoBehaviour
    {
        [SerializeField] private ShiftDownAnimator animator;

        private Board board;
        private Game game;
        private bool isInputBlocked;

        public void Initialize(Board board, Game game)
        {
            this.board = board;
            this.game = game;

            CenterCamera();
            Reset();
        }

        public void BlockInput()
        {
            isInputBlocked = true;
        }

        public void Reset()
        {
            CreateVisualPiecesFromBoardState();
            isInputBlocked = false;
        }

        private void CenterCamera()
        {
            Camera.main.transform.position = new Vector3((board.Width - 1) * 0.5f, -(board.Height - 1) * 0.5f);
        }

        private void CreateVisualPiecesFromBoardState()
        {
            DestroyVisualPieces();

            foreach (var pieceInfo in board.IteratePieces())
            {
                var visualPieceGO = CreateVisualPieceAndAddToAnimator(pieceInfo.piece);
                visualPieceGO.transform.localPosition = ViewUtils.LogicPosToVisualPos(pieceInfo.pos.x, pieceInfo.pos.y);
            }
        }

        private BoardPos ScreenPosToLogicPos(float x, float y)
        {
            var worldPos = Camera.main.ScreenToWorldPoint(new Vector3(x, y, -Camera.main.transform.position.z));
            var boardSpace = transform.InverseTransformPoint(worldPos);

            return new BoardPos()
            {
                x = Mathf.RoundToInt(boardSpace.x),
                y = -Mathf.RoundToInt(boardSpace.y)
            };
        }

        private void DestroyVisualPieces()
        {
            animator.Clear();
            game.Reset();
        }

        // private void Update()
        // {
        //     if (!isInputBlocked && Input.GetMouseButtonDown(0))
        //     {
        //         var pos = ScreenPosToLogicPos(Input.mousePosition.x, Input.mousePosition.y);

        //         if (board.IsWithinBounds(pos.x, pos.y))
        //         {
        //             var resolved = game.Resolve(pos.x, pos.y);

        //             animator.AnimateSequance(resolved, (go) =>
        //             {
        //                 Object.Destroy(go);
        //             }, (piece) => CreateVisualPieceAndAddToAnimator(piece));
        //         }
        //     }
        // }

        private GameObject CreateVisualPieceAndAddToAnimator(Piece piece)
        {
            var visualPieceGO = game.GetVisualForPiece(piece);
            animator.Add(piece, visualPieceGO);
            return visualPieceGO;
        }
    }
}
