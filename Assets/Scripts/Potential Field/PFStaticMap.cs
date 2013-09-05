using UnityEngine;
using System.Collections;
using System.Collections.Generic;



public class PFStaticMap : MonoBehaviour {

	private int[][] _map;
	
	public int width;
	public int height;
	
	public float tileWidth;
	public float tileHeight;
	
	private List<GameObject> _tiles;
	private Transform _quads;
	
	public bool shouldDrawHighlight = false;
	public bool shouldDrawGrid = true;
		
	
	void Start () {
		_map = new int[width][];
		for (int x=0; x<width; x++) {
			_map[x] = new int[height];
			
			for (int y=0; y<height; y++) {
				_map[x][y] = 0;
			}
		}
		
		LogMatrix();
		
		if (shouldDrawGrid) Draw();
	}
	
	public void AddField (PFField field) {
		CalculateField(field, 1);
		LogMatrix();
		
		if (shouldDrawHighlight) Draw();
	}
	
	public void RemoveField (PFField field) {
		CalculateField(field, -1);
		LogMatrix();
		
		if (shouldDrawHighlight) Draw();
	}
	
	public int GetPotential (int x, int y) {
		return _map[x][y];
	}
	
	private void CalculateField (PFField field, int multiplier) {
		PFPosition pos = WorldToMap(field.position);
		
		int minX = Mathf.Max(0, pos.x - field.boundsHalfWidth);
		int maxX = Mathf.Min(width-1, pos.x + field.boundsHalfWidth);
		int minY = Mathf.Max(0, pos.y - field.boundsHalfHeight);
		int maxY = Mathf.Min(height-1, pos.y + field.boundsHalfHeight);
		
		for (int x=minX; x<=maxX; x++) {
			for (int y=minY; y<=maxY; y++) {
				_map[x][y] += multiplier * field.GetLocalPotential(x - pos.x, y - pos.y);
			}
		}
	}
	
	public PFPosition WorldToMap (Vector3 position) {
		int x = (int) (position.x/tileWidth);
		int y = (int) (position.y/tileHeight);
		
		return new PFPosition(x, y);
	}
	
	public Vector3 MapToWorld (PFPosition pfPosition) {
		float x = pfPosition.x * tileWidth + tileWidth / 2;
		float y = pfPosition.y * tileHeight + tileHeight / 2;
		
		return new Vector3(x, y);
	}
	
	public void Draw () {
		if (_tiles == null) {
			_tiles = new List<GameObject>();
			
			GameObject go = new GameObject("quads");
			_quads = go.transform;
			_quads.position = Vector3.zero;
		}
		else {
			for (int i=_tiles.Count-1; i>=0; i--) {
				GameObject go = _tiles[i];
				_tiles.Remove(go);
				Destroy(go);
			}
		}
		
		for (int x=0; x<width; x++) {
			for (int y=0; y<height; y++) {
				GameObject go = GameObject.CreatePrimitive(PrimitiveType.Quad);
			
				int potential = _map[x][y];
				Color color;
				if (potential > 0) {
					color = new Color(1-potential*0.1f, 1, 1);
				}
				else {
					color = new Color(1, 1-potential*0.1f, 1);
				}
				
				go.renderer.material.color = color;
				
				go.transform.position = new Vector3(x * tileWidth + tileWidth/2, y * tileHeight + tileHeight/2, 0);
				go.transform.localScale = new Vector3(tileWidth, tileHeight, 1);
				
				go.transform.parent = _quads;
				_tiles.Add(go);
			}
		}
	}
	
	private void LogMatrix () {
		string matrix = "";
		
		for (int y=height-1; y>=0; y--) {
			for (int x=0; x<width; x++) {
				matrix += _map[x][y] + " ";
			}
			matrix += "\n";
		}
		
		Debug.Log(matrix);
	}
}


public struct PFPosition {
	public int x;
	public int y;
	
	public PFPosition (int x, int y) {
		this.x = x;
		this.y = y;
	}
}