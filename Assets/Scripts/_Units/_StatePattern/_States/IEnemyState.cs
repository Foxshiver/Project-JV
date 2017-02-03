
public interface IEnemyState
{
    void UpdateState();

    void ToAttackUnitState();

    void ToReachTargetState();

    void ToAttackTargetState();

}
