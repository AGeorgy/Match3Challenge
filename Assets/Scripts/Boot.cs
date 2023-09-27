using Tactile.TactileMatch3Challenge.Model;
using Tactile.TactileMatch3Challenge.PieceSpawn;
using Tactile.TactileMatch3Challenge.Strategy;
using Tactile.TactileMatch3Challenge.ViewComponents;
using UnityEngine;

namespace Tactile.TactileMatch3Challenge
{
    public class Boot : MonoBehaviour
    {
        [SerializeField] private BoardRenderer boardRenderer;
        [SerializeField] private RockedPieceSpawner rockedPieceSpawner;
        [SerializeField] private RegularPieceSpawner regularPieceSpawner;

        void Start()
        {
            int[,] boardDefinition = {
                {3, 3, 1, 2, 3, 3},
                {2, 2, 1, 2, 3, 3},
                {1, 1, 0, 0, 2, 2},
                {2, 2, 0, 0, 1, 1},
                {1, 1, 2, 2, 1, 1},
                {1, 1, 2, 2, 1, 4},
            };

            var board = Board.Create(boardDefinition);


            var resolver = new StrategyResolver(board, new RockedStrategy(rockedPieceSpawner), new SameTypeStrategy(regularPieceSpawner));

            boardRenderer.Initialize(board, resolver);
        }
    }
}
