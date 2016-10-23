using UnityEngine;
using System.Collections;

public class FoxUnit : Unit
{
    // Constructor
    public FoxUnit()
    {
        Debug.Log("FoxUnit constructor called");
    }

	// Update is called once per frame
	void Update()
    {
        Vector2 targetPosition = Vector3TOVector2(_simpleTarget.transform.position);

        Vector2 steering = ((SeekBehavior)_behaviors[0]).computeSeekSteering(targetPosition);
        _position = ((SeekBehavior)_behaviors[0]).computeNewPosition(steering);

<<<<<<< HEAD
        this.updatePosition(_position);
    }   
=======
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
>>>>>>> d258e6e0d12ff2315ebcdbe1af461b381674093a
}
