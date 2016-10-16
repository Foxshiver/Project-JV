using UnityEngine;
using System.Collections;

public class LeaderBehavior : GeneralBehavior
{
    public Transform _target;
    public float _radius;

    public LeaderBehavior() : base()
    {}

    void Update()
    {
        _position = this.computeNewPosition(0);
        this.transform.position = _position;
    }

    public override Vector3 computeSeekSteering()
    {
        Vector3 targetOffset = _target.position - _position;
        float distance = targetOffset.magnitude;
        Vector3 desiredVelocity;
       
        desiredVelocity = (_target.position - _position).normalized * _maxSpeed;    

        return desiredVelocity - _velocity;
    }

    public float getRadius()
    {
        return _radius;
    }
}
