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
    public Vector2 computePursuitSteering(Vector2 targetPosition, Vector2 targetVelocity)
    {
        Vector2 targetOffsetTarget = targetPosition - _unit._currentPosition;
        float distanceTarget = targetOffsetTarget.magnitude;

        // TO IMPROVE
        if (distanceTarget < 5.0f)
            CoefPredictedPosition = 0;
        else
            CoefPredictedPosition = 1;

        Vector2 predictedPosition = targetPosition + targetVelocity * CoefPredictedPosition;

        Vector2 targetOffset = predictedPosition - _unit._currentPosition;
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
            desiredVelocity = (predictedPosition - _unit._currentPosition).normalized * _unit._maxSpeed;
        }

        return desiredVelocity - _unit._velocity;
    }
}
