using UnityEngine;
using System.Collections;

public class EvilNestSpawner : FixedEntity {

    public Unit unitPrefab;
    Unit unitClone;
    public Unit newUnit = null;

    private int _nbUnitAttack;

    public void update()
    {}

    public void createUnit(FixedEntity target)
    {
        _nbUnitAttack = (int)Random.Range(2.0f, 6.0f);

        for(int f=0; f<_nbUnitAttack; f++)
        {
            unitClone = Instantiate(unitPrefab) as Unit;
            unitClone.init(target, nearToSpawner(), new WavePattern(unitClone));

            newUnit = unitClone;
        }
    }

    private Vector2 nearToSpawner()
    {
        float x = Random.Range(position.x - 2, position.x + 2);
        float y = Random.Range(position.y - 2, position.y + 2);

        return new Vector2(x, y);
    }
}
