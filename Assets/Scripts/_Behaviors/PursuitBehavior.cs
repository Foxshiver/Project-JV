using UnityEngine;
using System.Collections;

public class PursuitBehavior : GeneralBehavior
{
    // Attributes
    public int CoefPredictedPosition;

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

        desiredVelocity = (predictedPosition - _unit._position).normalized * _unit._maxSpeed;

        return desiredVelocity - _unit._velocity;
    }
}
