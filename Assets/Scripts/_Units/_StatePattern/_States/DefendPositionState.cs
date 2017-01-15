using UnityEngine;
using System.Collections;

public class DefendPositionState : IUnitState {

    private readonly StatePatternUnit state;
    private PursuitBehavior pursuit;

    public DefendPositionState(StatePatternUnit statePatternUnit, PursuitBehavior pursuitBehavior)
    {
        state = statePatternUnit;
        pursuit = pursuitBehavior;
    }

    public void UpdateState()
    {
        Pursuit();
    }

    public void TriggeringUpdate()
    {
        // Si l'ennemie chassé est détruit ou trop éloigné du spawner Alors l'unité retourne au point à défendre
        ToHoldPositionState();
    }

    public void ToWaitState()
    { Debug.Log("Can't return to wait state"); }

    public void ToFollowLeaderState()
    { Debug.Log("Can't transition to follow state from defend state"); }

    public void ToHoldPositionState()
    { state.currentState = state.holdPositionState; }

    public void ToDefendPositionState()
    { Debug.Log("Can't transition to same state"); }

    public void ToAttackEnemyState()
    { Debug.Log("Can't transition to atack state from defend state"); }

    public void ToWorkState()
    { Debug.Log("Can't transition to work state from defend state"); }

    /*
     * Pursuit behavior
     * 
     * Allow to hunt enemy
     */
    private void Pursuit()
    {
        Vector2 steering = pursuit.computePursuitSteering(state._NPCUnit._unitTarget._currentPosition, state._NPCUnit._unitTarget._velocity);
        state._NPCUnit._currentPosition = pursuit.computeNewPosition(steering - pursuit.computeSteeringSeparationForce());

        state._NPCUnit.updatePosition(state._NPCUnit._currentPosition);
    }
}
