using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Player : MovableEntity {
    
    public List<Unit> listOfUnits;
    public List<Unit> listOfWorkerUnits;
    public List<List<Unit>> listOfHoldPositionUnits;
    public List<FixedEntity> listOfPositions;

    private LeaderBehavior leader;

    public Vector2 _behindPosition;
    [HideInInspector] public Farm targetToDestroy;
    [HideInInspector] public bool nearToTarget;
    public int _money;
   
    // Constructor
    public Player()
	{
        listOfUnits = new List<Unit> { };
        listOfWorkerUnits = new List<Unit> { };
        listOfHoldPositionUnits = new List<List<Unit>> { };
        listOfPositions = new List<FixedEntity> { };

        leader = new LeaderBehavior(this);
	}

    public void init(Farm farm, int allyFaction, int enemyFaction)
    {
        this._faction = allyFaction;
        this._enemyFaction = enemyFaction;

        this._currentPosition = nearToFarm(farm);
        this.updatePosition(this._currentPosition);
    }

    public void init(Farm farm, Farm target, int allyFaction, int enemyFaction)
    {
        this.targetToDestroy = target;
        this._faction = allyFaction;
        this._enemyFaction = enemyFaction;

        this._currentPosition = nearToFarm(farm);
        this.updatePosition(this._currentPosition);
    }

	// Update is called once per frame
	public void update()
	{
        Vector2 prevPosition = this._currentPosition;

        // Update behind point and current player position 
        _currentPosition = Vector3TOVector2(this.transform.position);
        _behindPosition = leader.getBehindLeader();
        _currentPosition = leader.computeNewPosition(leader.controllerMovement());
        updatePosition(_currentPosition);

        _animatorEntity.SetFloat("Velocity", _velocity.magnitude);

        // Orientation du joueur
        //Vector2 vector = this._currentPosition - prevPosition;
        //float angle = AngleBetweenVector2(vector, new Vector2(0.0f, 0.0f));
        //this.transform.localEulerAngles = new Vector3(0.0f, -angle, 0.0f);

        // Seeking all the unit around the player
        List<Unit> listOfNeighboors = new List<Unit> { };
        List<Unit> listOfNeighboorEnemies = new List<Unit> { };
        ListOfNeighboors(listOfNeighboors, listOfNeighboorEnemies);
        Field[] listOfField = ListOfFields();

        //ChangeMaterialColorHighlight[] hList = GameObject.FindObjectsOfType<ChangeMaterialColorHighlight>();

        //foreach (ChangeMaterialColorHighlight h in hList) // Allow to highlight in blue all the neighboors of the player
        //{
        //	if (h != null) {
        //		//if (isInNeighboors (listOfNeighboor, h.gameObject)) 
        //		if (isInUnits (h.gameObject)) { 
        //			//h.HighlightColor = Color.blue;
        //			h.Highlight (true);
        //		} else {
        //			//h.HighlightColor = Color.red;
        //			h.Highlight (false);
        //		}
        //	}
        //}

        // Take unit on if player push 'space' button and unit is in radius (Or 'A' button on 360 controler)
        if(Input.GetButtonDown("TakeUnitOn"))
        {
            if(listOfNeighboors.Count != 0)
            {
                Unit nearestUnit = (Unit)getNearestUnit(listOfNeighboors, 0);

                if (_money >= 1 && nearestUnit != null)
                {
                    listOfUnits.Add(nearestUnit);

                    int newUnitIndex = listOfUnits.LastIndexOf(nearestUnit);
                    listOfUnits[newUnitIndex].getSimpleTarget()._nbCurrentUnit--;
                    listOfUnits[newUnitIndex]._unitTarget = this;
                    listOfUnits[newUnitIndex].general = this;
                    listOfUnits[newUnitIndex].setFaction(this._faction);
                    listOfUnits[newUnitIndex].triggeringUpdate();

                    _money--;
                }
            }
        }

        // Unit hold position if player push 'c' button (Or 'B' button on 360 controler)
        if(Input.GetButtonDown("HoldPosition"))
        {
            if(listOfUnits.Count == 0)
                return;

            int indiceHoldPositionUnitsList = listOfHoldPositionUnits.Count;

            PositionToHold positionToHold = new PositionToHold();
            positionToHold.init(this._faction);
            //positionToHold.name = "Position to hold n°" + indiceHoldPositionUnitsList;
            positionToHold.position = new Vector2(this._currentPosition[0], this._currentPosition[1]);

            listOfPositions.Add(positionToHold);
            int newPositionIndex = listOfPositions.LastIndexOf(positionToHold);

            List<Unit> listTampon = new List<Unit> { };

            for(int i = 0; i < listOfUnits.Count; i++)
            {
                listOfUnits[i].setSimpleTarget(listOfPositions[newPositionIndex]);
                listOfUnits[i].triggeringUpdate();

                listTampon.Add(listOfUnits[i]);
            }

            listOfHoldPositionUnits.Add(listTampon);

            listOfUnits.Clear();
        }

        // Call units back if player push 'v' button (Or 'X' button on 360 controler)
        if(Input.GetButtonDown("CallBack"))
        {
            for(int i = 0; i < listOfHoldPositionUnits.Count; i++)
            {
                for(int j = 0; j < listOfHoldPositionUnits[i].Count; j++)
                {
                    listOfHoldPositionUnits[i][j].triggeringUpdate();
                    listOfUnits.Add(listOfHoldPositionUnits[i][j]);
                }

                Destroy(listOfPositions[0]);
                listOfPositions.RemoveAt(0);
            }

            for(int i = 0; i < listOfWorkerUnits.Count; i++)
            {
                listOfWorkerUnits[i].getSimpleTarget()._nbCurrentUnit--;
                listOfWorkerUnits[i].triggeringUpdate();
                listOfUnits.Add(listOfWorkerUnits[i]);
            }

            listOfHoldPositionUnits.Clear();
            listOfWorkerUnits.Clear();
        }

        // Unit works if player push 'n' button (Or 'Y' button on 360 controler)
        if(Input.GetButtonDown("Work"))
        {
            if(listOfUnits.Count == 0 || listOfField.Length == 0)
                return;

            if(listOfField[0]._nbCurrentUnit == listOfField[0]._nbMaxUnit)
                return;

            float lessHPUnit = listOfUnits[0].getHealPoint();
            Unit weakestUnit = listOfUnits[0];

            for(int i = 1; i < listOfUnits.Count; i++)
            {
                float HPUnit = listOfUnits[i].getHealPoint();

                if(HPUnit < lessHPUnit)
                {
                    lessHPUnit = HPUnit;
                    weakestUnit = listOfUnits[i];
                }
            }

            listOfUnits.Remove(weakestUnit);
            listOfWorkerUnits.Add(weakestUnit);

            int newUnitIndex = listOfWorkerUnits.LastIndexOf(weakestUnit);

            listOfField[0]._nbCurrentUnit++;

            listOfWorkerUnits[newUnitIndex].setSimpleTarget(listOfField[0]);
            listOfWorkerUnits[newUnitIndex].triggeringUpdate();
        }

        // Engage units in combat
        if(listOfUnits.Count != 0)
        {
            if(listOfNeighboorEnemies.Count != 0)
            {
                Unit NearestUnit = getNearestUnit(listOfNeighboorEnemies, this._faction);

                if(NearestUnit.getFaction() == this._enemyFaction)
                {
                    for(int i=0; i<listOfUnits.Count; i++)
                    {
                        listOfUnits[i]._unitTarget = NearestUnit;
                        listOfUnits[i].triggeringUpdate();
                    }
                }
            }

            float distance = (this._currentPosition - this.targetToDestroy.position).magnitude;
            if(distance < this._fieldOfView)
            {
                nearToTarget = true;

                for(int i=0; i<listOfUnits.Count; i++)
                {
                    listOfUnits[i]._simpleTarget = this.targetToDestroy;
                    listOfUnits[i].triggeringUpdate();
                }
            }
            else
                nearToTarget = false;
        }

        foreach(PositionToHold p in listOfPositions)
            p.update();

        checkAllUnits();
    }

    // Return a position near to Vector2
    private Vector2 nearToFarm(Farm farm)
    {
        float x = Random.Range(farm.position.x - 2, farm.position.x + 2);
        float y = Random.Range(farm.position.y - 2, farm.position.y + 2);

        return new Vector2(x, y);
    }

    // Update all the unit list
    private void checkAllUnits()
    {
        for(int i=0; i<listOfUnits.Count; i++)
            if(listOfUnits[i] == null)
                listOfUnits.Remove(listOfUnits[i]);

        for(int i=0; i<listOfWorkerUnits.Count; i++)
            if(listOfWorkerUnits[i] == null)
            {
                listOfWorkerUnits[i]._simpleTarget._nbCurrentUnit--;
                listOfWorkerUnits.Remove(listOfWorkerUnits[i]);
            }

        for(int i=0; i<listOfHoldPositionUnits.Count; i++)
            for(int j=0; j<listOfHoldPositionUnits[i].Count; j++)
            {
                if(listOfHoldPositionUnits[i][j] == null)
                    listOfHoldPositionUnits[i].Remove(listOfHoldPositionUnits[i][j]);
            }
    }

    // Return the nearsest unit of the player
    public Unit getNearestUnit(List<Unit> listOfNeighboors, int faction)
	{
		float minDistance = float.MaxValue;
		Unit nearestUnit = null;

		foreach (Unit u in listOfNeighboors)
		{
            if(u.getFaction() == faction)
            {
                float distance = (this._currentPosition - u._currentPosition).magnitude;
                if (distance < minDistance)
                {
                    nearestUnit = u;
                    minDistance = distance;
                }
            }
		}

		return nearestUnit;
	}

    // Return the tab containing all the neighboors of the player
    public void ListOfNeighboors(List<Unit> listOfNeighboors, List<Unit> listOfNeighboorEnemies)
    {
        Unit[] listOfUnit = GameObject.FindObjectsOfType<Unit>();

        for (int i = 0; i < listOfUnit.Length; i++)
        {
            if (listOfUnit[i].gameObject != this.gameObject)
            {
                float distance = (listOfUnit[i].gameObject.transform.position - this.transform.position).magnitude;

                if (distance < this._fieldOfView)
                {
                    listOfNeighboors.Add(listOfUnit[i]);

                    if(listOfUnit[i]._faction == this._enemyFaction)
                        listOfNeighboorEnemies.Add(listOfUnit[i]);
                }
            }
        }
    }

    public Field[] ListOfFields()
    {
        Field[] listOfField = GameObject.FindObjectsOfType<Field>();
        bool[] isInRadius = new bool[listOfField.Length];

        int nbFields = 0;

        for (int i = 0; i < listOfField.Length; i++)
        {
            float distance = (listOfField[i].gameObject.transform.position - this.transform.position).magnitude;

            if (distance < this._fieldOfView)
            {
                isInRadius[i] = true;
                nbFields++;
            }
            else
            {
                isInRadius[i] = false;
            }
        }

        int indiceNewList = 0;

        Field[] listOfFields = new Field[nbFields];
        for (int i = 0; i < listOfField.Length; i++)
        {
            if (isInRadius[i])
            {
                listOfFields.SetValue(listOfField[i], indiceNewList);
                indiceNewList++;
            }
        }

        return listOfFields;
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
}