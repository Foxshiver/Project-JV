using UnityEngine;
using System.Collections;

public class SeekBehavior : GeneralBehavior
{
    // Attributes
    public float CoefRadiusTarget = 2.0f;

    // Constructor
    public SeekBehavior(Unit unit) : base(unit)
    {
        Debug.Log("SeekBehavior constructor called");
    }

    // Functions
    public Vector2 computeSeekSteering(Vector2 targetPosition)
    {
        Vector2 targetOffset = targetPosition - _unit._position;
        float distance = targetOffset.magnitude;
        Vector2 desiredVelocity;

        // Arrival Behavior
        float targetRadius = 5.0f;

        if (distance < targetRadius)
        {
            float rampedSpeed = _unit._maxSpeed * (distance / (targetRadius * CoefRadiusTarget));
            float clippedSpeed = Mathf.Min(rampedSpeed, _unit._maxSpeed);
            desiredVelocity = (clippedSpeed / distance) * targetOffset;
        }
        else
        {
            desiredVelocity = (targetPosition - _unit._position).normalized * _unit._maxSpeed;
        }

        return desiredVelocity - _unit._velocity;
    }
}
