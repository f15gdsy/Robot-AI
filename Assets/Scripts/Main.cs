using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Main : MonoBehaviour {
	
	public static Main instance;
	
	private PFStaticMap _pfMap;
	public PFStaticMap pfMap {get{return _pfMap;}}
	private List<PFField> _obstacles;
	private PFField _target;
	
	public bool isSimulating = false;
	
	public PFRadialField target;
	public PFRadialField obstacle;
	public PFRadialField obstacleField;
	public PFRadialField targetField;
	
	
	void Awake () {
		instance = this;	
		_pfMap = GetComponent<PFStaticMap>();
		_obstacles = new List<PFField>();
	}
	
	void Start () {}
	
	void OnEnable () {
		instance = this;	
	}
	
	void OnDisable () {
		instance = null;	
	}
	
	void Update () {
		if (Input.GetKeyDown(KeyCode.Mouse0)) {
			Vector3 mousePos = Utility.Helper.GetMousePositionInWorld2D(10, 0);
			PlantTarget(mousePos);
		}
		
		if (Input.GetKeyDown(KeyCode.Mouse1)) {
			Vector3 mousePos = Utility.Helper.GetMousePositionInWorld2D(10, 0);
			PlantObstacle(mousePos);
		}
	}
	
	
	
	private void PlantObstacle (Vector3 pos) {
		PFField field;
		if (isSimulating) {
			 field = (PFField) Instantiate(obstacle, pos, Quaternion.identity);
		}
		else {
			field = (PFField) Instantiate(obstacleField, pos, Quaternion.identity);
		}
		
		_obstacles.Add(field);
		_pfMap.AddField(field);
	}
	
	private void PlantTarget (Vector3 pos) {
		if (_target != null) {
			_pfMap.RemoveField(_target);
			Destroy(_target.gameObject);
		}
		
		PFField field;
		if (isSimulating) {
			field = (PFField) Instantiate(target, pos, Quaternion.identity);
		}
		else {
			field = (PFField) Instantiate(targetField, pos, Quaternion.identity);
		}
		
		_target = field;
		_pfMap.AddField(field);
		
	}
}
