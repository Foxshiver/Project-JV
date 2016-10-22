using UnityEngine;
using System.Collections;

public class LeaderBehavior : GeneralBehavior
{
    public float _radius;

    public LeaderBehavior() : base()
    {}

	void Start()
	{
		_position = this.transform.position;
	}

	void Update()
	{
		//_position = this.transform.position;

		_position = computeNewPosition(controllerMovement());
		this.transform.position = _position;
//		this.transform.position += controllerMovement();
//		_position = this.transform.position;

		//Debug.Log ("Velocity : " + _velocity);

	}

	Vector3 controllerMovement()
	{
		float forceHorizontal = Input.GetAxis ("Horizontal");
		float forceVertical = Input.GetAxis ("Vertical");

		Vector3 force = new Vector3 (forceHorizontal, 0, forceVertical);
		//Debug.Log ("mov vec : " + force);
//
		if (force == Vector3.zero) {
			_velocity *= 0.0f;
			Debug.Log ("Velocity : " + _velocity);
		}

		return force*2.0f;
	}


    public float getRadius()
    {
        return _radius;
    }

}
