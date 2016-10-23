using UnityEngine;
using System.Collections;

<<<<<<< HEAD
public class GeneralBehavior {
=======
public class GeneralBehavior : MonoBehaviour {
    
    public float _masse;
    public float _maxSpeed;
    public float _maxSteeringForce;
    public Vector2 _position;
	public Vector2 _direction;
	public Vector2 _velocity;
	public Vector2 _steering;
>>>>>>> d258e6e0d12ff2315ebcdbe1af461b381674093a

    protected Unit _unit;

<<<<<<< HEAD
    public GeneralBehavior(Unit unit)
    {
        _unit = unit;
    }

	public Vector2 computeNewPosition(Vector2 steering)
    {
        _unit._steering = Vector2.ClampMagnitude(steering, _unit._maxSteeringForce); // steering return the force corresponding at the behavior send by param
        Vector2 acceleration = _unit._steering / _unit._masse;
        _unit._velocity = Vector2.ClampMagnitude(_unit._velocity + acceleration /* *Time.deltaTime*/, _unit._maxSpeed);
=======
	public Vector2 computeNewPosition(Vector2 steering)
    {
		_steering = Vector2.ClampMagnitude(steering, _maxSteeringForce); // steering return the force corresponding at the behavior send by param
		//_steering.y = Vector3.ClampMagnitude(steering, _maxSteeringForce).z; // steering return the force corresponding at the behavior send by param
		Vector2 acceleration = _steering / _masse;
		_velocity = Vector2.ClampMagnitude(_velocity + acceleration /* *Time.deltaTime*/, _maxSpeed);
>>>>>>> d258e6e0d12ff2315ebcdbe1af461b381674093a
        
		return _unit._position + _unit._velocity * Time.deltaTime;
    }
}
