﻿using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class PlayerUnit : Unit {
    
    public List<NPCUnit> listOfUnits;
    private List< List<NPCUnit> > listOfHoldPositionUnits;
    private List<Buildings> listOfPositions;

    // Constructor
    public PlayerUnit()
	{
		_money = 100;

        _name = "Player";
        _faction = 1;
        _fieldOfVision = 8.0f;
        _healPoint = 50.0f;

        listOfUnits = new List<NPCUnit> { };
        listOfHoldPositionUnits = new List<List<NPCUnit>> { };
        listOfPositions = new List<Buildings> { };

        Debug.Log("PlayerUnit constructor called");
	}
		

	// Update is called once per frame
	void Update()
	{
        // Update behind point and current player position 
        _currentPosition = Vector3TOVector2(this.transform.position);
        _behindPosition = ((LeaderBehavior)_behaviors[5]).getBehindLeader();
        _currentPosition = ((LeaderBehavior)_behaviors[5]).computeNewPosition( ((LeaderBehavior)_behaviors [5]).controllerMovement() );
        updatePosition(_currentPosition);

        // Seeking all the unit around the player
        Unit[] listOfNeighboor = ListOfNeighboors();

		ChangeMaterialColorHighlight[] hList = GameObject.FindObjectsOfType<ChangeMaterialColorHighlight>();

		foreach (ChangeMaterialColorHighlight h in hList) // Allow to highlight in blue all the neighboors of the player
		{
			if (h != null) {
				//if (isInNeighboors (listOfNeighboor, h.gameObject)) 
				if (isInUnits (h.gameObject)) { 
					//h.HighlightColor = Color.blue;
					h.Highlight (true);
				} else {
					//h.HighlightColor = Color.red;
					h.Highlight (false);
				}
			}
		}

		// Take unit on if player push 'space' button and unit is in radius (Or 'A' button on 360 controler)
        if(Input.GetButtonDown("TakeUnitOn"))
        {
            if(listOfNeighboor.Length != 0)
            {
				NPCUnit nearestUnit = (NPCUnit)getNearestUnit(listOfNeighboor);

				if (nearestUnit.getFaction() == 0) {
					listOfUnits.Add (nearestUnit);

					int newUnitIndex = listOfUnits.LastIndexOf((NPCUnit)getNearestUnit(listOfNeighboor));

                    listOfUnits[newUnitIndex].getSimpleTarget()._nbCurrentUnit--;
                    listOfUnits[newUnitIndex].setGeneral(this);
					listOfUnits[newUnitIndex].setState(Unit.State.Seek);

					_money--;
				}
            }
        }

		// Unit hold position if player push 'c' button (Or 'B' button on 360 controler)
        if(Input.GetButtonDown("HoldPosition"))
        {
            int indiceHoldPositionUnitsList = listOfHoldPositionUnits.Count;

            Buildings positionToHold = new Buildings();
            positionToHold.name = "Position to hold n°" + indiceHoldPositionUnitsList;
            positionToHold.position = new Vector2(this._currentPosition[0], this._currentPosition[1]);

            listOfPositions.Add(positionToHold);
            int newPositionIndex = listOfPositions.LastIndexOf(positionToHold);

            List<NPCUnit> listTampon = new List<NPCUnit> { };

            for(int i=0; i<listOfUnits.Count; i++)
            {              
                listOfUnits[i].setSimpleTarget(listOfPositions[newPositionIndex]);
				listOfUnits[i].setNbHolders(listOfUnits.Count);
                listOfUnits[i].setState(Unit.State.Defend);

                listTampon.Add(listOfUnits[i]);  
            }

            listOfHoldPositionUnits.Add(listTampon);

            listOfUnits.Clear();
        }

		// Call units back if player push 'v' button (Or 'X' button on 360 controler)
        if(Input.GetButtonDown("CallBack"))
        {
            for(int i=0; i<listOfHoldPositionUnits.Count; i++)
            {
                for (int j = 0; j < listOfHoldPositionUnits[i].Count; j++)
                {
                    listOfHoldPositionUnits[i][j].setState(Unit.State.Seek);
                    listOfUnits.Add(listOfHoldPositionUnits[i][j]);
                }

                Destroy(listOfPositions[0]);
                listOfPositions.RemoveAt(0);
            }

            listOfHoldPositionUnits.Clear();
        }

        // Unit works if player push 'n' button (Or '?' button on 360 controler)
        if (Input.GetButtonDown("Work"))
        {
            
        }

        //        // Engage units in combat
        //        if(listOfNeighboor.Length != 0)
        //        {
        //            for(int i=0; i<listOfNeighboor.Length; i++)
        //            {
        //                if(listOfNeighboor[i].getFaction() == 2)
        //                {
        //                    for(int j=0; j<listOfUnits.Count; j++)
        //                    {
        //                        listOfUnits[j]._targetUnit  = listOfNeighboor[i];
        //                        listOfUnits[j]._stateUnit   = Unit.State.Fight;
        //                    }
        //                }
        //            }
        //        }

    }

    // Return the nearsest unit of the player
    public Unit getNearestUnit(Unit[] listOfNeighboor)
	{
		float minDistance = float.MaxValue;
		Unit nearestUnit = null;

		foreach (Unit u in listOfNeighboor)
		{
			float distance = (this._currentPosition - u._currentPosition).magnitude;
			if (distance < minDistance) {
				nearestUnit = u;
				minDistance = distance;
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

        for (int i = 0; i < listOfUnit.Length; i++)
        {
            if (listOfUnit[i].gameObject != this.gameObject)
            {
                float distance = (listOfUnit[i].gameObject.transform.position - this.transform.position).magnitude;

                if (distance < this._fieldOfVision && listOfUnit[i].getState() == Unit.State.Wait)
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

    // Return true if the Gameobject h is in the tab listOfNeighboors
    private bool isInNeighboors(Unit[] listOfNeighboors, GameObject h)
	{
		foreach (Unit u in listOfNeighboors)
		{
			if (h.gameObject == u.gameObject)
			{
				return true;
			}
		}
		return false;
	}

    // Return true if the Gameobject h is in the list listOfUnits
    private bool isInUnits(GameObject h)
	{
		foreach (Unit u in listOfUnits)
		{
			if (h.gameObject == u.gameObject)
			{
				return true;
			}
		}
		return false;
	}

    // Call when player destroyed enemy QG
    public void win()
    {
        Debug.Log("VICTORY !");
    }

    // Call when player QG is destroyed
    public void lose()
    {
        Debug.Log("DEFEAT !");
    }
}