using UnityEngine;
using System.Collections;

public class FollowerBehavior : GeneralBehavior {

	//Attributes
	public float CoefRadiusTarget = 20.0f;
	int CoefPredictedPosition = 20;

	public Vector3 computeSeekSteering(LeaderBehavior target)
	{
		Vector3 targetOffset = target._position - _position;
		float distance = targetOffset.magnitude;
		Vector3 desiredVelocity;

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

	public Vector3 computePursuitSteering(LeaderBehavior target)
	{
		Vector3 targetOffsetTarget = target._position - _position;
		float distanceTarget = targetOffsetTarget.magnitude;

		if (distanceTarget < 1.0f)
			CoefPredictedPosition = (int)1;
		else if (distanceTarget > 10.0f)
			CoefPredictedPosition = (int)10;
		else
			CoefPredictedPosition = (int)distanceTarget;


		Vector3 predictedPosition = target._position + target._velocity * CoefPredictedPosition;

		//Debug.Log("Pursuit : " + predictedPosition);
		Vector3 targetOffset = predictedPosition - _position;
		float distance = targetOffset.magnitude;
		Vector3 desiredVelocity;

		desiredVelocity = (predictedPosition - _position).normalized * _maxSpeed;

		return desiredVelocity - _velocity;
	}

	public Vector3 computeFleeSteering(LeaderBehavior target)
	{
		Vector3 targetOffset = target._position - _position;
		float distance = targetOffset.magnitude;

		Vector3 desiredVelocity = (target._position - _position).normalized * _maxSpeed;
		desiredVelocity *= -1;

		return desiredVelocity - _velocity;
	}

	public Vector3 computeEvasionSteering(LeaderBehavior target)
	{
		Vector3 predictedPosition = target._position + target._velocity * CoefPredictedPosition;

		Vector3 targetOffset = predictedPosition - _position;
		float distance = targetOffset.magnitude;

		Vector3 desiredVelocity = (predictedPosition - _position).normalized * _maxSpeed;
		desiredVelocity *= -1;

		return desiredVelocity - _velocity;


	}
}
