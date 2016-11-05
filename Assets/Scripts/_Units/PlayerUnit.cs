﻿using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class PlayerUnit : Unit {

    private float radius = 8.0f;
    private List<Unit> listOfUnits;
    private List<List<Unit>> listOfHoldPositionUnits;

    // Constructor
    public PlayerUnit()
	{
		_money = 100;

        _name = "Player";
        listOfUnits = new List<Unit> { };
        listOfHoldPositionUnits = new List<List<Unit>> { };

        Debug.Log("PlayerUnit constructor called");

	}

	// Update is called once per frame
	void Update()
	{
        // Update behind point et current player position 
        _currentPosition = Vector3TOVector2(this.transform.position);
        _behindPosition = ((LeaderBehavior)_behaviors[4]).getBehindLeader();
        _currentPosition = ((LeaderBehavior)_behaviors[4]).computeNewPosition( ((LeaderBehavior)_behaviors [4]).controllerMovement() );
        updatePosition(_currentPosition);

        // Take unit on if player push 'space' button and unit is in radius
        if(Input.GetButtonDown("TakeUnitOn"))
        {
            Unit[] ListOfNeighboor = ListOfNeighboors();
            if(ListOfNeighboor.Length != 0)
            {
                ListOfNeighboor[0]._targetUnit = this;
                ListOfNeighboor[0]._stateUnit = Unit.State.Pursuit;

                listOfUnits.Add(ListOfNeighboor[0]);
            }
        }

        // Unit hold position if player push 'c' button
        if(Input.GetButtonDown("HoldPosition"))
        {
            int indiceUnitsList = 0;
            int indiceHoldPositionUnitsList = listOfHoldPositionUnits.Count;

            GameObject positionToHold = new GameObject();
            positionToHold.name = "Position to hold n°" + indiceHoldPositionUnitsList;
            positionToHold.transform.position = new Vector3(this._currentPosition[0], 0.0f, this._currentPosition[1]);

            List<Unit> listTampon = new List<Unit> { };

            for(int i=0; i<listOfUnits.Count; i++)
            {
                listOfUnits[i]._simpleTarget = positionToHold;
                listOfUnits[i]._stateUnit = Unit.State.Wait;

                listTampon.Add(listOfUnits[i]);
                     
                indiceUnitsList++;    
            }

            listOfHoldPositionUnits.Add(listTampon);

            listOfUnits.Clear();
        }

        // Call units back if player push 'v' button
        if(Input.GetButtonDown("CallBack"))
        {
            for(int i=0; i<listOfHoldPositionUnits.Count; i++)
            {
                for (int j = 0; j < listOfHoldPositionUnits[i].Count; j++)
                {
                    listOfHoldPositionUnits[i][j]._stateUnit = Unit.State.Pursuit;

                    listOfUnits.Add(listOfHoldPositionUnits[i][j]);
                }
            }

            listOfHoldPositionUnits.Clear();
        }
    }

    public Unit[] ListOfNeighboors()
    {
        Unit[] listOfUnit = GameObject.FindObjectsOfType<Unit>();
        bool[] isInRadius = new bool[listOfUnit.Length];

        int nbNeighboors = 0;

        for (int i = 0; i < listOfUnit.Length; i++)
        {
            if (listOfUnit[i].gameObject != this.gameObject)
            {
                float distance = (listOfUnit[i].gameObject.transform.position - this.transform.position).magnitude;

                if (distance < this.radius)
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
            if(isInRadius[i] && listOfUnit[i]._stateUnit == Unit.State.Wait)
            {
                listOfNeighboors.SetValue(listOfUnit[i], indiceNewList);
                indiceNewList++;
            }
        }

        return listOfNeighboors;
    }
}