using UnityEngine;
using System.Collections;

public class ReachTargetState : IEnemyState
{

    private readonly StatePatternEnemy state;
    private SeekBehavior seek;

    // Constructor
    public ReachTargetState(StatePatternEnemy statePatternEnemy, SeekBehavior seekBehavior)
    {
        state = statePatternEnemy;
        seek = seekBehavior;
    }

    public void UpdateState()
    {
        //CheckAround();
        Seek();
    }


    public void ToReachTargetState()
    { Debug.Log("Can't transition to same state"); }

    public void ToAttackTargetState()
    { /*  Quand rentre dans zone */ }

    public void ToAttackUnitState() // TODO
    { state.currentState = state.attackUnitState; }

    /*
     * Seek behavior
     * 
     * Allow to follow the leader
     */
    private void Seek()
    {
        Vector2 steering = seek.computeSeekSteering(state._NPCUnit._unitTarget._currentPosition);
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
}

