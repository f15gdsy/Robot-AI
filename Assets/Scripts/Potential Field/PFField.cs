using UnityEngine;
using System.Collections;

public abstract class PFField : MonoBehaviour {

	public int potential;
	public int gradation;
	
	abstract public int boundsHalfWidth {get;}
	abstract public int boundsHalfHeight {get;}
	
	public Vector3 position {get {return transform.position;}}
	
	
	abstract public int GetLocalPotential (int localX, int localY);
	
}
