using UnityEngine;
using System.Collections;

public class StatePatternEnemy {
    
    [HideInInspector] public IEnemyState currentState;

    [HideInInspector] public SeekBehavior seekBehavior;
    [HideInInspector] public PursuitBehavior pursuitBehavior;

    [HideInInspector] public ReachTargetState reachTargetState;
    [HideInInspector] public AttackTargetState attackTargetState;
    [HideInInspector] public AttackUnitState attackUnitState;

    [HideInInspector] public NPCUnit _NPCUnit;

    // Constructor
    public StatePatternEnemy(NPCUnit NPCUnit)
    {
        _NPCUnit = NPCUnit;

        seekBehavior = new SeekBehavior(_NPCUnit);
        pursuitBehavior = new PursuitBehavior(_NPCUnit);

        reachTargetState = new ReachTargetState(this, seekBehavior);
        attackTargetState = new AttackTargetState(this, seekBehavior);
        attackUnitState = new AttackUnitState(this, pursuitBehavior);

        currentState = reachTargetState;
    }
	
	// Update is called once per frame
	public void updateState()
    {
        currentState.UpdateState();
    }
}