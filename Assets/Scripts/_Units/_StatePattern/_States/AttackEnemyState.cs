using UnityEngine;
using System.Collections;

public class AttackEnemyState : IUnitState {

    private readonly StatePatternUnit state;
    private PursuitBehavior pursuit;

    private double timeFirstCall = Time.time;

    public AttackEnemyState(StatePatternUnit statePatternUnit, PursuitBehavior pursuitBehavior)
    {
        state = statePatternUnit;
        pursuit = pursuitBehavior;
    }

    public void UpdateState()
    {
        checkAround();
        actionFight();
    }

    public void TriggeringUpdate()
    {
        // Si l'ennemie chassé est détruit ou trop éloigné du joueur Alors l'unité retourne vers le joueur
        ToFollowLeaderState();
    }

    public void ToWaitState()
    { Debug.Log("Can't return to wait state"); }

    public void ToFollowLeaderState()
    { state.currentState = state.followLeaderState; }

    public void ToHoldPositionState()
    { Debug.Log("Can't transition to hold position state from attack state"); }

    public void ToDefendPositionState()
    { Debug.Log("Can't transition to defend state from attack state"); }

    public void ToAttackEnemyState()
    { Debug.Log("Can't transition to same state"); }

    public void ToWorkState()
    { Debug.Log("Can't transition to work state from attack state"); }

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

    private void checkAround()
    {
        float distance = (state._NPCUnit._unitTarget._currentPosition - state._NPCUnit._currentPosition).magnitude;

        if (distance > state._NPCUnit._unitTarget._fieldOfView)
            ToFollowLeaderState(); 
    }

    private void actionFight()
    {
        if ((Time.time - timeFirstCall) >= 1.0f)
        {
            timeFirstCall = Time.time;
            fight();
        }

        Pursuit();
    }

    // Fight function
    private void fight()
    {
        if (state._NPCUnit._unitTarget == null) // If enemy is already dead
        {
            state._NPCUnit._unitTarget = state._NPCUnit.general;

            ToFollowLeaderState();
        }
        else
        {
            Unit enemy = state._NPCUnit._unitTarget;

            float healPointRemaining = enemy.getHealPoint() - state._NPCUnit._damagePoint;
            Debug.Log("HIT : " + healPointRemaining);
            enemy.setHealPoint(healPointRemaining);

            if (healPointRemaining <= 0.0f)
            {
                state._NPCUnit._unitTarget = state._NPCUnit.general;
                ToFollowLeaderState();
            }
        }
    }
}
