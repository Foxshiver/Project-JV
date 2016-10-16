using UnityEngine;
using System.Collections;

public class FollowerBehavior : GeneralBehavior {

    public GameObject test;

    public LeaderBehavior _target;

    public FollowerBehavior() : base()
    {}

    void Update()
    {
        _position = this.computeNewPosition(2);
        this.transform.position = _position;
    }

    public override Vector3 computeSeekSteering()
    {
        Vector3 targetOffset = _target._position - _position;
        float distance = targetOffset.magnitude;
        Vector3 desiredVelocity;

        if (distance < _target.getRadius())
        {
            float rampedSpeed = _maxSpeed * (distance / (_target.getRadius() * 20.0f));

            Debug.Log(rampedSpeed);

            float clippedSpeed = Mathf.Min(rampedSpeed, _maxSpeed);
            desiredVelocity = (clippedSpeed / distance) * targetOffset;
        }
        else
        {
            desiredVelocity = (_target._position - _position).normalized * _maxSpeed;
        }

        return desiredVelocity - _velocity;
    }

    public override Vector3 computePursuitSteering()
    {
        Vector3 predictedPosition = _target._position + _target._velocity * 4;

        test.transform.position = predictedPosition;
        Debug.Log("Pursuit : " + predictedPosition);

        Vector3 targetOffset = predictedPosition - _position;
        float distance = targetOffset.magnitude;
        Vector3 desiredVelocity;

        desiredVelocity = (predictedPosition - _position).normalized * _maxSpeed;

        return desiredVelocity - _velocity;
    }

    public override Vector3 computeFleeSteering()
    {
        Vector3 targetOffset = _target._position - _position;
        float distance = targetOffset.magnitude;

        Vector3 desiredVelocity = (_target._position - _position).normalized * _maxSpeed;
        desiredVelocity *= -1;

        return desiredVelocity - _velocity;
    }
}
