using UnityEngine;
using System.Collections;

public class FoxUnit : FollowerBehavior {

	public int stateSteering = 0;

	//Attributes
	public LeaderBehavior _target;


	// Use this for initialization
	void Start () {
		_position = new Vector2 (this.transform.position.x,this.transform.position.z);
	}
	
	// Update is called once per frame
	void Update () {
		if(stateSteering == 0)
			_position = computeNewPosition(computeSeekSteering(_target)-computeSeparationSteering());
		if(stateSteering == 1)
			_position = computeNewPosition(computeFleeSteering(_target)-computeSeparationSteering());
		if(stateSteering == 2)
			_position = computeNewPosition(computePursuitSteering(_target)-computeSeparationSteering());
		if(stateSteering == 3)
			_position = computeNewPosition(computeEvasionSteering(_target)-computeSeparationSteering());
		
		this.transform.position = new Vector3(_position.x,0.0f,_position.y);	
	}
}
