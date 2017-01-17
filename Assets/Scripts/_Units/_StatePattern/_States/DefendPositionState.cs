using UnityEngine;
using System.Collections;

public class DefendPositionState : IUnitState {

    private readonly StatePatternUnit state;
    private PursuitBehavior pursuit;

    private double timeFirstCall = Time.time;

    public DefendPositionState(StatePatternUnit statePatternUnit, PursuitBehavior pursuitBehavior)
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
        if (Input.GetButtonDown("CallBack"))
            ToFollowLeaderState();
    }

    public void ToWaitState()
    { Debug.Log("Can't return to wait state"); }

    public void ToFollowLeaderState()
    { state.currentState = state.followLeaderState; }

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

    private void checkDistanceBase()
    {
        float distance = (state._NPCUnit._currentPosition - state._NPCUnit._simpleTarget.position).magnitude;

        if (distance > state._NPCUnit._simpleTarget.defendingArea)
        {
            state._NPCUnit._unitTarget = state._NPCUnit.general;
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
        if (state._NPCUnit._unitTarget == null) // If enemy is already dead
        {
            state._NPCUnit._unitTarget = state._NPCUnit.general;
            ToHoldPositionState();
        }
        else
        {
            Unit enemy = state._NPCUnit._unitTarget;

            float distance = (state._NPCUnit._currentPosition - state._NPCUnit._unitTarget._currentPosition).magnitude;
            if (distance < state._NPCUnit._fieldOfView)
            {
                float healPointRemaining = enemy.getHealPoint() - getDamagePoint(state._NPCUnit.getName(), enemy.getName());
                Debug.Log("ENEMY HP : " + enemy.getHealPoint());
                enemy.setHealPoint(healPointRemaining);
            }

            if (enemy.getHealPoint() <= 0.0f)
            {
                state._NPCUnit._unitTarget = state._NPCUnit.general;
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
