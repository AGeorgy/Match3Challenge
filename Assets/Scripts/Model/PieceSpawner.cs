namespace Tactile.TactileMatch3Challenge.Model {

	public class PieceSpawner : IPieceSpawner {
        private readonly int maxPiceCount;

        public PieceSpawner(int maxPiceCount)
        {
            this.maxPiceCount = maxPiceCount;
        }

		public int CreateBasicPiece() {
			return UnityEngine.Random.Range(0, maxPiceCount);
		}
		
	}

}