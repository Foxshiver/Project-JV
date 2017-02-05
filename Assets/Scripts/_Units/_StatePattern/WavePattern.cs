using UnityEngine;
using System.Collections;

public class WavePattern {
    
    [HideInInspector] public IEnemyState currentState;

    [HideInInspector] public SeekBehavior seekBehavior;
    [HideInInspector] public PursuitBehavior pursuitBehavior;

    [HideInInspector] public ReachTargetState reachTargetState;
    [HideInInspector] public AttackTargetState attackTargetState;
    [HideInInspector] public AttackUnitState attackUnitState;

    [HideInInspector] public Unit _unit;

    // Constructor
    public WavePattern(Unit unit)
    {
        _unit = unit;

        seekBehavior = new SeekBehavior(_unit);
        pursuitBehavior = new PursuitBehavior(_unit);

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