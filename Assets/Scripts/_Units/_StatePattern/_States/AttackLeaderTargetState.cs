using UnityEngine;
using System.Collections;

public class AttackLeaderTargetState : IUnitState
{
    private readonly RecruitmentPattern state;
    private SeekBehavior seek;

    private double timeFirstCall = Time.time;

    private Animator animator;

    public AttackLeaderTargetState(RecruitmentPattern statePatternUnit, SeekBehavior seekBehavior)
    {
        state = statePatternUnit;
        seek = seekBehavior;
    }

    public void UpdateState()
    {
        checkAround();
        actionFight();
        Seek();
    }

    public void TriggeringUpdate()
    {
        // 2 scénarios possibles
        // - Si le joueur appuie sur 'B' Alors l'unité qui le suit garde la position
        if(Input.GetButtonDown("HoldPosition"))
        {
            ToHoldPositionState();
            return;
        }

        // - Si le joueur appuie sur 'Y' ET qu'il est à proximité d'un champ Alors l'unité va travailler au champ
        if(Input.GetButtonDown("Work"))
        {
            ToWorkState();
            return;
        }
    }

    public void ToWaitState()
    { Debug.Log("Can't return to wait state"); }

    public void ToFollowLeaderState()
    {
        state.currentState = state.followLeaderState;
        state._unit._animatorEntity.SetBool("IsWorking", false);
        state._unit._animatorEntity.SetBool("IsAttacking", false);
    }

    public void ToHoldPositionState()
    {
        state.currentState = state.holdPositionState;
        state._unit._animatorEntity.SetBool("IsWorking", false);
        state._unit._animatorEntity.SetBool("IsAttacking", false);
    }

    public void ToDefendPositionState()
    { Debug.Log("Can't transition to defend state from attack target state"); }

    public void ToAttackEnemyState()
    { Debug.Log("Can't transition to attack enemy state from attack target state"); }

    public void ToAttackTargetState()
    { Debug.Log("Can't transition to same state"); }

    public void ToWorkState()
    {
        state.currentState = state.workState;
        state._unit._animatorEntity.SetBool("IsAttacking", false);
        state._unit._animatorEntity.SetBool("IsWorking", true);
    }

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

    private void checkAround()
    {
        float distance = (state._unit._unitTarget._currentPosition - state._unit.general._currentPosition).magnitude;

        if(distance > state._unit.general._fieldOfView)
        {
            state._unit._unitTarget = state._unit.general;
            ToFollowLeaderState();
        }
    }

    private void actionFight()
    {
        if((Time.time - timeFirstCall) >= 1.0f)
        {
            timeFirstCall = Time.time;
            fight();
        }
    }

    // Fight function
    private void fight()
    {
        if(state._unit._simpleTarget != null)
        {
            FixedEntity enemy = state._unit._simpleTarget;

            float distance = (state._unit._currentPosition - state._unit._simpleTarget.position).magnitude;
            if(distance < state._unit._fieldOfView)
            {
                enemy.setHealPoint(enemy.getHealPoint() - 2.0f);
                enemy.playDamageSound();
            }
        }
    }
}
