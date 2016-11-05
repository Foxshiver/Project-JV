﻿using UnityEngine;
using System.Collections;

public class NPCUnit : Unit {

    private Unit general;

    // State
    protected Vector2 computePosition(State state)
    {
        switch(_stateUnit)
        {
            case Unit.State.Pursuit:
                return usePursuitBehavior();
            case Unit.State.Wait:
                return useWaitBehavior();
            case Unit.State.Evade:
                return useEvasionBehavior();
            case Unit.State.Fight:
                return useFightBehavior();

            //case Unit.State.HoldPosition:
            //    break;

            default:
                return new Vector2(0.0f, 0.0f);
        }
    }

    // All the behaviors are implemented here
    private Vector2 useWaitBehavior()
    {
        Vector2 targetPosition = Vector3TOVector2(_simpleTarget.transform.position);
        Vector2 steering = ((WaitBehavior)_behaviors[5]).computeWaitSteering(targetPosition, 7.0f, 4.0f);
        return ((WaitBehavior)_behaviors[5]).computeNewPosition(steering - ((WaitBehavior)_behaviors[5]).computeSteeringSeparationForce());
    }

    private Vector2 usePursuitBehavior()
    {
        Vector2 steering = ((PursuitBehavior)_behaviors[2]).computePursuitSteering(_targetUnit._currentPosition, _targetUnit._velocity);
        return ((PursuitBehavior)_behaviors[2]).computeNewPosition(steering - ((PursuitBehavior)_behaviors[2]).computeSteeringSeparationForce());
    }

    private Vector2 useEvasionBehavior()
    {
        Vector2 steering = ((EvasionBehavior)_behaviors[3]).computeEvasionSteering(_targetUnit._currentPosition, _targetUnit._velocity);
        return ((EvasionBehavior)_behaviors[3]).computeNewPosition(steering - ((EvasionBehavior)_behaviors[3]).computeSteeringSeparationForce());
    }

    private Vector2 useFightBehavior()
    {
        float distance = (_targetUnit._currentPosition - this._currentPosition).magnitude;

        if(distance > this._fieldOfVision)
            return usePursuitBehavior();
        else
        {
            InvokeRepeating("fight", 0.0f, 1.0f);

            return this._currentPosition;
        }       
    }

    // Fight function
    private void fight()
    {
        Unit enemy = this._targetUnit;
        
        float healPointRemaining = enemy.getHealPoint() - this._damagePoint;
        enemy.setHealPoint(healPointRemaining);

        if(healPointRemaining <= 0.0f)
        {
            Debug.Log("Destroy " + enemy.getName());

            Destroy(enemy.gameObject);
            CancelInvoke("fight");

            GameObject positionToWait = new GameObject();
            positionToWait.name = "Position to wait";
            positionToWait.transform.position = new Vector3(this._currentPosition[0], 0.0f, this._currentPosition[1]);

            this._simpleTarget = positionToWait;
            this._stateUnit = Unit.State.Wait;
        }
    }

    // Setter and Getter
    public Unit getGeneral()
    { return general; }
    
    public void setGeneral(Unit newGeneral)
    { general = newGeneral; }
}
