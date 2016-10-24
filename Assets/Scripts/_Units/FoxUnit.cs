using UnityEngine;
using System.Collections;

public class FoxUnit : Unit
{
    // Constructor
    public FoxUnit()
    {
        Debug.Log("FoxUnit constructor called");
    }

	// Update is called once per frame
	void Update()
    {
        Vector2 steering = ((SeekBehavior)_behaviors[0]).computeSeekSteering(_targetUnit._behindPosition);
		_currentPosition = ((SeekBehavior)_behaviors[0]).computeNewPosition(steering - ((SeekBehavior)_behaviors[0]).computeSteeringSeparationForce());
        
        this.updatePosition(_currentPosition);
    }
}
