using UnityEngine;
using System.Collections;

public class GeneralBehavior : MonoBehaviour {
    
    public float _masse;
    public float _maxSpeed;
    public float _maxSteeringForce;
    public Vector3 _position;
    public Vector3 _direction;
    public Vector3 _velocity;
    public Vector3 _steering;

    public GeneralBehavior()
    {}

	public Vector3 computeNewPosition(Vector3 steering)
    {
		_steering = Vector3.ClampMagnitude(steering, _maxSteeringForce); // steering return the force corresponding at the behavior send by param
        Vector3 acceleration = _steering / _masse;
        _velocity = Vector3.ClampMagnitude(_velocity + acceleration /* *Time.deltaTime*/, _maxSpeed);
        
		return _position + _velocity * Time.deltaTime;
    }
}
