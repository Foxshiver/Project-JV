using UnityEngine;
using System.Collections;

public class PursuitBehavior : GeneralBehavior
{
    // Attributes
    public int CoefPredictedPosition;
    public float CoefRadiusTarget = 2.0f;

    // Constructor
    public PursuitBehavior(Unit unit) : base(unit)
    {
        Debug.Log("PursuitBehavior constructor called");
    }

    // Functions
    public Vector2 computePursuitSteering(Unit targetUnit)
    {
        Vector2 targetOffsetTarget = targetUnit._position - _unit._position;
        float distanceTarget = targetOffsetTarget.magnitude;

        // TO IMPROVE
        if (distanceTarget < 5.0f)
            CoefPredictedPosition = 0;
        else
            CoefPredictedPosition = 1;

        Vector2 predictedPosition = targetUnit._position + targetUnit._velocity * CoefPredictedPosition;

        Vector2 targetOffset = predictedPosition - _unit._position;
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
            desiredVelocity = (predictedPosition - _unit._position).normalized * _unit._maxSpeed;
        }

        return desiredVelocity - _unit._velocity;
    }
}
