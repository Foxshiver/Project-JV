﻿using UnityEngine;
using System.Collections;

public class PlayerUnit : Unit {

    // public GameObject test;

    // Constructor
    public PlayerUnit()
	{
<<<<<<< HEAD
		Debug.Log("PlayerUnit constructor called");
		_money = 100;
=======
        _name = "Player";
        Debug.Log("PlayerUnit constructor called");
>>>>>>> 58fa32dba1df0f1a89aead6f3ec1b4c47d74d6f6
	}

	// Update is called once per frame
	void Update()
	{
        _behindPosition = ((LeaderBehavior)_behaviors[4]).getBehindLeader();
        // test.transform.position = new Vector3(_behindPosition.x, 0.0f, _behindPosition.y);

        _currentPosition = Vector3TOVector2(this.transform.position);

		_currentPosition = ((LeaderBehavior)_behaviors[4]).computeNewPosition( ((LeaderBehavior)_behaviors [4]).controllerMovement() );
        updatePosition(_currentPosition);		
		
        //Debug.Log ("Velocity : " + _velocity);
	}  
}
