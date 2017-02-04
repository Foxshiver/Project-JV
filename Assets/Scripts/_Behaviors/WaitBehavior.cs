using UnityEngine;
using System.Collections;

public class WaitBehavior : SeekBehavior
{
	// Attributes
	protected Vector2 _currentTarget = Vector2.zero;

	private Vector2 _randomTarget;
	private float _startTime;

	// Constructor
	public WaitBehavior(MovableEntity mc) : base(mc)
    { }

	public Vector2 computeWaitSteering(Vector2 centerTargetPosition, float distance, float timeBeforeChangePos)
	{
		if ((centerTargetPosition != _currentTarget) || ((Time.time - _startTime) > timeBeforeChangePos))
        {
            _mc.timeBeforeChangePos = Random.Range(3.0f, 6.0f);
            _startTime = Time.time;

			_currentTarget = centerTargetPosition;

			float newPositionOnX = Random.Range ((centerTargetPosition.x - distance), (centerTargetPosition.x + distance));
			float newPositionOnY = Random.Range ((centerTargetPosition.y - distance), (centerTargetPosition.y + distance));

			_randomTarget = new Vector2 (newPositionOnX, newPositionOnY);
		}

		return computeSeekSteering(_randomTarget);
	}
//
//	// Functions
//	public Vector2 computeSeekSteering(Vector2 targetPosition)
//	{
//		Vector2 targetOffset = targetPosition - _unit._currentPosition;
//		float distance = targetOffset.magnitude;
//		Vector2 desiredVelocity;
//
//		// Arrival Behavior
//		float targetRadius = 5.0f;
//
//		if (distance < targetRadius)
//		{
//			float rampedSpeed = _unit._maxSpeed * (distance / (targetRadius * CoefRadiusTarget));
//			float clippedSpeed = Mathf.Min(rampedSpeed, _unit._maxSpeed);
//			desiredVelocity = (clippedSpeed / distance) * targetOffset;
//		}
//		else
//		{
//			desiredVelocity = (targetPosition - _unit._currentPosition).normalized * _unit._maxSpeed;
//		}
//
//		return desiredVelocity - _unit._velocity;
//	}
}
