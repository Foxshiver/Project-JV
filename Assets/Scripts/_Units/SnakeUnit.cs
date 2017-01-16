using UnityEngine;
using System.Collections;

public class SnakeUnit : NPCUnit
{
    // Constructor
    public SnakeUnit()
    {
        _name = "Snake";
        _faction = 0;
        _fieldOfView = 3.0f;
        _healPoint = 7.0f;
        _damagePoint = 2.5f;
        _stateUnit = Unit.State.Wait;

        Debug.Log("SnakeUnit constructor called");
    }
}