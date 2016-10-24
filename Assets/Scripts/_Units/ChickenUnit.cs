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
        Vector2 targetPosition = Vector3TOVector2(_simpleTarget.transform.position);

        Vector2 steering = ((FleeBehavior)_behaviors[1]).computeFleeSteering(targetPosition);
        _currentPosition = ((FleeBehavior)_behaviors[1]).computeNewPosition(steering);

        this.updatePosition(_currentPosition);
    }
}