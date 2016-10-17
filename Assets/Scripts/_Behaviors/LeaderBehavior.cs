using UnityEngine;
using System.Collections;

public class LeaderBehavior : GeneralBehavior
{
    public float _radius;

    public LeaderBehavior() : base()
    {}

	void Update()
	{
		_position = this.transform.position;

	}

    public float getRadius()
    {
        return _radius;
    }
}
