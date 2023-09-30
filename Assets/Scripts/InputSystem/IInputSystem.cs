using System;

namespace Tactile.TactileMatch3Challenge.InputSystem
{
    public interface IInputSystem
    {
        public event EventHandler<InputEventArgs> Click;
    }
}