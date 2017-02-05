using UnityEngine;
using System.Collections;

public class NestSpawner : FixedEntity {

    public Unit unitPrefab;
    Unit unitClone;
    public Unit newUnit = null;
    
    private int _nbMaxUnit = 3;

    public void update()
    {
	    if(_nbCurrentUnit < _nbMaxUnit)
            InvokeRepeating("createUnit", 5.0f, 1.0f);
	}

    public void createUnit()
    {
        unitClone = Instantiate(unitPrefab) as Unit;
        unitClone.init(this, nearToSpawner(), new RecruitmentPattern(unitClone));

        newUnit = unitClone;
        _nbCurrentUnit++;

        CancelInvoke("createUnit");
    }

    private Vector2 nearToSpawner()
    {
        float x = Random.Range(position.x - 2, position.x + 2);
        float y = Random.Range(position.y - 2, position.y + 2);

        return new Vector2(x, y);
    }
}
