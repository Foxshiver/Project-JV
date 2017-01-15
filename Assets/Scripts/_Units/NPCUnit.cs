using UnityEngine;
using System.Collections;

public class NPCUnit : Unit {

    public Unit general;
    //private State previousState;

    //private bool isAttacking = false;
    private int nbHolders = 0;

    public StatePatternUnit statePattern;
    public string currentState;

    public void init(Spawner spawner, Vector2 pos)
    {
        this._simpleTarget = spawner;

        this._currentPosition = pos;
        this.updatePosition(this._currentPosition);

        sizeRadius = 2.0f;
        timeBeforeChangePos = Random.Range(3.0f, 6.0f);

        statePattern = new StatePatternUnit(this);
    }

    public void update()
    {
        statePattern.updateState();
        currentState = statePattern.currentState.ToString();
    }

    public void triggeringUpdate()
    {
        statePattern.triggeringUpdate();
    }

/*
 * Compute position of the unit according 
 * to his current state
 */
//    protected Vector2 computePosition(State state)
//    {
//        switch(state)
//        {
//            case Unit.State.Seek:
//                return useSeekBehavior();
//            case Unit.State.Pursuit:
//                return usePursuitBehavior();
//            case Unit.State.Wait:
//                return useWaitBehavior(5.0f, 4.0f);
//            case Unit.State.Fight:
//                return useFightBehavior();
//            case Unit.State.Defend:
//                return useDefendBehavior();
//            case Unit.State.Work:
//                return useWorkBehavior(2.0f, 1.5f);
//            default:
//                return new Vector2(0.0f, 0.0f);
//        }

//        return new Vector2(0.0f, 0.0f);
//    }

//    /////////////////////////////////////////
//    // IMPLEMENTATION OF ALL THE BEHAVIORS //
//    /////////////////////////////////////////

//    /*
//     * Wait behavior
//     * 
//     * Allow to initialize neutral unit position at the beggining
//     */
//    private Vector2 useWaitBehavior(float sizeRadius, float timeBeforeChangePos)
//    {
//        return ExtensionMethods.wait(this._behaviors, this._simpleTarget.position, sizeRadius, timeBeforeChangePos);
//    }

//    /*
//     * Seek behavior
//     * 
//     * Allow to follow the leader
//     */
//    private Vector2 useSeekBehavior()
//    {
//        return transform.seek(this._behaviors, this._unitTarget._currentPosition);
//    }

//    /*
//     * Pursuit behavior
//     * 
//     * Allow to hunt enemy
//     */
//    private Vector2 usePursuitBehavior()
//    {
//        // If dans le rayon, on change l'etat en fight
//        // Sinon pursuit normal

//        return transform.pursuit(this._behaviors, this._unitTarget._currentPosition, this._unitTarget._velocity);
//    }

//    /*
//     * Defend behavior
//     * 
//     * Allow to hold position
//     */
//    private Vector2 useDefendBehavior()
//	{
//		Unit[] listOfNeighboors = ListOfNeighboors (5.0f);

//		if((this._currentPosition - this._simpleTarget.position).magnitude > nbHolders) // nbHolder A CHANGER
//			return useWaitBehavior (nbHolders,4.0f);  // nbHolder A CHANGER

//		foreach (Unit u in listOfNeighboors) {
//			if ((u.getFaction () != this.getFaction ()) && (u.getFaction () != 0)) { // if there is an enemy
//				this._unitTarget = u;
//				this._stateUnit = State.Fight; // Si dans le voisinage on a des ennemis, on passe en state Fight ! 

//				return useFightBehavior ();
//			}
//		}

//		return ExtensionMethods.wait(this._behaviors, this._simpleTarget.position, nbHolders, 4.0f);   // nbHolder A CHANGER
//	}

//    /*
//     * Fight behavior
//     * 
//     * Allow to attack close enemy
//     */
//    private Vector2 useFightBehavior()
//    {
//        //float distance = (_targetUnit._currentPosition - this._currentPosition).magnitude;

//        //if (distance > this._fieldOfVision)
//        //	return usePursuitBehavior ();    // Remettre, en changeant l'etat en Pursuit OU Defend

//		if (!isAttacking)
//			InvokeRepeating ("fight", 0.0f, 1.0f);

//		return transform.pursuit(this._behaviors, this._unitTarget._currentPosition, this._unitTarget._velocity);
//    }

//    /*
//     * Work behavior
//     * 
//     * Allow to earn money by affecting unit to work
//     */
//    private Vector2 useWorkBehavior(float sizeRadius, float timeBeforeChangePos)
//    {
//        return ExtensionMethods.wait(this._behaviors, this._simpleTarget.position, sizeRadius, timeBeforeChangePos);
//    }

//    ///////////////////////
//    // HELPFUL FUNCTIONS //
//    ///////////////////////

//    // Fight function
//    private void fight()
//    {
//		isAttacking = true;

//		if (this._unitTarget == null) // If enemy is already dead
//		{
//			this._unitTarget = this.general;

//			CancelInvoke("fight");
//			isAttacking = false;
//		}
//		else
//		{
//			Unit enemy = this._unitTarget;

//			float healPointRemaining = enemy.getHealPoint() - this._damagePoint;
//			Debug.Log("HIT : " + healPointRemaining);
//			enemy.setHealPoint(healPointRemaining);

//			if(healPointRemaining <= 0.0f)
//			{
//                CancelInvoke("fight");
//				isAttacking = false;
//				Destroy(enemy.gameObject);

//				this._unitTarget = this.general;
//			}
//		}
//    }

//	public Unit[] ListOfNeighboors(float radius) // Return the tab containing all the neighboors of the player
//	{
//		Unit[] listOfUnit = GameObject.FindObjectsOfType<Unit>();
//		bool[] isInRadius = new bool[listOfUnit.Length];

//		int nbNeighboors = 0;

//		for(int i = 0; i < listOfUnit.Length; i++)
//		{
//			if (listOfUnit[i].gameObject != this.gameObject)
//			{
//				float distance = (listOfUnit[i]._currentPosition - this._simpleTarget.position).magnitude;

//				if (distance < radius)
//				{
//					isInRadius[i] = true;
//					nbNeighboors++;
//				}
//				else
//				{
//					isInRadius[i] = false;
//				}
//			}
//		}

//		int indiceNewList = 0;

//		Unit[] listOfNeighboors = new Unit[nbNeighboors];
//		for(int i = 0; i < listOfUnit.Length; i++)
//		{
//			if(isInRadius[i])
//			{
//				listOfNeighboors.SetValue(listOfUnit[i], indiceNewList);
//				indiceNewList++;
//			}
//		}

//		return listOfNeighboors;
//	}

//    /////////////////////
//    // SETTER - GETTER //
//    /////////////////////

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
