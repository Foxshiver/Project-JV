using UnityEngine;
using System.Collections;

public class NPCUnit : Unit {

    private Unit general;
	private bool isAttacking = false;

	private int nbHolders = 0;

    // State
    protected Vector2 computePosition(State state)
    {
		switch(state)
        {
            case Unit.State.Pursuit:
                return usePursuitBehavior();
            case Unit.State.Wait:
                return useWaitBehavior(7.0f,4.0f);
            case Unit.State.Evade:
                return useEvasionBehavior();
			case Unit.State.Defend:
				return useDefendBehavior();
            case Unit.State.Fight:
                return useFightBehavior();
            default:
                return new Vector2(0.0f, 0.0f);
        }
    }

    // All the behaviors are implemented here
	private Vector2 useWaitBehavior(float sizeRadius, float timeBeforeChangePos)
    {
        Vector2 targetPosition = Vector3TOVector2(_simpleTarget.transform.position);
		Vector2 steering = ((WaitBehavior)_behaviors[5]).computeWaitSteering(targetPosition, sizeRadius, timeBeforeChangePos);
        return ((WaitBehavior)_behaviors[5]).computeNewPosition(steering - ((WaitBehavior)_behaviors[5]).computeSteeringSeparationForce());
    }

    private Vector2 usePursuitBehavior()
    {
		// If dans le rayon, on change l'etat en fight
		// Sinon pursuit normal


        Vector2 steering = ((PursuitBehavior)_behaviors[2]).computePursuitSteering(_targetUnit._currentPosition, _targetUnit._velocity);
        return ((PursuitBehavior)_behaviors[2]).computeNewPosition(steering - ((PursuitBehavior)_behaviors[2]).computeSteeringSeparationForce());
    }

    private Vector2 useEvasionBehavior()
    {
        Vector2 steering = ((EvasionBehavior)_behaviors[3]).computeEvasionSteering(_targetUnit._currentPosition, _targetUnit._velocity);
        return ((EvasionBehavior)_behaviors[3]).computeNewPosition(steering - ((EvasionBehavior)_behaviors[3]).computeSteeringSeparationForce());
    }

	private Vector2 useDefendBehavior()
	{
		Unit[] listOfNeighboors = ListOfNeighboors (5.0f);

		if((this._currentPosition - Vector3TOVector2(this._simpleTarget.transform.position)).magnitude > nbHolders) // nbHolder A CHANGER
			return useWaitBehavior (nbHolders,4.0f);  // nbHolder A CHANGER

		foreach (Unit u in listOfNeighboors) {
			if ((u.getFaction () != this.getFaction ()) && (u.getFaction () != 0)) { // if there is an enemy
				this._targetUnit = u;
				this._stateUnit = State.Fight; // Si dans le voisinage on a des ennemis, on passe en state Fight ! 
				Debug.Log ("FIGHT !");
				return useFightBehavior ();
			}
		}

		Debug.Log ("NO FIGHT !");
			
		return useWaitBehavior (nbHolders,4.0f);  // nbHolder A CHANGER
	}

    private Vector2 useFightBehavior()
    {
        //float distance = (_targetUnit._currentPosition - this._currentPosition).magnitude;

//		if (distance > this._fieldOfVision)
//			return usePursuitBehavior ();    // Remettre, en changeant l'etat en Pursuit OU Defend
		
		if (!isAttacking)
			InvokeRepeating ("fight", 0.0f, 1.0f);

		return usePursuitBehavior();
    }

    // Fight function
    private void fight()
    {
		isAttacking = true;

		if (this._targetUnit == null) // If enemy is already dead
		{
			this._targetUnit = this.general;

			CancelInvoke("fight");
			isAttacking = false;
		}
		else
		{
			Unit enemy = this._targetUnit;

			float healPointRemaining = enemy.getHealPoint() - this._damagePoint;
			Debug.Log("HIT : " + healPointRemaining);
			enemy.setHealPoint(healPointRemaining);

			if(healPointRemaining <= 0.0f)
			{

				CancelInvoke("fight");
				isAttacking = false;
				Destroy(enemy.gameObject);

				Debug.Log ("TARGET : " + this._targetUnit);
				this._targetUnit = this.general;

			}
		}
    }

	public Unit[] ListOfNeighboors(float radius) // Return the tab containing all the neighboors of the player
	{
		Unit[] listOfUnit = GameObject.FindObjectsOfType<Unit>();
		bool[] isInRadius = new bool[listOfUnit.Length];

		int nbNeighboors = 0;

		for (int i = 0; i < listOfUnit.Length; i++)
		{
			if (listOfUnit[i].gameObject != this.gameObject)
			{
				float distance = (listOfUnit[i].gameObject.transform.position - this._simpleTarget.transform.position).magnitude;

				if (distance < radius)
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

    // Setter and Getter
    public Unit getGeneral()
    { return general; }
    
    public void setGeneral(Unit newGeneral)
    { general = newGeneral; }

	public int getNbHolders()
	{
		return nbHolders;
	}
	public void setNbHolders(int newNbHolders)
	{
		nbHolders = newNbHolders;
	}
}
