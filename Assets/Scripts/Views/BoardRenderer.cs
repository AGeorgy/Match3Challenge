using System.Collections.Generic;
using Tactile.TactileMatch3Challenge.Model;
using Tactile.TactileMatch3Challenge.Views.Animation;
using UnityEngine;

namespace Tactile.TactileMatch3Challenge.ViewComponents {

	public class BoardRenderer : MonoBehaviour {
		[SerializeField] private ShiftDownAnimator animator;
		[SerializeField] private VisualPiece visualPiecePrefab;
		
		private Board board;
		private PieceTypeDatabase pieceTypeDatabase;
		
		public void Initialize(Board board, PieceTypeDatabase pieceTypeDatabase) {
			this.board = board;
            this.pieceTypeDatabase = pieceTypeDatabase;

			CenterCamera();
			CreateVisualPiecesFromBoardState();
		}

		private void CenterCamera() {
			Camera.main.transform.position = new Vector3((board.Width-1)*0.5f,-(board.Height-1)*0.5f);
		}

		private void CreateVisualPiecesFromBoardState() {
			DestroyVisualPieces();

			foreach (var pieceInfo in board.IteratePieces()) {
				var visualPieceGO = CreateVisualPieceAndAddToAnimator(pieceInfo.piece);
				visualPieceGO.transform.localPosition = ViewUtils.LogicPosToVisualPos(pieceInfo.pos.x, pieceInfo.pos.y);
			}
		}

		private BoardPos ScreenPosToLogicPos(float x, float y) { 
			
			var worldPos = Camera.main.ScreenToWorldPoint(new Vector3(x,y,-Camera.main.transform.position.z));
			var boardSpace = transform.InverseTransformPoint(worldPos);

			return new BoardPos() {
				x = Mathf.RoundToInt(boardSpace.x),
				y = -Mathf.RoundToInt(boardSpace.y)
			};

		}

		private GameObject CreateVisualPiece(Piece piece) {
			
			var pieceObject = Instantiate(visualPiecePrefab, transform, true);
			var sprite = pieceTypeDatabase.GetSpriteForPieceType(piece.type);
			pieceObject.SetSprite(sprite);
			return pieceObject.gameObject;
			
		}

		private void DestroyVisualPieces() {
            animator.Clear();
			foreach (var visualPiece in GetComponentsInChildren<VisualPiece>()) {
				Object.Destroy(visualPiece.gameObject);
			}
		}

		private void Update() {
			
			if (Input.GetMouseButtonDown(0)) {

				var pos = ScreenPosToLogicPos(Input.mousePosition.x, Input.mousePosition.y);

				if (board.IsWithinBounds(pos.x, pos.y)) {
					var resolved = board.Resolve(pos.x, pos.y);

                    animator.AnimateSequance(resolved, (go) => {
                        Object.Destroy(go);
                    }, (go)=> CreateVisualPieceAndAddToAnimator(go));
				}
			}
		}

        private GameObject CreateVisualPieceAndAddToAnimator(Piece piece) {
            var visualPieceGO = CreateVisualPiece(piece);
            animator.Add(piece, visualPieceGO);
            return visualPieceGO;
        }
    }
}
