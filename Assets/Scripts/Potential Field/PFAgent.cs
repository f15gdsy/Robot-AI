using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PFAgent : MonoBehaviour {
	
	private List<PFStaticMap> _staticMaps;
	private List<bool> _staticMapSwitches;
	
	private List<PFTrail> _trail;
	public int trailLength = 0;
	public int trailPotential = 0;
	
	void Awake () {
		_staticMaps = new List<PFStaticMap>();
		_staticMapSwitches = new List<bool>();
		
//		if (trailLength > 0) {
			_trail = new List<PFTrail>();	
//		}
	}
	
	public void AddStaticMap (PFStaticMap staticMap) {
		_staticMaps.Add(staticMap);
		_staticMapSwitches.Add(true);
	}
	
	public int GetStaticPotentialSum (int x, int y) {
		int sum = 0;
		for (int i=0; i<_staticMaps.Count; i++) {
			if (_staticMapSwitches[i]) {
				sum += _staticMaps[i].GetPotential(x, y);
			}
		}
		
		return sum;
	}
	
	public int GetTrailPotentialSum (int x, int y) {
		int potential = 0;
		foreach (PFTrail trail in _trail) {
			if (trail.position.x == x && trail.position.y == y) {
				potential += trail.potential;
			}
		}
		return potential;
	}
	
	public Vector3 GetTargetPosition () {
		PFStaticMap map = _staticMaps[0];
		PFPosition pfPos = map.WorldToMap(transform.position);
		
		int minX = Mathf.Max(0, pfPos.x - 1);
		int maxX = Mathf.Min(map.width - 1, pfPos.x + 1);
		int minY = Mathf.Max(0, pfPos.y - 1);
		int maxY = Mathf.Min(map.height - 1, pfPos.y + 1);
		
		PFPosition pfTargetPos = pfPos;
		int maxPotential = -100;
		
		// looping through the neibouring and the current grids
		for (int x=minX; x<=maxX; x++) {
			for (int y=minY; y<=maxY; y++) {
				int potential = GetStaticPotentialSum(x, y) + GetTrailPotentialSum(x, y);
				if (potential > maxPotential) {
					maxPotential = potential;
					pfTargetPos = new PFPosition(x, y);
				}
			}
		}
		
		if (trailLength > 0) {
			_trail.Insert(0, new PFTrail(pfTargetPos.x, pfTargetPos.y, trailPotential));
			if (_trail.Count > trailLength) {
				_trail.RemoveAt(trailLength - 1);	
			}
		}
		
		return map.MapToWorld(pfTargetPos);
	}
	
	
	private class PFTrail {
		public PFPosition position;
		public int potential;
		
		public PFTrail (int x, int y, int potential) {
			position = new PFPosition(x, y);
			this.potential = potential;
		}
	}
}
