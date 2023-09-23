using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tactile.TactileMatch3Challenge.Views.Animation {
    
    [RequireComponent(typeof(SpriteRenderer))]
    public class AnimatedVisualPiece : MonoBehaviour {
        [SerializeField] private float spawnAnimationDuration = 0.2f;
        [SerializeField] private float moveAnimationDuration = 0.2f;
        [SerializeField] private float destroyAnimationDuration = 0.2f;

        private readonly Queue<IEnumerator> animationStack = new();
        private Coroutine currentAnimation;

        public void AnimateDestroy(float delay, Action onFinished)
        {
            animationStack.Enqueue(DelayCoroutine(delay));
            animationStack.Enqueue(AnimateDestroyCoroutine(onFinished));
            StartCoroutineIfStacked();
        }

        public void AnimateSpawn(float delay, Vector3 from, Vector3 to)
        {
            animationStack.Enqueue(DelayCoroutine(delay));
            animationStack.Enqueue(AnimateSpawnCoroutine(from));
            animationStack.Enqueue(AnimateMoveCoroutine(from, to));
            StartCoroutineIfStacked();
        }

        public void AnimateMove(float delay, Vector3 from, Vector3 to)
        {
            animationStack.Enqueue(DelayCoroutine(delay));
            animationStack.Enqueue(AnimateMoveCoroutine(from, to));
            StartCoroutineIfStacked();
        }

        private IEnumerator DelayCoroutine(float delay){
            yield return new WaitForSeconds(delay);
            FinishAndStartNextAnimation();
        }

        private IEnumerator AnimateSpawnCoroutine(Vector3 from){
            transform.localScale = Vector3.zero;
            transform.position = from;
            float time = 0;
            while(time < spawnAnimationDuration){
                time += Time.deltaTime;
                transform.localScale = Vector3.Lerp(Vector3.zero, Vector3.one, time / spawnAnimationDuration);
                yield return null;
            }
            transform.localScale = Vector3.one;
            FinishAndStartNextAnimation();
        }

        private IEnumerator AnimateMoveCoroutine(Vector3 from, Vector3 to)
        {
            transform.position = from;
            float time = 0;
            while(time < moveAnimationDuration){
                time += Time.deltaTime;
                transform.position = Vector3.Lerp(from, to, time / moveAnimationDuration);
                yield return null;
            }
            transform.position = to;
            FinishAndStartNextAnimation();
        }

        private IEnumerator AnimateDestroyCoroutine(Action onFinished)
        {
            transform.localScale = Vector3.one;
            float time = 0;
            while(time < destroyAnimationDuration){
                time += Time.deltaTime;
                transform.localScale = Vector3.Lerp(Vector3.one, Vector3.zero, time / spawnAnimationDuration);
                yield return null;
            }
            transform.localScale = Vector3.zero;
            onFinished();
            FinishAndStartNextAnimation();
        }

        private void FinishAndStartNextAnimation()
        {
            currentAnimation = null;
            StartCoroutineIfStacked();
        }

        private void StartCoroutineIfStacked()
        {
            if(currentAnimation == null && animationStack.Count > 0){
                currentAnimation = StartCoroutine(animationStack.Dequeue());
            }
        }
    }
}