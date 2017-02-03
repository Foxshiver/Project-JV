using UnityEngine;
using System.Collections;

public class FleeBehavior : GeneralBehavior
{
    // Constructor
    public FleeBehavior(Unit unit) : base(unit)
    {
    }

    // Functions
    public Vector2 computeFleeSteering(Vector2 targetPosition)
    {
        Vector2 targetOffset = targetPosition - _unit._currentPosition;
        float distance = targetOffset.magnitude;

        Vector2 desiredVelocity = (targetPosition - _unit._currentPosition).normalized * _unit._maxSpeed;
        desiredVelocity *= -1;

        return desiredVelocity - _unit._velocity;
    }
}
