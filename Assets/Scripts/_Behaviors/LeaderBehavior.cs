﻿using UnityEngine;
using System.Collections;

public class LeaderBehavior : GeneralBehavior
{
    public float _radius;
    private int _joystickNumber;
    private float LEADER_BEHIND_DIST = 5.0f;

    public LeaderBehavior(MovableEntity mc, int joystickNumber) : base(mc)
    {
        _joystickNumber = joystickNumber;
    }

	public Vector2 controllerMovement()
	{
		float forceHorizontal = Input.GetAxis ("Horizontal_" + _joystickNumber.ToString());
		float forceVertical = Input.GetAxis ("Vertical_" + _joystickNumber.ToString());

		Vector2 force = new Vector2 (forceHorizontal, forceVertical);

		if (force == Vector2.zero)
			_mc._velocity *= 0.0f;

		return force*2.0f;
	}

    public Vector2 getBehindLeader()
    {
        Vector2 behind;

        Vector2 tv = new Vector2(1,0);

        if (_mc._velocity != Vector2.zero)
            tv = _mc._velocity * -1;

        tv = tv.normalized * LEADER_BEHIND_DIST;

        behind = _mc._currentPosition + tv;

        return behind;
    }
}
