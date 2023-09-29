using System;
using System.Collections.Generic;
using Tactile.TactileMatch3Challenge.Model;
using Tactile.TactileMatch3Challenge.ViewComponents;
using UnityEngine;

namespace Tactile.TactileMatch3Challenge.Views.Animation
{
    [CreateAssetMenu(fileName = "Animator", menuName = "Tactile/Animator", order = 1)]
    public class ShiftDownAnimator : ScriptableObject
    {
        [SerializeField] private float waveDelay = 0.2f;

        private readonly Dictionary<Piece, AnimatedVisualPiece> visualPieces = new();

        public void AnimateSequance(ResolveResult resolveResult, Action<GameObject> onDestroy, Action<Piece> onCreate)
        {
            var changes = resolveResult.Changes;

            foreach (var piece in changes.Keys)
            {
                var changeInfo = changes[piece];

                if (changeInfo.Change == ChangeType.Created)
                {
                    onCreate(piece);
                    var newVisualPiece = visualPieces[piece];
                    var from = ViewUtils.LogicPosToVisualPos(changeInfo.CurrPos.x, changeInfo.CurrPos.y);
                    newVisualPiece.GetComponent<AnimatedVisualPiece>().AnimateSpawn(GetDelay(changeInfo.CreationTime), from);
                }
                else if (changeInfo.Change == ChangeType.Moved)
                {
                    var visualPiece = visualPieces[piece];
                    var from = ViewUtils.LogicPosToVisualPos(changeInfo.CurrPos.x, changeInfo.CurrPos.y);
                    var to = ViewUtils.LogicPosToVisualPos(changeInfo.ToPos.x, changeInfo.ToPos.y);
                    visualPiece.AnimateMove(GetDelay(changeInfo.CreationTime), from, to);
                }
                else if (changeInfo.Change == ChangeType.Removed)
                {
                    var visualPiece = visualPieces[piece];
                    visualPieces.Remove(piece);
                    visualPiece.AnimateDestroy(GetDelay(changeInfo.CreationTime), () =>
                    {
                        onDestroy(visualPiece.gameObject);
                    });
                }
                else if (changeInfo.Change == ChangeType.CreatedAndMoved)
                {
                    onCreate(piece);
                    var newVisualPiece = visualPieces[piece];
                    var from = ViewUtils.LogicPosToVisualPos(changeInfo.CurrPos.x, changeInfo.CurrPos.y);
                    var to = ViewUtils.LogicPosToVisualPos(changeInfo.ToPos.x, changeInfo.ToPos.y);
                    newVisualPiece.GetComponent<AnimatedVisualPiece>().AnimateSpawn(GetDelay(changeInfo.CreationTime), from);
                    newVisualPiece.GetComponent<AnimatedVisualPiece>().AnimateMove(0, from, to);
                }
                else
                {
                    throw new ArgumentOutOfRangeException();
                }
            }
        }

        private float GetDelay(int wave)
        {
            return waveDelay * wave;
        }

        public void Add(Piece piece, GameObject gameObject)
        {
            if (gameObject == null)
            {
                throw new ArgumentNullException(nameof(gameObject));
            }
            visualPieces.Add(piece, gameObject.GetComponent<AnimatedVisualPiece>());
        }

        public void Clear()
        {
            visualPieces.Clear();
        }
    }
}
