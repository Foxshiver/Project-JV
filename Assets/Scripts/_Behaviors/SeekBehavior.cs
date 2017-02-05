using UnityEngine;
using System.Collections;

public class SeekBehavior : GeneralBehavior
{
    // Attributes
    public float CoefRadiusTarget = 2.0f;

    // Constructor
    public SeekBehavior(MovableEntity mc) : base(mc)
    { }

    // Functions
    public Vector2 computeSeekSteering(Vector2 targetPosition)
    {
        Vector2 targetOffset = targetPosition - _mc._currentPosition;
        float distance = targetOffset.magnitude;
        Vector2 desiredVelocity;

        // Arrival Behavior
        float targetRadius = 5.0f;

        if (distance < targetRadius)
        {
            float rampedSpeed = _mc._maxSpeed * (distance / (targetRadius * CoefRadiusTarget));
            float clippedSpeed = Mathf.Min(rampedSpeed, _mc._maxSpeed);
            desiredVelocity = (clippedSpeed / distance) * targetOffset;
        }
        else
        {
            desiredVelocity = (targetPosition - _mc._currentPosition).normalized * _mc._maxSpeed;
        }

        return desiredVelocity - _mc._velocity;
    }
}
