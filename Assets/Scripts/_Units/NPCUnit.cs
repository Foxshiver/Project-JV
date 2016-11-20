using UnityEngine;
using System.Collections;

public class NPCUnit : Unit {

    private Unit general;
    private State previousState;

	private bool isAttacking = false;
	private int nbHolders = 0;

    /*
     * Compute position of the unit according 
     * to his current state
     */
    protected Vector2 computePosition(State state)
    {
		switch(state)
        {
            case Unit.State.Seek:
                return useSeekBehavior();
            case Unit.State.Pursuit:
                return usePursuitBehavior();
            case Unit.State.Wait:
                return useWaitBehavior(7.0f, 4.0f);
            case Unit.State.Fight:
                return useFightBehavior();
            case Unit.State.Defend:
				return useDefendBehavior();
            case Unit.State.Work:
                return useWorkBehavior(7.0f, 4.0f);
            default:
                return new Vector2(0.0f, 0.0f);
        }
    }

    /////////////////////////////////////////
    // IMPLEMENTATION OF ALL THE BEHAVIORS //
    /////////////////////////////////////////

    /*
     * Wait behavior
     * 
     * Allow to initialize neutral unit position at the beggining
     */
    private Vector2 useWaitBehavior(float sizeRadius, float timeBeforeChangePos)
    {
        Vector2 steering = ((WaitBehavior)_behaviors[0]).computeWaitSteering(_simpleTarget.position, sizeRadius, timeBeforeChangePos);
        return ((WaitBehavior)_behaviors[0]).computeNewPosition(steering - ((WaitBehavior)_behaviors[0]).computeSteeringSeparationForce());
    }

    /*
     * Seek behavior
     * 
     * Allow to follow the leader
     */
    private Vector2 useSeekBehavior()
    {
        Vector2 steering = ((SeekBehavior)_behaviors[1]).computeSeekSteering(_unitTarget._currentPosition);
        return ((SeekBehavior)_behaviors[1]).computeNewPosition(steering - ((SeekBehavior)_behaviors[1]).computeSteeringSeparationForce());
    }

    /*
     * Pursuit behavior
     * 
     * Allow to hunt enemy
     */
    private Vector2 usePursuitBehavior()
    {
		// If dans le rayon, on change l'etat en fight
		// Sinon pursuit normal


        Vector2 steering = ((PursuitBehavior)_behaviors[3]).computePursuitSteering(_unitTarget._currentPosition, _unitTarget._velocity);
        return ((PursuitBehavior)_behaviors[3]).computeNewPosition(steering - ((PursuitBehavior)_behaviors[3]).computeSteeringSeparationForce());
    }

    /*
     * Defend behavior
     * 
     * Allow to hold position
     */
    private Vector2 useDefendBehavior()
	{
		Unit[] listOfNeighboors = ListOfNeighboors (5.0f);

		if((this._currentPosition - this._simpleTarget.position).magnitude > nbHolders) // nbHolder A CHANGER
			return useWaitBehavior (nbHolders,4.0f);  // nbHolder A CHANGER

		foreach (Unit u in listOfNeighboors) {
			if ((u.getFaction () != this.getFaction ()) && (u.getFaction () != 0)) { // if there is an enemy
				this._unitTarget = u;
				this._stateUnit = State.Fight; // Si dans le voisinage on a des ennemis, on passe en state Fight ! 
				Debug.Log ("FIGHT !");
				return useFightBehavior ();
			}
		}

		Debug.Log ("NO FIGHT !");
			
		return useWaitBehavior (nbHolders,4.0f);  // nbHolder A CHANGER
	}

    /*
     * Fight behavior
     * 
     * Allow to attack close enemy
     */
    private Vector2 useFightBehavior()
    {
        //float distance = (_targetUnit._currentPosition - this._currentPosition).magnitude;

        //if (distance > this._fieldOfVision)
        //	return usePursuitBehavior ();    // Remettre, en changeant l'etat en Pursuit OU Defend
		
		if (!isAttacking)
			InvokeRepeating ("fight", 0.0f, 1.0f);

		return usePursuitBehavior();
    }


    /*
     * Work behavior
     * 
     * Allow to earn money by affecting unit to work
     */
    private Vector2 useWorkBehavior(float sizeRadius, float timeBeforeChangePos)
    {
        Vector2 steering = ((WaitBehavior)_behaviors[0]).computeWaitSteering(_simpleTarget.position, sizeRadius, timeBeforeChangePos);
        return ((WaitBehavior)_behaviors[0]).computeNewPosition(steering - ((WaitBehavior)_behaviors[0]).computeSteeringSeparationForce());
    }

    ///////////////////////
    // HELPFUL FUNCTIONS //
    ///////////////////////

    // Fight function
    private void fight()
    {
		isAttacking = true;

		if (this._unitTarget == null) // If enemy is already dead
		{
			this._unitTarget = this.general;

			CancelInvoke("fight");
			isAttacking = false;
		}
		else
		{
			Unit enemy = this._unitTarget;

			float healPointRemaining = enemy.getHealPoint() - this._damagePoint;
			Debug.Log("HIT : " + healPointRemaining);
			enemy.setHealPoint(healPointRemaining);

			if(healPointRemaining <= 0.0f)
			{

				CancelInvoke("fight");
				isAttacking = false;
				Destroy(enemy.gameObject);

				Debug.Log ("TARGET : " + this._unitTarget);
				this._unitTarget = this.general;

			}
		}
    }

	public Unit[] ListOfNeighboors(float radius) // Return the tab containing all the neighboors of the player
	{
		Unit[] listOfUnit = GameObject.FindObjectsOfType<Unit>();
		bool[] isInRadius = new bool[listOfUnit.Length];

		int nbNeighboors = 0;

		for(int i = 0; i < listOfUnit.Length; i++)
		{
			if (listOfUnit[i].gameObject != this.gameObject)
			{
				float distance = (listOfUnit[i]._currentPosition - this._simpleTarget.position).magnitude;

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

    /////////////////////
    // SETTER - GETTER //
    /////////////////////

    public Unit getGeneral()
    { return general; }
    public void setGeneral(Unit newGeneral)
    {
        general = newGeneral;
        setUnitTarget(general);
        setFaction(general.getFaction());
    }

	public int getNbHolders()
	{ return nbHolders; }
	public void setNbHolders(int newNbHolders)
	{ nbHolders = newNbHolders; }
}
