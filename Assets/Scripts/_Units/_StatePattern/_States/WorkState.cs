using UnityEngine;
using System.Collections;

public class WorkState : IUnitState {

    private readonly RecruitmentPattern state;
    private WaitBehavior wait;
    private Field field;

    public WorkState(RecruitmentPattern statePatternUnit, WaitBehavior waitBehavior)
    {
        state = statePatternUnit;
        wait = waitBehavior;
    }

    public void UpdateState()
    {
        Work();
        EarnCoin();
    }

    public void TriggeringUpdate()
    {
        // Si le joueur appuie sur 'X' Alors l'unité repasse en état poursuite du joueur
        if(Input.GetButtonDown("CallBack"))
            ToFollowLeaderState();
    }

    public void ToWaitState()
    { Debug.Log("Can't return to wait state"); }

    public void ToFollowLeaderState()
    { state.currentState = state.followLeaderState; }

    public void ToHoldPositionState()
    { Debug.Log("Can't transition to hold position state from work state"); }

    public void ToDefendPositionState()
    { Debug.Log("Can't transition to defend state from work state"); }

    public void ToAttackEnemyState()
    { Debug.Log("Can't transition to atack state from work state"); }

    public void ToWorkState()
    { Debug.Log("Can't transition to same state"); }

    /*
     * Wait behavior
     * 
     * Allow to initialize neutral unit position at the beggining
     */
    private void Work()
    {
        Vector2 steering = wait.computeWaitSteering(state._unit._simpleTarget.position, state._unit.sizeRadius, state._unit.timeBeforeChangePos);
        state._unit._currentPosition = wait.computeNewPosition(steering - wait.computeSteeringSeparationForce());

        state._unit.updatePosition(state._unit._currentPosition);
    }

    private void EarnCoin()
    {
        field = (Field)state._unit._simpleTarget;

        if(Random.Range(0.0f, 1.0f) < 0.001f)
            field.createCoin();
    }
}
