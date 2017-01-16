using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PositionToHold : Buildings {

    private Object[] _EnemiesUnitsList;
    private int _faction;

    public void init(int faction)
    {
        _faction = faction;
        _EnemiesUnitsList = FindObjectsOfType(typeof(NPCUnit)) as NPCUnit[];
    }

    public void update()
    {
        getNearestEnemy();
    }

    public void getNearestEnemy()
    {
        List<NPCUnit> ListEnemies = new List<NPCUnit>();
        foreach(NPCUnit u in _EnemiesUnitsList)
            if (u.getFaction() != this._faction && u.getFaction() != 0)
                ListEnemies.Add(u);

        float minDistance = float.MaxValue;
        foreach (NPCUnit u in ListEnemies)
        {
            float distance = (this.position - u._currentPosition).magnitude;
            if (distance < minDistance)
            {
                this.nearestEnemy = u;
                minDistance = distance;
            }
        }
    }

    // DEBUG
    public void createUnit(NPCUnit unitPrefab, NPCUnit unitClone)
    {
        unitClone = Instantiate(unitPrefab) as NPCUnit;
        unitClone.init(this, nearToSpawner());
        unitClone.setFaction(2);

        // DEBUG
        unitClone.statePattern.currentState = unitClone.statePattern.holdPositionState;
    }

    private Vector2 nearToSpawner()
    {
        float x = Random.Range(position.x - 2, position.x + 2);
        float y = Random.Range(position.y - 2, position.y + 2);

        return new Vector2(x, y);
    }
}
