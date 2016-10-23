using UnityEngine;
using System.Collections;

public class GeneralBehavior : MonoBehaviour {
    
    public float _masse;
    public float _maxSpeed;
    public float _maxSteeringForce;
    public Vector2 _position;
	public Vector2 _direction;
	public Vector2 _velocity;
	public Vector2 _steering;

    public GeneralBehavior()
    {}

	public Vector2 computeNewPosition(Vector2 steering)
    {
		_steering = Vector2.ClampMagnitude(steering, _maxSteeringForce); // steering return the force corresponding at the behavior send by param
		//_steering.y = Vector3.ClampMagnitude(steering, _maxSteeringForce).z; // steering return the force corresponding at the behavior send by param
		Vector2 acceleration = _steering / _masse;
		_velocity = Vector2.ClampMagnitude(_velocity + acceleration /* *Time.deltaTime*/, _maxSpeed);
        
		return _position + _velocity * Time.deltaTime;
    }
}
