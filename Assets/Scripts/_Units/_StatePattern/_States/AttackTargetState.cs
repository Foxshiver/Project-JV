using UnityEngine;
using System.Collections;

public class AttackTargetState : IEnemyState
{
    private readonly StatePatternEnemy state;
    private SeekBehavior seek;

    private double timeFirstCall = Time.time;

    public AttackTargetState(StatePatternEnemy statePatternEnemy, SeekBehavior seekBehavior)
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
     * Pursuit behavior
     * 
     * Allow to hunt enemy
     */
    private void Seek()
    {
        Vector2 steering = seek.computeSeekSteering(state._NPCUnit._simpleTarget.position);
        state._NPCUnit._currentPosition = seek.computeNewPosition(steering - seek.computeSteeringSeparationForce());

        state._NPCUnit.updatePosition(state._NPCUnit._currentPosition);
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
        if (state._NPCUnit._unitTarget != null)
        {
            Unit enemy = state._NPCUnit._unitTarget;

            float distance = (state._NPCUnit._currentPosition - state._NPCUnit._unitTarget._currentPosition).magnitude;
            if (distance < state._NPCUnit._fieldOfView)
            {
                float healPointRemaining = enemy.getHealPoint() - 2.5f;
                enemy.setHealPoint(healPointRemaining);
            }
        }
    }
}
