﻿using UnityEngine;
using System.Collections;

public class SnakeUnit : Unit
{
    // Constructor
    public SnakeUnit()
    {
        _name = "Snake";
        Debug.Log("SnakeUnit constructor called");
    }

    // Update is called once per frame
    void Update()
    {
        if (_simpleTarget == null)
            return;

        _currentPosition = Vector3TOVector2(this.transform.position);

        Vector2 targetPosition = Vector3TOVector2(_simpleTarget.transform.position);

        Vector2 steering = ((WaitBehavior)_behaviors[5]).computeWaitSteering(targetPosition, 7.0f, 4.0f);
		_currentPosition = ((WaitBehavior)_behaviors[5]).computeNewPosition(steering - ((WaitBehavior)_behaviors[5]).computeSteeringSeparationForce());

        this.updatePosition(_currentPosition);
    }
}