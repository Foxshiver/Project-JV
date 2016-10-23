using UnityEngine;
using System.Collections;

public class SnakeUnit : Unit
{
    // Constructor
    public SnakeUnit()
    {
        Debug.Log("SnakeUnit constructor called");
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 steering = ((PursuitBehavior)_behaviors[2]).computePursuitSteering(_targetUnit);
        _position = ((PursuitBehavior)_behaviors[2]).computeNewPosition(steering);

        this.updatePosition(_position);
    }
}