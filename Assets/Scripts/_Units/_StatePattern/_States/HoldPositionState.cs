using UnityEngine;
using System.Collections;

public class HoldPositionState : IUnitState {

    private readonly RecruitmentPattern state;
    private WaitBehavior wait;

    public HoldPositionState(RecruitmentPattern statePatternUnit, WaitBehavior waitBehavior)
    {
        state = statePatternUnit;
        wait = waitBehavior;
    }

    public void UpdateState()
    {
        checkAround();
        Wait();
    }

    public void TriggeringUpdate()
    {
        // 2 scenarios possibles
        // - Si des unités ennemies passent à proximité du point à défendre Alors l'unité attaque
        // - Si le joueur appuie sur 'X' Alors l'unité repasse en état de poursuite du joueur
        if(Input.GetButtonDown("CallBack"))
            ToFollowLeaderState();
    }

    public void ToWaitState()
    { Debug.Log("Can't return to wait state"); }

    public void ToFollowLeaderState()
    {
        state.currentState = state.followLeaderState;
        state._unit._animator.SetBool("IsWorking", false);
        state._unit._animator.SetBool("IsAttacking", false);
    }

    public void ToHoldPositionState()
    { Debug.Log("Can't transition to same state"); }

    public void ToDefendPositionState()
    {
        state.currentState = state.defendPositionState;
        state._unit._animator.SetBool("IsWorking", false);
        state._unit._animator.SetBool("IsAttacking", false);
    }

    public void ToAttackEnemyState()
    { Debug.Log("Can't transition to attack state from hold position state"); }

    public void ToWorkState()
    { Debug.Log("Can't transition to work state from hold position state"); }

    /*
     * Wait behavior
     * 
     * Allow to initialize neutral unit position at the beggining
     */
    private void Wait()
    {
        Vector2 steering = wait.computeWaitSteering(state._unit._simpleTarget.position, state._unit.sizeRadius, state._unit.timeBeforeChangePos);
        state._unit._currentPosition = wait.computeNewPosition(steering - wait.computeSteeringSeparationForce());

        state._unit.updatePosition(state._unit._currentPosition);
    }

    private void checkAround()
    {
        if(state._unit._simpleTarget.nearestEnemy == null)
            return;
        
        float distance = (state._unit._simpleTarget.position - state._unit._simpleTarget.nearestEnemy._currentPosition).magnitude;

        if (distance < state._unit._simpleTarget.defendingArea)
        {
            state._unit._unitTarget = state._unit._simpleTarget.nearestEnemy;
            ToDefendPositionState();
        }
    }
}
