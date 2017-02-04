using UnityEngine;
using System.Collections;

public class RecruitmentPattern {
    
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

    [HideInInspector] public Unit _unit;

    // Constructor
    public RecruitmentPattern(Unit unit)
    {
        _unit = unit;

        waitBehavior = new WaitBehavior(_unit);
        seekBehavior = new SeekBehavior(_unit);
        pursuitBehavior = new PursuitBehavior(_unit);

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