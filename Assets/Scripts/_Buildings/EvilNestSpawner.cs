using UnityEngine;
using System.Collections;

public class EvilNestSpawner : FixedEntity {

    public Unit unitPrefab;
    Unit unitClone;
    public Unit newUnit = null;

    private int _nbUnitAttack;
    private int nbAttack = 0;

    public void update()
    {}

    public void createUnit(FixedEntity target)
    {
        nbAttack++;
        _nbUnitAttack = (int)Random.Range(1, nbAttack+2);

        for (int f=0; f<_nbUnitAttack; f++)
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
