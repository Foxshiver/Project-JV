using UnityEngine;
using System.Collections;

public class FleeBehavior : GeneralBehavior
{
    // Constructor
    public FleeBehavior(MovableEntity mc) : base(mc)
    { }

    // Functions
    public Vector2 computeFleeSteering(Vector2 targetPosition)
    {
        Vector2 targetOffset = targetPosition - _mc._currentPosition;
        float distance = targetOffset.magnitude;

        Vector2 desiredVelocity = (targetPosition - _mc._currentPosition).normalized * _mc._maxSpeed;
        desiredVelocity *= -1;

        return desiredVelocity - _mc._velocity;
    }
}
