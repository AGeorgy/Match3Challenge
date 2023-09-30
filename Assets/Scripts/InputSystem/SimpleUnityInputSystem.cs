using System;
using UnityEngine;

namespace Tactile.TactileMatch3Challenge.InputSystem
{
    public class SimpleUnityInputSystem : MonoBehaviour, IInputSystem
    {
        public event EventHandler<InputEventArgs> Click;

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                var pos = ScreenPosToWorldPos(Input.mousePosition.x, Input.mousePosition.y);
                Click?.Invoke(this, new InputEventArgs(pos.x, pos.y));
            }
        }

        private Vector3 ScreenPosToWorldPos(float x, float y)
        {
            var worldPos = Camera.main.ScreenToWorldPoint(new Vector3(x, y, -Camera.main.transform.position.z));
            return transform.InverseTransformPoint(worldPos);
        }
    }
}