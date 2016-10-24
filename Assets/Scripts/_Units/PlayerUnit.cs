using UnityEngine;
using System.Collections;

public class PlayerUnit : Unit {

	// Constructor
	public PlayerUnit()
	{
		Debug.Log("PlayerUnit constructor called");
	}

	// Update is called once per frame
	void Update()
	{
		_position = Vector3TOVector2(this.transform.position);

		_position = ((LeaderBehavior)_behaviors [4]).computeNewPosition( ((LeaderBehavior)_behaviors [4]).controllerMovement() );
		//this.transform.position = new Vector3(_position.x,0.0f,_position.y);
		updatePosition(_position);
//		this.transform.position += ((LeaderBehavior)_behaviors [4]).controllerMovement();
//		_position = this.transform.position;
		
				//Debug.Log ("Velocity : " + _velocity);
	}  
}
