using System;
using System.Collections.Generic;
using Tactile.TactileMatch3Challenge.Model;
using Tactile.TactileMatch3Challenge.ViewComponents;
using UnityEngine;

namespace Tactile.TactileMatch3Challenge.Views.Animation {

    [CreateAssetMenu(fileName = "Animator", menuName = "Tactile/Animator", order = 1)]
    public class ShiftDownAnimator : ScriptableObject
    {
        [SerializeField] private float spawnAnimationDelay = 0.2f;
        [SerializeField] private float moveAnimationDelay = 0.2f;
        [SerializeField] private float destroyAnimationDelay = 0.0f;

        private readonly Dictionary<Piece, AnimatedVisualPiece> visualPieces = new();

        public void AnimateSequance(ResolveResult resolveResult, Action<GameObject> onDestroy, Action<Piece> onCreate)
        {
            var changes = resolveResult.Changes;

            foreach (var piece in changes.Keys)
            {
                var changeInfo = changes[piece];

                if(changeInfo.Change == ChangeType.Created){
                    onCreate(piece);
                    var newVisualPiece = visualPieces[piece];
                    var from = ViewUtils.LogicPosToVisualPos(changeInfo.FromPos.x, changeInfo.FromPos.y);
                    newVisualPiece.GetComponent<AnimatedVisualPiece>().AnimateSpawn(GetSpawnDelay(changeInfo.CreationTime), from);
                }
                else if(changeInfo.Change == ChangeType.Moved){
                    var visualPiece = visualPieces[piece];
                    var from = ViewUtils.LogicPosToVisualPos(changeInfo.FromPos.x, changeInfo.FromPos.y);
                    var to = ViewUtils.LogicPosToVisualPos(changeInfo.ToPos.x, changeInfo.ToPos.y);
                    visualPiece.AnimateMove(GetMoveDelay(changeInfo.CreationTime), from, to);
                }
                else if(changeInfo.Change == ChangeType.Removed){
                    var visualPiece = visualPieces[piece];
                    visualPieces.Remove(piece);
                    visualPiece.AnimateDestroy(destroyAnimationDelay, () => {
                        onDestroy(visualPiece.gameObject);
                    });
                }
                else if(changeInfo.Change == ChangeType.CreatedAndMoved){
                    onCreate(piece);
                    var newVisualPiece = visualPieces[piece];
                    var from = ViewUtils.LogicPosToVisualPos(changeInfo.FromPos.x, changeInfo.FromPos.y);
                    var to = ViewUtils.LogicPosToVisualPos(changeInfo.ToPos.x, changeInfo.ToPos.y);
                    newVisualPiece.GetComponent<AnimatedVisualPiece>().AnimateSpawn(GetSpawnDelay(changeInfo.CreationTime), from);
                    newVisualPiece.GetComponent<AnimatedVisualPiece>().AnimateMove(0, from, to);
                }
                else{
                    throw new ArgumentOutOfRangeException();
                }
            }
        }

        private float GetSpawnDelay(int creationTime)
        {
            return destroyAnimationDelay + spawnAnimationDelay + creationTime * moveAnimationDelay;
        }

        private float GetMoveDelay(int wave)
        {
            return destroyAnimationDelay + moveAnimationDelay * wave;
        }

        internal void Add(Piece piece, GameObject gameObject)
        {
            visualPieces.Add(piece, gameObject.GetComponent<AnimatedVisualPiece>());
        }

        internal void Clear()
        {
            visualPieces.Clear();
        }
    }
}