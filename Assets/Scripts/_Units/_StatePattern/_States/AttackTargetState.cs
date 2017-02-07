using UnityEngine;
using System.Collections;

public class AttackTargetState : IEnemyState
{
    private readonly WavePattern state;
    private SeekBehavior seek;

    private double timeFirstCall = Time.time;

    private Animator animator;

    public AttackTargetState(WavePattern statePatternEnemy, SeekBehavior seekBehavior)
    {
        state = statePatternEnemy;
        seek = seekBehavior;
    }

    public void UpdateState()
    {
        CheckEnemyAround();
        actionFight();
        Seek();
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
    { Debug.Log("Can't transition to Attack target state from attack state"); }

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

    private void CheckEnemyAround()
    {
        Unit[] listOfNeighboor = ListOfNeighboors();
        Unit nearsestUnit = getNearestUnit(listOfNeighboor);

        if(nearsestUnit == null)
            return;

        state._unit._unitTarget = nearsestUnit;
        ToAttackUnitState();
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
        if (state._unit._simpleTarget != null)
        {
            FixedEntity enemy = state._unit._simpleTarget;

            float distance = (state._unit._currentPosition - state._unit._simpleTarget.position).magnitude;
            if (distance < state._unit._fieldOfView)
            {
                enemy.setHealPoint(enemy.getHealPoint()-2.0f);
                enemy.playDamageSound();
            }
        }
    }

    // Return the nearsest unit of the player
    public Unit getNearestUnit(Unit[] listOfNeighboor)
    {
        float minDistance = float.MaxValue;
        Unit nearestUnit = null;

        foreach(Unit u in listOfNeighboor)
        {
            if(u.getFaction() == 1)
            {
                float distance = (state._unit._currentPosition - u._currentPosition).magnitude;
                if(distance < minDistance)
                {
                    nearestUnit = u;
                    minDistance = distance;
                }
            }
        }

        return nearestUnit;
    }

    // Return the tab containing all the neighboors of the player
    public Unit[] ListOfNeighboors()
    {
        Unit[] listOfUnit = GameObject.FindObjectsOfType<Unit>();
        bool[] isInRadius = new bool[listOfUnit.Length];

        int nbNeighboors = 0;

        for(int i = 0; i < listOfUnit.Length; i++)
        {
            if(listOfUnit[i].gameObject != state._unit.gameObject)
            {
                float distance = (listOfUnit[i].gameObject.transform.position - state._unit.transform.position).magnitude;

                if(distance < state._unit._fieldOfView)
                {
                    isInRadius[i] = true;
                    nbNeighboors++;
                }
                else
                {
                    isInRadius[i] = false;
                }
            }
        }

        int indiceNewList = 0;

        Unit[] listOfNeighboors = new Unit[nbNeighboors];
        for(int i = 0; i < listOfUnit.Length; i++)
        {
            if(isInRadius[i])
            {
                listOfNeighboors.SetValue(listOfUnit[i], indiceNewList);
                indiceNewList++;
            }
        }

        return listOfNeighboors;
    }
}
