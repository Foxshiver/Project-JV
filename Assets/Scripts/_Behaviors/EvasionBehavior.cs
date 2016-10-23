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
    public Vector2 computeEvasionSteering(Unit avoidUnit)
    {
        CoefPredictedPosition = 1;
        Vector2 predictedPosition = avoidUnit._position + avoidUnit._velocity * CoefPredictedPosition;

        Vector2 targetOffset = predictedPosition - _unit._position;
        float distance = targetOffset.magnitude;

        Vector2 desiredVelocity = (predictedPosition - _unit._position).normalized * _unit._maxSpeed;
        desiredVelocity *= -1;

        return desiredVelocity - _unit._velocity;
    }
}
