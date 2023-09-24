using System;

namespace Tactile.TactileMatch3Challenge.Model {

	public class ChangeInfo {
		
		public ChangeType Change { get; set; }
		public int CreationTime { get; set; }
		public BoardPos FromPos { get; set; }
		public BoardPos ToPos { get; set; }
		
	}

    [Flags]
    public enum ChangeType : short
    {
        Created = 1,
        Moved = 2,
        Removed = 4,
        CreatedAndMoved = Created | Moved,
    }

}