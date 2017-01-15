using UnityEngine;
using System.Collections;

public class Spawner : Buildings
{
    public NPCUnit unitPrefab;
    NPCUnit unitClone;
    public NPCUnit newUnit = null;

<<<<<<< HEAD
    private int _nbMaxUnit = 3;
    public bool _isCreatingUnit = false;
=======
    public GameObject World;

    void Start()
    {
        position = Vector3TOVector2(this.transform.position);
    }
>>>>>>> ffb495081a7cee26c4fb40b3762d7f9dd741f35a

    public void update()
    {
	    if(_nbCurrentUnit < _nbMaxUnit)
            InvokeRepeating("createUnit", 5.0f, 1.0f);
	}

    public void createUnit()
    {
        unitClone = Instantiate(unitPrefab) as NPCUnit;
<<<<<<< HEAD
        unitClone.init(this, nearToSpawner());
        newUnit = unitClone;

        _isCreatingUnit = true;
=======
        unitClone.updatePosition(nearToSpawner());
        unitClone.setSimpleTarget(this);
        unitClone.gameObject.transform.parent = World.transform;
>>>>>>> ffb495081a7cee26c4fb40b3762d7f9dd741f35a
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
