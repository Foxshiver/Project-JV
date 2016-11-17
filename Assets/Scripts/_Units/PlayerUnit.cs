using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class PlayerUnit : Unit {
    
    public List<NPCUnit> listOfUnits;
    private List< List<NPCUnit> > listOfHoldPositionUnits;
    private List<GameObject> listOfPositions;

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
        listOfPositions = new List<GameObject> { };

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

        // Seeking all the unit around the player
        Unit[] listOfNeighboor = ListOfNeighboors();

		Highlightable[] hList = GameObject.FindObjectsOfType<Highlightable>();

		foreach (Highlightable h in hList) // Allow to highlight in blue all the neighboors of the player
		{
			//if (isInUnits(h.gameObject)) // We have some problems with this, because of destruction when fight I guess
			if (isInNeighboors (listOfNeighboor, h.gameObject))
				h.Highlight (true);
			else
				h.Highlight (false);
		}

		// Take unit on if player push 'space' button and unit is in radius (Or 'A' button on 360 controler)
        if (Input.GetButtonDown("TakeUnitOn"))
        {
            if(listOfNeighboor.Length != 0)
            {
                listOfUnits.Add((NPCUnit)listOfNeighboor[0]);

                int newUnitIndex = listOfUnits.LastIndexOf((NPCUnit)listOfNeighboor[0]);

                listOfUnits[newUnitIndex].setGeneral(this);
                listOfUnits[newUnitIndex].setFaction(this._faction);
                listOfUnits[newUnitIndex]._targetUnit = this;
                listOfUnits[newUnitIndex]._stateUnit = Unit.State.Pursuit;
            }
        }

		// Unit hold position if player push 'c' button (Or 'B' button on 360 controler)
        if(Input.GetButtonDown("HoldPosition"))
        {
            int indiceUnitsList = 0;
            int indiceHoldPositionUnitsList = listOfHoldPositionUnits.Count;

            GameObject positionToHold = new GameObject();
            positionToHold.name = "Position to hold n°" + indiceHoldPositionUnitsList;
            positionToHold.transform.position = new Vector3(this._currentPosition[0], 0.0f, this._currentPosition[1]);

            listOfPositions.Add(positionToHold);
            int newPositionIndex = listOfPositions.LastIndexOf(positionToHold);

            List<NPCUnit> listTampon = new List<NPCUnit> { };

            for(int i=0; i<listOfUnits.Count; i++)
            {              
                listOfUnits[i]._simpleTarget = listOfPositions[newPositionIndex];
                listOfUnits[i]._stateUnit = Unit.State.Wait;

                listTampon.Add(listOfUnits[i]);
                     
                indiceUnitsList++;    
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
                    listOfHoldPositionUnits[i][j]._stateUnit = Unit.State.Pursuit;
                    listOfUnits.Add(listOfHoldPositionUnits[i][j]);
                }

                Destroy(listOfPositions[0].gameObject);
                listOfPositions.RemoveAt(0);
            }

            listOfHoldPositionUnits.Clear();
        }

        // Engage units in combat
        if(listOfNeighboor.Length != 0)
        {
            for(int i=0; i<listOfNeighboor.Length; i++)
            {
                if(listOfNeighboor[i].getFaction() == 2)
                {
                    for(int j=0; j<listOfUnits.Count; j++)
                    {
                        listOfUnits[i]._targetUnit  = listOfNeighboor[i];
                        listOfUnits[i]._stateUnit   = Unit.State.Fight;
                    }
                }
            }
        }

    }

    public Unit[] ListOfNeighboors() // Return the tab containing all the neighboors of the player
    {
        Unit[] listOfUnit = GameObject.FindObjectsOfType<Unit>();
        bool[] isInRadius = new bool[listOfUnit.Length];

        int nbNeighboors = 0;

        for (int i = 0; i < listOfUnit.Length; i++)
        {
            if (listOfUnit[i].gameObject != this.gameObject)
            {
                float distance = (listOfUnit[i].gameObject.transform.position - this.transform.position).magnitude;

                if (distance < this._fieldOfVision && listOfUnit[i]._stateUnit == Unit.State.Wait)
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

	private bool isInNeighboors(Unit[] listOfNeighboors, GameObject h) // Return true if the Gameobject h is in the tab listOfNeighboors
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

	private bool isInUnits(GameObject h) // Return true if the Gameobject h is in the list listOfUnits
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
}
