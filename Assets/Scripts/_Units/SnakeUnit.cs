using UnityEngine;
using System.Collections;

public class SnakeUnit : NPCUnit
{
    // Constructor
    public SnakeUnit()
    {
        _name = "Snake";
        _stateUnit = Unit.State.Wait;

        Debug.Log("SnakeUnit constructor called");
    }

    // Update is called once per frame
    void Update()
    {
        _currentPosition = Vector3TOVector2(this.transform.position);
        _currentPosition = computePosition(_stateUnit);

        this.updatePosition(_currentPosition);
    }
}