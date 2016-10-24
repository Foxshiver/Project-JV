using UnityEngine;
using System.Collections;

public class GeneralBehavior {

    protected Unit _unit;

	private float CoefSteeringSeparation = 0.8f;
	private float _radiusUnit = 5.0f;
    
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

	public Vector2 computeSteeringSeparationForce()
	{
			Unit[] listOfNeighboors = ListOfNeighboors ();

			Vector2 steeringSeparation = Vector2.zero;

			for (int i = 0; i < listOfNeighboors.Length; i++)
			{

				//Debug.Log ("Voisin of " + _unit.name + " " + i + " = " + listOfNeighboors [i].name);
				Vector2 vecDistance = (listOfNeighboors [i]._position - this._unit._position);  //this._position);
				float distance = vecDistance.magnitude;

				steeringSeparation += vecDistance / distance;
			}
			//Debug.Log ("SteeringSep : " + steeringSeparation);
			return steeringSeparation * CoefSteeringSeparation;
	}

	public Unit[] ListOfNeighboors()
	{
		Unit[] listOfUnit = GameObject.FindObjectsOfType<Unit> ();
		bool[] isInRadius = new bool[listOfUnit.Length];

		int nbNeighboors = 0;

		for (int i = 0; i < listOfUnit.Length; i++) 
		{
			if (listOfUnit [i].gameObject != _unit.gameObject) // this.gameObject)
			{
				float distance = (listOfUnit [i].gameObject.transform.position - _unit.transform.position).magnitude;

				if (distance < _radiusUnit) {
					isInRadius [i] = true;
					nbNeighboors++;
				} else {
					isInRadius [i] = false;
				}
			}
		}

		int indiceNewList = 0;

		Unit[] listOfNeighboors = new Unit[nbNeighboors];
		for (int i = 0; i < listOfUnit.Length; i++)
		{
			if (isInRadius [i])
			{
				listOfNeighboors.SetValue (listOfUnit [i], indiceNewList);  //listOfNeighboors[indiceNewList] = listOfFollower [i];
				indiceNewList++;
			}
		}

		return listOfNeighboors;
	}
}
