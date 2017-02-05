using UnityEngine;
using System.Collections;

public class EvasionBehavior : GeneralBehavior
{
    // Attributes
    public int CoefPredictedPosition;

    // Constructor
    public EvasionBehavior(MovableEntity mc) : base(mc)
    { }

    // Functions
    public Vector2 computeEvasionSteering(Vector2 targetPosition, Vector2 targetVelocity)
    {
        CoefPredictedPosition = 1;
        Vector2 predictedPosition = targetPosition + targetVelocity * CoefPredictedPosition;

        Vector2 targetOffset = predictedPosition - _mc._currentPosition;
        float distance = targetOffset.magnitude;

        Vector2 desiredVelocity = (predictedPosition - _mc._currentPosition).normalized * _mc._maxSpeed;
        desiredVelocity *= -1;

        return desiredVelocity - _mc._velocity;
    }
}