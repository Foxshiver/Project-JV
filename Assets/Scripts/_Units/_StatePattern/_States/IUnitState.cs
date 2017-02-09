using UnityEngine;
using System.Collections;

public interface IUnitState {

    void UpdateState();

    void TriggeringUpdate();

    void ToWaitState();

    void ToFollowLeaderState();

    void ToHoldPositionState();

    void ToDefendPositionState();

    void ToAttackEnemyState();

    void ToAttackTargetState();

    void ToWorkState();
}
