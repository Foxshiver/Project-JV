using UnityEngine;
using System.Collections;

public class ChickenUnit : NPCUnit
{
    // Constructor
    public ChickenUnit()
    {
        _name = "Chicken";
        _faction = 0;
        _fieldOfVision = 3.0f;
        _healPoint = 5.0f;
        _damagePoint = 2.5f;
        _stateUnit = Unit.State.Wait;

        Debug.Log("ChickenUnit constructor called");
    }
}