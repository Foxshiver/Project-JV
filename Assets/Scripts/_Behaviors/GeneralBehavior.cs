using UnityEngine;
using System.Collections;

public class GeneralBehavior {

    protected Unit _unit;

    public GeneralBehavior(Unit unit)
    {
        _unit = unit;
    }

	public Vector2 computeNewPosition(Vector2 steering)
    {
        _unit._steering = Vector2.ClampMagnitude(steering, _unit._maxSteeringForce); // steering return the force corresponding at the behavior send by param
        Vector2 acceleration = _unit._steering / _unit._masse;
        _unit._velocity = Vector2.ClampMagnitude(_unit._velocity + acceleration /* *Time.deltaTime*/, _unit._maxSpeed);
        
		return _unit._position + _unit._velocity * Time.deltaTime;
    }
}
