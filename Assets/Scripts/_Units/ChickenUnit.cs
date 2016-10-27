using UnityEngine;
using System.Collections;

public class ChickenUnit : Unit
{
    // Constructor
    public ChickenUnit()
    {
        Debug.Log("ChickenUnit constructor called");
    }

    // Update is called once per frame
    void Update()
    {
		_currentPosition = Vector3TOVector2(this.transform.position);

        Vector2 targetPosition = Vector3TOVector2(_simpleTarget.transform.position);

		Vector2 steering = ((WaitBehavior)_behaviors [5]).computeWaitSteering (targetPosition, 7.0f, 3.0f);
		_currentPosition = ((WaitBehavior)_behaviors[5]).computeNewPosition(steering - ((WaitBehavior)_behaviors[5]).computeSteeringSeparationForce());

        this.updatePosition(_currentPosition);
    }
}