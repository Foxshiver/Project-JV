using UnityEngine;
using System.Collections;

public class AttackTargetState : IEnemyState
{
    private readonly WavePattern state;
    private SeekBehavior seek;

    private double timeFirstCall = Time.time;

    public AttackTargetState(WavePattern statePatternEnemy, SeekBehavior seekBehavior)
    {
        state = statePatternEnemy;
        seek = seekBehavior;
    }

    public void UpdateState()
    {
        //CheckAround();
        actionFight();
        Seek();
    }

    public void ToAttackUnitState()
    { Debug.Log("Can't transition to same state"); }

    public void ToReachTargetState()
    { state.currentState = state.reachTargetState; }

    public void ToAttackTargetState()
    { Debug.Log("Can't transition to Attack target state from attack state"); }

    /*
     * Seek behavior
     * 
     * Allow to reach target
     */
    private void Seek()
    {
        Vector2 steering = seek.computeSeekSteering(state._unit._simpleTarget.position);
        state._unit._currentPosition = seek.computeNewPosition(steering - seek.computeSteeringSeparationForce());

        state._unit.updatePosition(state._unit._currentPosition);
    }

    //private void CheckAround()
    //{
    //    float distance = (state._NPCUnit._currentPosition - /* POSITION BNEAREST ENEMY */ ).magnitude;

    //    if (distance < state._NPCUnit._fieldOfView)
    //    {
    //        state._NPCUnit._simpleTarget = state._NPCUnit. /* NEAREST ENEMY */;
    //        ToAttackUnitState();
    //    }
    //}

    private void actionFight()
    {
        if ((Time.time - timeFirstCall) >= 1.0f)
        {
            timeFirstCall = Time.time;
            fight();
        }
    }

    // Fight function
    private void fight()
    {
        if (state._unit._simpleTarget != null)
        {
            FixedEntity enemy = state._unit._simpleTarget;

            float distance = (state._unit._currentPosition - state._unit._simpleTarget.position).magnitude;
            if (distance < state._unit._fieldOfView)
            {
                enemy.setHealPoint(enemy.getHealPoint()-2.0f);
            }
        }
    }
}
