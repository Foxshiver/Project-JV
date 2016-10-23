using UnityEngine;
using System.Collections;

public class LeaderBehavior : GeneralBehavior
{
    public float _radius;

    public LeaderBehavior() : base()
    {}

	void Start()
	{
		_position = new Vector2(this.transform.position.x,this.transform.position.z);
	}

	void Update()
	{
		//_position = this.transform.position;

		_position = computeNewPosition(controllerMovement());
		this.transform.position = new Vector3(_position.x,0.0f,_position.y);
//		this.transform.position += controllerMovement();
//		_position = this.transform.position;

		//Debug.Log ("Velocity : " + _velocity);

	}

	Vector2 controllerMovement()
	{
		float forceHorizontal = Input.GetAxis ("Horizontal");
		float forceVertical = Input.GetAxis ("Vertical");

		Vector2 force = new Vector2 (forceHorizontal, forceVertical);
		//Debug.Log ("mov vec : " + force);
//
		if (force == Vector2.zero) {
			_velocity *= 0.0f;
			//Debug.Log ("Velocity : " + _velocity);
		}

		return force*2.0f;
	}


    public float getRadius()
    {
        return _radius;
    }

}
