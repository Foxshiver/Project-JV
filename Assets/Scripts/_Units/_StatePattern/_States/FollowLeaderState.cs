using UnityEngine;
using System.Collections;

public class FollowLeaderState : IUnitState {

    private readonly StatePatternUnit state;
    private SeekBehavior seek;

    // Constructor
    public FollowLeaderState(StatePatternUnit statePatternUnit, SeekBehavior seekBehavior)
    {
        state = statePatternUnit;
        seek = seekBehavior;
    }

    public void UpdateState()
    {
        Seek();
    }

    public void TriggeringUpdate()
    {
        // 3 scénarios possibles
        // - Si le joueur appuie sur 'B' Alors l'unité qui le suit garde la position
        if(Input.GetButtonDown("HoldPosition"))
        {
            ToHoldPositionState();
            return;
        }

        // - Si le joueur appuie sur 'Y' ET qu'il est à proximité d'un champ Alors l'unité va travailler au champ
        if (Input.GetButtonDown("Work"))
        {
            ToWorkState();
            return;
        }

        // - Si le joueur est à proximité d'un ennemie Alors l'unité va attaquer
        float distance = (state._NPCUnit._unitTarget._currentPosition - state._NPCUnit.general._currentPosition).magnitude;

        if (distance <= state._NPCUnit.general._fieldOfView)
            ToAttackEnemyState();
    }

    public void ToWaitState()
    { Debug.Log("Can't return to wait state"); }

    public void ToFollowLeaderState()
    { Debug.Log("Can't transition to same state"); }

    public void ToHoldPositionState()
    { state.currentState = state.holdPositionState; }

    public void ToDefendPositionState()
    { Debug.Log("Can't transition to defend state from follow leader state"); }

    public void ToAttackEnemyState()
    { state.currentState = state.attackEnemyState; }

    public void ToWorkState()
    { state.currentState = state.workState; }

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
}
