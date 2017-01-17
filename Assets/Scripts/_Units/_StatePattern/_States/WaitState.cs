﻿using UnityEngine;
using System.Collections;

public class WaitState : IUnitState {

    private readonly StatePatternUnit state;
    private WaitBehavior wait;

    // Constructor
    public WaitState(StatePatternUnit statePatternUnit, WaitBehavior waitBehavior)
    {
        state = statePatternUnit;
        wait = waitBehavior;
    }

    public void UpdateState()
    {
        Wait();
    }

    public void TriggeringUpdate()
    {
        if(Input.GetButtonDown("TakeUnitOn"))
            ToFollowLeaderState();
    }

    public void ToWaitState()
    { Debug.Log("Can't transition to same state"); }

    public void ToFollowLeaderState()
    { state.currentState = state.followLeaderState; }

    public void ToHoldPositionState()
    { Debug.Log("Can't transition to hold position state from wait state"); }

    public void ToDefendPositionState()
    { Debug.Log("Can't transition to defend state from wait state"); }

    public void ToAttackEnemyState()
    { Debug.Log("Can't transition to atack state from wait state"); }

    public void ToWorkState()
    { Debug.Log("Can't transition to work state from wait state"); }

    /*
     * Wait behavior
     * 
     * Allow to initialize neutral unit position at the beggining
     */
    private void Wait()
    {
        Vector2 steering = wait.computeWaitSteering(state._NPCUnit._simpleTarget.position, state._NPCUnit.sizeRadius, state._NPCUnit.timeBeforeChangePos);
        state._NPCUnit._currentPosition = wait.computeNewPosition(steering - wait.computeSteeringSeparationForce());

        state._NPCUnit.updatePosition(state._NPCUnit._currentPosition);
    }
}