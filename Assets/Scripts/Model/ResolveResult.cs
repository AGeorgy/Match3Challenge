using System.Collections.Generic;

namespace Tactile.TactileMatch3Challenge.Model {

	public class ResolveResult {
		public Dictionary<Piece, ChangeInfo> Changes{ get; private set;}

        public ResolveResult(Dictionary<Piece, ChangeInfo> changes)
        {
            Changes = changes;
        }
	}

}