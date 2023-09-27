using Tactile.TactileMatch3Challenge.Solvers;
using Tactile.TactileMatch3Challenge.ViewComponents;
using UnityEngine;

namespace Tactile.TactileMatch3Challenge.PieceSpawn
{
    public abstract class BasePieceSpawner : MonoBehaviour, IPieceSpawner
    {
        [SerializeField] private VisualPiece visualPiecePrefab;

        protected (int, int) indexFromTO;

        void Awake()
        {
            indexFromTO = GetFromToIndex();
        }

        public GameObject GetVisualPiece(int type)
        {
            if (!IsRelevant(type))
            {
                return null;
            }

            var pieceObject = Instantiate(visualPiecePrefab, transform, true);
            var sprite = GetSpriteForPieceTypeBase(type);
            pieceObject.SetSprite(sprite);
            return pieceObject.gameObject;
        }

        public ISolver GetSolver(int type)
        {
            return GetSolverBase(type);
        }

        public bool IsRelevant(int type)
        {
            return type >= indexFromTO.Item1 && type <= indexFromTO.Item2;
        }

        public int GetRandomPiece()
        {
            return Random.Range(indexFromTO.Item1, indexFromTO.Item2 + 1);
        }

        protected virtual (int, int) GetFromToIndex()
        {
            return (0, 0);
        }

        protected virtual Sprite GetSpriteForPieceTypeBase(int type)
        {
            return null;
        }

        protected virtual ISolver GetSolverBase(int type)
        {
            return null;
        }

        protected int MapPieceType(int type)
        {
            var index = type - indexFromTO.Item1;
            return index;
        }
    }
}