using UnityEngine;
using System.Collections;

public class EvasionBehavior : GeneralBehavior
{
    // Attributes
    public int CoefPredictedPosition;

    // Constructor
    public EvasionBehavior(Unit unit) : base(unit)
    {
        Debug.Log("EvasionBehavior constructor called");
    }

    // Functions
    public Vector2 computeEvasionSteering(Vector2 targetPosition, Vector2 targetVelocity)
    {
        CoefPredictedPosition = 1;
        Vector2 predictedPosition = targetPosition + targetVelocity * CoefPredictedPosition;

        Vector2 targetOffset = predictedPosition - _unit._currentPosition;
        float distance = targetOffset.magnitude;

        Vector2 desiredVelocity = (predictedPosition - _unit._currentPosition).normalized * _unit._maxSpeed;
        desiredVelocity *= -1;

        return desiredVelocity - _unit._velocity;
    }
}