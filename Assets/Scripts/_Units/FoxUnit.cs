using UnityEngine;
using System.Collections;

public class FoxUnit : NPCUnit
{
    // Constructor
    public FoxUnit()
    {
        _name = "Fox";
        _faction = 0;
        _fieldOfView = 3.0f;
        _healPoint = 10.0f;
        _damagePoint = 2.5f;
        _stateUnit = Unit.State.Wait;

        Debug.Log("FoxUnit constructor called");
    }
}