using UnityEngine;

namespace Tactile.TactileMatch3Challenge.ViewComponents {

	public static class ViewUtils {
        public static Vector3 LogicPosToVisualPos(float x,float y) { 
			return new Vector3(x, -y, -y);
		}
    }
}