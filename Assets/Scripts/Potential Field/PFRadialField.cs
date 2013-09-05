using UnityEngine;
using System.Collections;

public class PFRadialField : PFField {

	public int radius;
	public override int boundsHalfWidth {get{return radius;}}
	public override int boundsHalfHeight {get{return radius;}}
	

	
	public override int GetLocalPotential (int localX, int localY) {
		int dist = Mathf.Abs(localX) + Mathf.Abs(localY);	// manhattan distance
		if (dist > radius) return 0;
		
		int type = potential / Mathf.Abs(potential);
//		Debug.Log("local x " + localX + " local y " + localY);
		if (type > 0) {
			return Mathf.Max(0, potential - gradation * dist);
		}
		else {
			return Mathf.Min(0, potential + gradation * dist);	
		}
	}
}
