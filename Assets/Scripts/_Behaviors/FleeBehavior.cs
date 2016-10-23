using UnityEngine;
using System.Collections;

public class FleeBehavior : GeneralBehavior
{
    // Constructor
    public FleeBehavior(Unit unit) : base(unit)
    {
        Debug.Log("FleeBehavior constructor called");
    }

    // Functions
    public Vector2 computeFleeSteering(Vector2 targetPosition)
    {
        Vector2 targetOffset = targetPosition - _unit._position;
        float distance = targetOffset.magnitude;

        Vector2 desiredVelocity = (targetPosition - _unit._position).normalized * _unit._maxSpeed;
        desiredVelocity *= -1;

        return desiredVelocity - _unit._velocity;
    }
}
