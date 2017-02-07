using UnityEngine;
using System.Collections;

public class AttackUnitState : IEnemyState
{
    private readonly WavePattern state;
    private PursuitBehavior pursuit;

    private double timeFirstCall = Time.time;

    public AttackUnitState(WavePattern statePatternEnemy, PursuitBehavior pursuitBehavior)
    {
        state = statePatternEnemy;
        pursuit = pursuitBehavior;
    }

    public void UpdateState()
    {
        checkAround();
        actionFight();
        Pursuit();
    }

    public void ToReachTargetState()
    {
        state.currentState = state.reachTargetState;
        state._unit._animatorEntity.SetBool("IsWorking", false);
        state._unit._animatorEntity.SetBool("IsAttacking", false);
    }

    public void ToAttackUnitState()
    { Debug.Log("Can't transition to same state"); }

    public void ToAttackTargetState()
    { Debug.Log("Can't transition to attack target state from attack state"); }

    /*
     * Pursuit behavior
     * 
     * Allow to hunt enemy
     */
    private void Pursuit()
    {
        if(state._unit._unitTarget == null)
            return;

        Vector2 steering = pursuit.computePursuitSteering(state._unit._unitTarget._currentPosition, state._unit._unitTarget._velocity);
        state._unit._currentPosition = pursuit.computeNewPosition(steering - pursuit.computeSteeringSeparationForce());

        state._unit.updatePosition(state._unit._currentPosition);
    }
    
    private void checkAround()
    {
        float distance = (state._unit._currentPosition - state._unit._unitTarget._currentPosition).magnitude;

        if (distance > state._unit._fieldOfView)
        {
            ToReachTargetState();
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
            ToReachTargetState();
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
                ToReachTargetState();
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
