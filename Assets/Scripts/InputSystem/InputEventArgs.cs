namespace Tactile.TactileMatch3Challenge.InputSystem
{
    public readonly struct InputEventArgs
    {
        private readonly float x;
        private readonly float y;

        public InputEventArgs(float x, float y)
        {
            this.x = x;
            this.y = y;
        }

        public readonly float X => x;
        public readonly float Y => y;
    }
}