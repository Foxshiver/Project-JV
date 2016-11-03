using UnityEngine;
using System.Collections;

public class PlayerUnit : Unit {

    private float radius = 3.0f;

    // Constructor
    public PlayerUnit()
	{
        _name = "Player";
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
            Unit[] listOfNeighboors = ListOfNeighboors();
            if(listOfNeighboors.Length != 0)
            {
                listOfNeighboors[0]._targetUnit = this;
                listOfNeighboors[0]._stateUnit = Unit.State.Pursuit;
            }
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
        for (int i = 0; i < listOfUnit.Length; i++)
        {
            if (isInRadius[i])
            {
                listOfNeighboors.SetValue(listOfUnit[i], indiceNewList);
                indiceNewList++;
            }
        }

        return listOfNeighboors;
    }
}
