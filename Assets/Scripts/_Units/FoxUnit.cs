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
        Vector2 targetPosition = Vector3TOVector2(_simpleTarget.transform.position);

        Vector2 steering = ((SeekBehavior)_behaviors[0]).computeSeekSteering(targetPosition);
        _position = ((SeekBehavior)_behaviors[0]).computeNewPosition(steering);
        
        this.updatePosition(_position);
    }   
}
