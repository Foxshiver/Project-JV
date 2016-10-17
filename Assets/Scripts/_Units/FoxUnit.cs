using UnityEngine;
using System.Collections;

public class FoxUnit : FollowerBehavior {

	public int stateSteering = 0;

	//Attributes
	public LeaderBehavior _target;


	// Use this for initialization
	void Start () {
		_position = this.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		if(stateSteering == 0)
			_position = computeNewPosition(computeSeekSteering(_target));
		if(stateSteering == 1)
			_position = computeNewPosition(computeFleeSteering(_target));
		if(stateSteering == 2)
			_position = computeNewPosition(computePursuitSteering(_target));
		if(stateSteering == 3)
			_position = computeNewPosition(computeEvasionSteering(_target));
		this.transform.position = _position;	
	}
}
