using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PositionToHold : FixedEntity {

    private Object[] _EnemiesUnitsList;
    private int _faction;

    public void init(int faction)
    {
        _faction = faction;
    }

    public void update()
    {
        getNearestEnemy();
    }

    public void getNearestEnemy()
    {
        _EnemiesUnitsList = FindObjectsOfType(typeof(Unit)) as Unit[];
        float minDistance = float.MaxValue;
        List<Unit> ListEnemies = new List<Unit>();
        foreach(Unit u in _EnemiesUnitsList)
            if (u.getFaction() != this._faction && u.getFaction() != 0)
            {
                float distance = (this.position - u._currentPosition).magnitude;
                if (distance < minDistance)
                {
                    this.nearestEnemy = u;
                    minDistance = distance;
                }
            }
    }
}
