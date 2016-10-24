﻿using UnityEngine;
using System.Collections;

public class LeaderBehavior : GeneralBehavior
{
    public float _radius;
    private int LEADER_BEHIND_DIST = 5;

    public LeaderBehavior(Unit unit) : base(unit)
    {
        Debug.Log("LeaderBehavior constructor called");
    }

	public Vector2 controllerMovement()
	{
		float forceHorizontal = Input.GetAxis ("Horizontal");
		float forceVertical = Input.GetAxis ("Vertical");

		Vector2 force = new Vector2 (forceHorizontal, forceVertical);

		if (force == Vector2.zero) {
			_unit._velocity *= 0.0f;
		}

		return force*2.0f;
	}

    // Functions
    public float getRadius() {
        return _radius;
    }

    public Vector2 getBehindLeader()
    {
        Vector2 behind;

        Vector2 tv = _unit._velocity * -1;
        tv = tv * LEADER_BEHIND_DIST;

        behind = _unit._position + tv;

        return behind;
    }
}
