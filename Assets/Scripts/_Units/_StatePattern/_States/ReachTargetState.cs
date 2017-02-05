using UnityEngine;
using System.Collections;

public class ReachTargetState : IEnemyState
{
    private readonly WavePattern state;
    private SeekBehavior seek;

    // Constructor
    public ReachTargetState(WavePattern statePatternEnemy, SeekBehavior seekBehavior)
    {
        state = statePatternEnemy;
        seek = seekBehavior;
    }

    public void UpdateState()
    {
        CheckEnemyAround();
        CheckTargetAround();
        Seek();
    }

    public void ToReachTargetState()
    { Debug.Log("Can't transition to same state"); }

    public void ToAttackUnitState()
    { state.currentState = state.attackUnitState; }

    public void ToAttackTargetState()
    { state.currentState = state.attackTargetState; }
    
    /*
     * Seek behavior
     * 
     * Allow to follow the leader
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

    private void CheckTargetAround()
    {
        float distance = (state._unit._currentPosition - state._unit._simpleTarget.position).magnitude;

        if(distance < state._unit._fieldOfView)
        {
            ToAttackTargetState();
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

