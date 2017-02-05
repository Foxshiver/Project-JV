using UnityEngine;
using System.Collections;

public class ReachTargetState : IEnemyState
{
    private readonly WavePattern state;
    private SeekBehavior seek;

    // Constructor
    public ReachTargetState(WavePattern statePatternEnemy, SeekBehavior seekBehavior)
    {
        state = statePatternEnemy;
        seek = seekBehavior;
    }

    public void UpdateState()
    {
        CheckEnemyAround();
        CheckTargetAround();
        Seek();
    }


    public void ToReachTargetState()
    { Debug.Log("Can't transition to same state"); }

    public void ToAttackTargetState()
    { state.currentState = state.attackTargetState; }

    public void ToAttackUnitState()
    { state.currentState = state.attackUnitState; }

    /*
     * Seek behavior
     * 
     * Allow to follow the leader
     */
    private void Seek()
    {
        Vector2 steering = seek.computeSeekSteering(state._unit._simpleTarget.position);
        state._unit._currentPosition = seek.computeNewPosition(steering - seek.computeSteeringSeparationForce());

        state._unit.updatePosition(state._unit._currentPosition);
    }

    private void CheckEnemyAround()
    {
        //float distance = (state._unit._currentPosition - /* POSITION NEAREST ENEMY */ ).magnitude;

        //if(distance < state._unit._fieldOfView)
        //{
        //    state._unit._simpleTarget = state._unit. /* NEAREST ENEMY */;
        //    ToAttackUnitState();
        //}
    }

    private void CheckTargetAround()
    {
        float distance = (state._unit._currentPosition - state._unit._simpleTarget.position).magnitude;

        if(distance < state._unit._fieldOfView)
        {
            ToAttackTargetState();
        }
    }
}

