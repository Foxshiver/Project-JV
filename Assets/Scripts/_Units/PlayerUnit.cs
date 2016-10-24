using UnityEngine;
using System.Collections;

public class PlayerUnit : Unit {

    public GameObject test;

    // Constructor
    public PlayerUnit()
	{
		Debug.Log("PlayerUnit constructor called");
	}

	// Update is called once per frame
	void Update()
	{
        _behindPosition = ((LeaderBehavior)_behaviors[4]).getBehindLeader();
        test.transform.position = new Vector3(_behindPosition.x, 0.0f, _behindPosition.y);

        _currentPosition = Vector3TOVector2(this.transform.position);

		_currentPosition = ((LeaderBehavior)_behaviors [4]).computeNewPosition( ((LeaderBehavior)_behaviors [4]).controllerMovement() );
        updatePosition(_currentPosition);		
		
        //Debug.Log ("Velocity : " + _velocity);
	}  
}
