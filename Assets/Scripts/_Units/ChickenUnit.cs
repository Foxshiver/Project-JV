using UnityEngine;
using System.Collections;

public class ChickenUnit : NPCUnit
{
    // Constructor
    public ChickenUnit()
    {
        _name = "Chicken";
        _stateUnit = Unit.State.Wait;

        Debug.Log("ChickenUnit constructor called");
    }

    // Update is called once per frame
    void Update()
    {
        _currentPosition = Vector3TOVector2(this.transform.position);

        _currentPosition = computePosition(_stateUnit);

        //switch (_stateUnit)
        //{
        //case Unit.State.WaitFree: 
        //	_currentPosition = useWaitBehavior(_simpleTarget, 7.0f, 4.0f);
        //	if (Input.GetButtonDown("JoystickA"))
        //		&& (new Vector2(_simpleTarget.transform.position.x,_simpleTarget.transform.position.z) - _targetUnit._currentPosition).magnitude < 5.0f)
        //	{
        //		_stateUnit = Unit.State.PursuitLeader;
        //	}
        //	break;
        //case Unit.State.PursuitLeader:
        //	_currentPosition = useFleeBehavior();
        //	if (Input.GetButtonDown("JoystickB"))
        //	{
        //		_stateUnit = Unit.State.WaitFree;
        //	}
        //	break;

        //}


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
}