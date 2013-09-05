using UnityEngine;
using System.Collections;

public interface IMoveAgent {
	Vector3 velocity {get; set;}
	float maxVelocity {get;}
	float maxForce {get;}
	Vector3 position {get; set;}
	float mass {get;}
}


public class MoveEngine {
	
	private Vector3 _steering;
	private IMoveAgent agent;
	
	
	public MoveEngine (IMoveAgent agent) {
		this.agent = agent;	
	}
	
	public void CalculatePosition () {
		_steering = Utility.Math.Truncate(_steering, agent.maxForce);
		_steering /= agent.mass;
		
		agent.velocity += _steering;
		Utility.Math.Truncate(agent.velocity, agent.maxVelocity);

		agent.position += agent.velocity * Time.deltaTime;
		
		_steering = Vector3.zero;
	}
	
	
	
	private Vector3 DoSeek (Vector3 target, float slowingRadius) {
		Vector3 desiredVelocity = target - agent.position;
		float distance = desiredVelocity.magnitude;
		
		desiredVelocity.Normalize();
		
		if (distance > slowingRadius) {	// not reached
			desiredVelocity *= agent.maxVelocity;
		}
		else {
			if (distance > 0.05f) {
				desiredVelocity *= agent.maxVelocity * distance / slowingRadius;
			}
			else {
				return -agent.velocity;
			}
		}
		
		return desiredVelocity - agent.velocity;
	}
	
	public void Seek (Vector3 target, float slowingRadius) {
		_steering += DoSeek(target, slowingRadius);
	}
}
