using UnityEngine;
using System.Collections;

public class FollowLeaderState : IUnitState {

    private readonly RecruitmentPattern state;
    private SeekBehavior seek;
    
    // Constructor
    public FollowLeaderState(RecruitmentPattern statePatternUnit, SeekBehavior seekBehavior)
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

        if(state._unit.general.nearToTarget)
        {
            ToAttackTargetState();
            return;
        }

        // - Si le joueur est à proximité d'un ennemie Alors l'unité va attaquer
        ToAttackEnemyState();
    }

    public void ToWaitState()
    { Debug.Log("Can't return to wait state"); }

    public void ToFollowLeaderState()
    { Debug.Log("Can't transition to same state"); }

    public void ToHoldPositionState()
    {
        state.currentState = state.holdPositionState;
        state._unit._animatorEntity.SetBool("IsWorking", false);
        state._unit._animatorEntity.SetBool("IsAttacking", false);
    }

    public void ToDefendPositionState()
    { Debug.Log("Can't transition to defend state from follow leader state"); }

    public void ToAttackEnemyState()
    {
        state.currentState = state.attackEnemyState;
        state._unit._animatorEntity.SetBool("IsWorking", false);
        state._unit._animatorEntity.SetBool("IsAttacking", true);
    }

    public void ToAttackTargetState()
    {
        state.currentState = state.attackTargetState;
        state._unit._animatorEntity.SetBool("IsWorking", false);
        state._unit._animatorEntity.SetBool("IsAttacking", true);
    }

    public void ToWorkState()
    {
        state.currentState = state.workState;
        state._unit._animatorEntity.SetBool("IsAttacking", false);
        state._unit._animatorEntity.SetBool("IsWorking", true);
    }

    /*
     * Seek behavior
     * 
     * Allow to follow the leader
     */
    private void Seek()
    {
        Vector2 steering = seek.computeSeekSteering(state._unit._unitTarget._currentPosition);
        state._unit._currentPosition = seek.computeNewPosition(steering - seek.computeSteeringSeparationForce());

        state._unit.updatePosition(state._unit._currentPosition);
    }
}
