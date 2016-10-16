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

    public Vector3 computeNewPosition(int state)
    {
        if(state == 0)
            _steering = Vector3.ClampMagnitude(computeSeekSteering(), _maxSteeringForce);
        if(state == 1)
            _steering = Vector3.ClampMagnitude(computeFleeSteering(), _maxSteeringForce);
        if(state == 2)
            _steering = Vector3.ClampMagnitude(computePursuitSteering(), _maxSteeringForce);
        if(state == 3)
            _steering = Vector3.ClampMagnitude(computeEvasionSteering(), _maxSteeringForce);

        Vector3 acceleration = _steering / _masse;
        _velocity = Vector3.ClampMagnitude(_velocity + acceleration * Time.deltaTime, _maxSpeed);

        return _position + _velocity * Time.deltaTime;
    }

    public virtual Vector3 computeSeekSteering()
    {
        return new Vector3(0.0f,0.0f,0.0f);
    }

    public virtual Vector3 computePursuitSteering()
    {
        return new Vector3(0.0f, 0.0f, 0.0f);
    }

    public virtual Vector3 computeFleeSteering()
    {
        return new Vector3(0.0f, 0.0f, 0.0f);
    }

    public virtual Vector3 computeEvasionSteering()
    {
        return new Vector3(0.0f, 0.0f, 0.0f);
    }
}
