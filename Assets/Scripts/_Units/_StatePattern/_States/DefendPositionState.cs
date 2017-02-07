using UnityEngine;
using System.Collections;

public class DefendPositionState : IUnitState {

    private readonly RecruitmentPattern state;
    private PursuitBehavior pursuit;

    private double timeFirstCall = Time.time;

    public DefendPositionState(RecruitmentPattern statePatternUnit, PursuitBehavior pursuitBehavior)
    {
        state = statePatternUnit;
        pursuit = pursuitBehavior;
    }

    public void UpdateState()
    {
        Pursuit();
        checkDistanceBase();
        actionFight();
    }

    public void TriggeringUpdate()
    {
        // Si l'ennemie chassé est détruit ou trop éloigné du spawner Alors l'unité retourne au point à défendre
        // - Si le joueur appuie sur 'X' Alors l'unité repasse en état de poursuite du joueur
        if (Input.GetButtonDown("CallBack_" + state._unit.general._joystickNumber.ToString()))
            ToFollowLeaderState();
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
    { Debug.Log("Can't transition to same state"); }

    public void ToAttackEnemyState()
    { Debug.Log("Can't transition to atack enemy state from defend state"); }

    public void ToAttackTargetState()
    { Debug.Log("Can't transition to attack target state from defend state"); }

    public void ToWorkState()
    { Debug.Log("Can't transition to work state from defend state"); }

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

    private void checkDistanceBase()
    {
        float distance = (state._unit._currentPosition - state._unit._simpleTarget.position).magnitude;

        if (distance > state._unit._simpleTarget.defendingArea)
        {
            state._unit._unitTarget = state._unit.general;
            ToHoldPositionState();
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
            ToHoldPositionState();
        }
        else
        {
            if(state._unit._unitTarget == state._unit.general)
                return;
            
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
                ToHoldPositionState();
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
