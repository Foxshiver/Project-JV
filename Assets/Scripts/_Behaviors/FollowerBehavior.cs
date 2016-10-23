using UnityEngine;
using System.Collections;

public class FollowerBehavior : GeneralBehavior {

	//Attributes
	public float CoefRadiusTarget = 20.0f;
	int CoefPredictedPosition = 20;
	public float CoefSteeringSeparation = 0.4f;

	float radiusFollower = 5.0f;

	public Vector2 computeSeekSteering(LeaderBehavior target)
	{
		Vector2 targetOffset = target._position - _position;
		float distance = targetOffset.magnitude;
		Vector2 desiredVelocity;

		if (distance < target.getRadius())
		{
			float rampedSpeed = _maxSpeed * (distance / (target.getRadius() * CoefRadiusTarget));

			//Debug.Log(rampedSpeed);

			float clippedSpeed = Mathf.Min(rampedSpeed, _maxSpeed);
			desiredVelocity = (clippedSpeed / distance) * targetOffset;
		}
		else
		{
			desiredVelocity = (target._position - _position).normalized * _maxSpeed;
		}
			
		return desiredVelocity - _velocity;
	}

	public Vector2 computePursuitSteering(LeaderBehavior target)
	{
		Vector2 targetOffsetTarget = target._position - _position;
		float distanceTarget = targetOffsetTarget.magnitude;

		if (distanceTarget < 1.0f)
			CoefPredictedPosition = (int)1;
		else if (distanceTarget > 10.0f)
			CoefPredictedPosition = (int)10;
		else
			CoefPredictedPosition = (int)distanceTarget;


		Vector2 predictedPosition = target._position + target._velocity * CoefPredictedPosition;

		//Debug.Log("Pursuit : " + predictedPosition);
		Vector2 targetOffset = predictedPosition - _position;
		float distance = targetOffset.magnitude;
		Vector2 desiredVelocity;

		desiredVelocity = (predictedPosition - _position).normalized * _maxSpeed;

		return desiredVelocity - _velocity;
	}

	public Vector2 computeFleeSteering(LeaderBehavior target)
	{
		Vector2 targetOffset = target._position - _position;
		float distance = targetOffset.magnitude;

		Vector2 desiredVelocity = (target._position - _position).normalized * _maxSpeed;
		desiredVelocity *= -1;

		return desiredVelocity - _velocity;
	}

	public Vector2 computeEvasionSteering(LeaderBehavior target)
	{
		Vector2 predictedPosition = target._position + target._velocity * CoefPredictedPosition;

		Vector2 targetOffset = predictedPosition - _position;
		float distance = targetOffset.magnitude;

		Vector2 desiredVelocity = (predictedPosition - _position).normalized * _maxSpeed;
		desiredVelocity *= -1;

		return desiredVelocity - _velocity;


	}

	public Vector2 computeSeparationSteering()
	{
		FollowerBehavior[] listOfNeighboors = ListOfNeighboors ();

//		//Debug
//		foreach (FollowerBehavior neighboors in listOfNeighboors)
//		{
//			Debug.Log ("neighboors of " + this.name + " : " + neighboors.name);
//		}

		Vector2 steeringSeparation = Vector2.zero;

		for (int i = 0; i < listOfNeighboors.Length; i++)
		{
			Vector2 vecDistance = (listOfNeighboors [i]._position - this._position);
			float distance = vecDistance.magnitude;

			steeringSeparation += vecDistance / distance;

		}
		//Debug.Log ("SteeringSep : " + steeringSeparation);
		return steeringSeparation * CoefSteeringSeparation;
	}

	public FollowerBehavior[] ListOfNeighboors()
	{
		FollowerBehavior[] listOfFollower = FindObjectsOfType<FollowerBehavior> ();
		bool[] isInRadius = new bool[listOfFollower.Length];

		int nbNeighboors = 0;

		for (int i = 0; i < listOfFollower.Length; i++) 
		{
			if (listOfFollower [i].gameObject != this.gameObject)
			{
				float distance = (listOfFollower [i].gameObject.transform.position - this.transform.position).magnitude;

				if (distance < radiusFollower) {
					isInRadius [i] = true;
					nbNeighboors++;
				} else {
					isInRadius [i] = false;
				}
			}
		}

		int indiceNewList = 0;

		FollowerBehavior[] listOfNeighboors = new FollowerBehavior[nbNeighboors];
		for (int i = 0; i < listOfFollower.Length; i++)
		{
			if (isInRadius [i])
			{
				listOfNeighboors.SetValue (listOfFollower [i], indiceNewList);  //listOfNeighboors[indiceNewList] = listOfFollower [i];
				indiceNewList++;
			}
		}

		return listOfNeighboors;
	}
}
