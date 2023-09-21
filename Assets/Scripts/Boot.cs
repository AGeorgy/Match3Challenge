using Tactile.TactileMatch3Challenge.Model;
using Tactile.TactileMatch3Challenge.ViewComponents;
using UnityEngine;

namespace Tactile.TactileMatch3Challenge {
	
	public class Boot : MonoBehaviour {
		[SerializeField] private PieceTypeDatabase pieceTypeDatabase;
		[SerializeField] private BoardRenderer boardRenderer;
		
		void Start () {
			
			int[,] boardDefinition = {
				{3, 3, 1, 2, 3, 3},
				{2, 2, 1, 2, 3, 3},
				{1, 1, 0, 0, 2, 2},
				{2, 2, 0, 0, 1, 1},
				{1, 1, 2, 2, 1, 1},
				{1, 1, 2, 2, 1, 4},
			};

			var pieceSpawner = new PieceSpawner(pieceTypeDatabase.GetPieceTypeCount());
			var board = Board.Create(boardDefinition, pieceSpawner);
			
			boardRenderer.Initialize(board, pieceTypeDatabase);
		}

	}
	
}
