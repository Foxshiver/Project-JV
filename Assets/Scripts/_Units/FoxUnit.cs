using UnityEngine;
using System.Collections;

public class FoxUnit : Unit
{
    // Constructor
    public FoxUnit()
    {
        _name = "Fox";
        Debug.Log("FoxUnit constructor called");
    }

	// Update is called once per frame
	void Update()
    {
        if (_simpleTarget == null)
        {
            Debug.Log("simple target is null");

            return;
        }

        
        _currentPosition = Vector3TOVector2(this.transform.position);

        Vector2 targetPosition = Vector3TOVector2(_simpleTarget.transform.position);
        
        Vector2 steering = ((WaitBehavior)_behaviors[5]).computeWaitSteering(targetPosition, 7.0f, 4.0f);
        Debug.Log("simple target is NOT null " + steering);

        _currentPosition = ((WaitBehavior)_behaviors[5]).computeNewPosition(steering - ((WaitBehavior)_behaviors[5]).computeSteeringSeparationForce());

        this.updatePosition(_currentPosition);
    }
}
