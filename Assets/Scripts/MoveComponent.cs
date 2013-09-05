using UnityEngine;
using System.Collections;

public class MoveComponent : EXMonoBehavior, IMoveAgent {
	
	public Vector3 velocity {get; set;}
	
	[SerializeField]
	protected float _maxVelocity;
	public float maxVelocity {get{return _maxVelocity;}}
	
	[SerializeField]
	protected float _maxForce;
	public float maxForce {get{return _maxForce;}}
	
	[SerializeField]
	protected float _slowingRadius;
	
	public float mass {get{return _rigidbody.mass;}}
	
	public Vector3 position {get{return _transform.position;} set{_transform.position = value;}}
	
	protected Transform _transform;
	protected Rigidbody _rigidbody;
		
	protected MoveEngine _engine;
	
	protected PFAgent _pfAgent;
	
//	public Vector3 finalTarget {get; set;}
	protected Vector3 _target;
	
//	protected bool _isReached = true;
	
	
	void Awake () {
		_transform = transform;
		_rigidbody = rigidbody;
		
		velocity = Vector3.zero;
		_target = position;
		
		_pfAgent = GetComponent<PFAgent>();
	}
	
	void Start () {
		_pfAgent.AddStaticMap(Main.instance.pfMap);
	}
	
	void OnEnable () {
		_engine = new MoveEngine(this);	
	}
	
	void OnDisable () {
		_engine = null;	
	}
	
	void Update () {
//		if (_isReached) return;
		
		_target = _pfAgent.GetTargetPosition();
//		Move();
		
		_engine.Seek(_target, _slowingRadius);
		_engine.CalculatePosition();
	}
	
	
//	private void Move () {
//		Vector3 direction = _target - position;
//		float sqrDist = direction.sqrMagnitude;
//		
//		if (sqrDist > 0.0025f) {
//			position += direction.normalized * velocity * Time.deltaTime;
//		}
//		else {
//			direction = finalTarget - _target;
//			sqrDist = direction.sqrMagnitude;
//			if (sqrDist < 0.01f) {
//				_isReached = true;
//			}
//		}
//	}
}