using UnityEngine;
using System.Collections;

public class StatePatternUnit {
    
    [HideInInspector] public IUnitState currentState;

    [HideInInspector] public WaitBehavior waitBehavior;
    [HideInInspector] public SeekBehavior seekBehavior;
    [HideInInspector] public PursuitBehavior pursuitBehavior;

    [HideInInspector] public WaitState waitState;
    [HideInInspector] public FollowLeaderState followLeaderState;
    [HideInInspector] public HoldPositionState holdPositionState;
    [HideInInspector] public DefendPositionState defendPositionState;
    [HideInInspector] public AttackEnemyState attackEnemyState;
    [HideInInspector] public WorkState workState;

    [HideInInspector] public NPCUnit _NPCUnit;

    // Constructor
    public StatePatternUnit(NPCUnit NPCUnit)
    {
        _NPCUnit = NPCUnit;

        waitBehavior = new WaitBehavior(_NPCUnit);
        seekBehavior = new SeekBehavior(_NPCUnit);
        pursuitBehavior = new PursuitBehavior(_NPCUnit);

        waitState = new WaitState(this, waitBehavior);
        followLeaderState = new FollowLeaderState(this, seekBehavior);
        holdPositionState = new HoldPositionState(this, waitBehavior);
        defendPositionState = new DefendPositionState(this, pursuitBehavior);
        attackEnemyState = new AttackEnemyState(this, pursuitBehavior);
        workState = new WorkState(this, waitBehavior);

        currentState = waitState;
    }
	
	// Update is called once per frame
	public void updateState()
    {
        currentState.UpdateState();
    }

    public void triggeringUpdate()
    {
        currentState.TriggeringUpdate();
    }
}