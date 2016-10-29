using UnityEngine;
using System.Collections;

public class ChickenUnit : Unit
{
    // Constructor
    public ChickenUnit()
    {
        _name = "Chicken";
        Debug.Log("ChickenUnit constructor called");
    }

    // Update is called once per frame
    void Update()
    {
		_currentPosition = Vector3TOVector2(this.transform.position);

		switch (_stateUnit)
		{
		case Unit.State.WaitFree: 
			_currentPosition = useWaitBehavior(_simpleTarget, 7.0f, 4.0f);
			if (Input.GetButtonDown("JoystickA")
				&& (new Vector2(_simpleTarget.transform.position.x,_simpleTarget.transform.position.z) - _targetUnit._currentPosition).magnitude < 5.0f)
			{
				_stateUnit = Unit.State.SeekLeader;
			}
			break;
		case Unit.State.SeekLeader:
			_currentPosition = useFleeBehavior();
			if (Input.GetButtonDown("JoystickB"))
			{
				_stateUnit = Unit.State.WaitFree;
			}
			break;

		}


//		if(Input.GetButtonDown("JoystickA"))
//		{
//			Debug.Log("PRESS A");
//			_stateUnit = Unit.State.WaitFree;
//		}
//
//		if(Input.GetButtonDown("JoystickB"))
//		{
//			Debug.Log("PRESS B");
//			_stateUnit = Unit.State.SeekLeader;
//		}

        this.updatePosition(_currentPosition);
    }

	public Vector2 useWaitBehavior (GameObject simpleTarget, float distance, float timeBeforeChangePos)
	{
		Vector2 targetPosition = Vector3TOVector2(simpleTarget.transform.position);

		Vector2 steering = ((WaitBehavior)_behaviors [5]).computeWaitSteering (targetPosition, distance, timeBeforeChangePos);
		return ((WaitBehavior)_behaviors[5]).computeNewPosition(steering - ((WaitBehavior)_behaviors[5]).computeSteeringSeparationForce());
	}

	public Vector2 useFleeBehavior ()
	{
		Vector2 steering = ((FleeBehavior)_behaviors[1]).computeFleeSteering(_targetUnit._behindPosition);
		return ((FleeBehavior)_behaviors[1]).computeNewPosition(steering - ((SeekBehavior)_behaviors[0]).computeSteeringSeparationForce());
	}

}