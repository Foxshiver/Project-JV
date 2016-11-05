﻿using UnityEngine;
using System.Collections;

public class FoxUnit : NPCUnit
{
    // Constructor
    public FoxUnit()
    {
        _name = "Fox";
        _stateUnit = Unit.State.Wait;

        Debug.Log("FoxUnit constructor called");
    }

	// Update is called once per frame
	void Update()
    {
        _currentPosition = Vector3TOVector2(this.transform.position);
        _currentPosition = computePosition(_stateUnit);

        this.updatePosition(_currentPosition);
    }
}
