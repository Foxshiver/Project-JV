using UnityEngine;
using System.Collections;

public class AttackEnemyState : IUnitState {

    private readonly RecruitmentPattern state;
    private PursuitBehavior pursuit;

    private double timeFirstCall = Time.time;

    private Animator animator;

    public AttackEnemyState(RecruitmentPattern statePatternUnit, PursuitBehavior pursuitBehavior)
    {
        //animator = state._unit.GetComponent<Animator>();
        state = statePatternUnit;
        pursuit = pursuitBehavior;
    }

    public void UpdateState()
    {
        checkAround();
        actionFight();
        Pursuit();
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
    { Debug.Log("Can't transition to defend state from attack state"); }

    public void ToAttackEnemyState()
    { Debug.Log("Can't transition to same state"); }

    public void ToWorkState()
    {
        state.currentState = state.workState;
        state._unit._animatorEntity.SetBool("IsAttacking", false);
        state._unit._animatorEntity.SetBool("IsWorking", true);
    }

    /*
     * Pursuit behavior
     * 
     * Allow to hunt enemy
     */
    private void Pursuit()
    {
        Vector2 steering = pursuit.computePursuitSteering(state._unit._unitTarget._currentPosition, state._unit._unitTarget._velocity);
        state._unit._currentPosition = pursuit.computeNewPosition(steering - pursuit.computeSteeringSeparationForce());

        state._unit.updatePosition(state._unit._currentPosition);
    }

    private void checkAround()
    {
        float distance = (state._unit._unitTarget._currentPosition - state._unit.general._currentPosition).magnitude;

        if (distance > state._unit.general._fieldOfView)
        {
            state._unit._unitTarget = state._unit.general;
            ToFollowLeaderState();
        }
    }

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

        if (state._unit._unitTarget == null) // If enemy is already dead
        {
            state._unit._unitTarget = state._unit.general;
            ToFollowLeaderState();
        }
        else
        {
            Unit enemy = (Unit)state._unit._unitTarget;

            float distance = (state._unit._currentPosition - state._unit._unitTarget._currentPosition).magnitude;
            if (distance < state._unit._fieldOfView)
            {
                float healPointRemaining = enemy.getHealPoint() - getDamagePoint(state._unit.getName(), enemy.getName());
                enemy.setHealPoint(healPointRemaining);
            }

            if (enemy.getHealPoint() <= 0.0f)
            {
                state._unit._unitTarget = state._unit.general;
                ToFollowLeaderState();
            }
        }
    }

    private float getDamagePoint(string unitName, string enemyName)
    {
        if (unitName == "Fox")
        {
            switch (enemyName)
            {
                case "Fox":
                    return 2.0f;
                case "Chicken":
                    return 5.0f;
                case "Snake":
                    return 1.0f;
                default:
                    Debug.Log("ERROR DAMAGE - WRONG NAME");
                    return 0.0f;
            }
        }
        else if (unitName == "Chicken")
        {
            switch (enemyName)
            {
                case "Fox":
                    return 1.0f;
                case "Chicken":
                    return 2.0f;
                case "Snake":
                    return 5.0f;
                default:
                    Debug.Log("ERROR DAMAGE - WRONG NAME");
                    return 0.0f;
            }
        }
        else if (unitName == "Snake")
        {
            switch (enemyName)
            {
                case "Fox":
                    return 5.0f;
                case "Chicken":
                    return 1.0f;
                case "Snake":
                    return 2.0f;
                default:
                    Debug.Log("ERROR DAMAGE - WRONG NAME");
                    return 0.0f;
            }
        }
        else
            return 0.0f;
    }
}
