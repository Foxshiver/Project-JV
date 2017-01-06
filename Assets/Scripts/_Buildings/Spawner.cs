using UnityEngine;
using System.Collections;

public class Spawner : Buildings
{
    private int _nbMaxUnit = 3;

    public NPCUnit unitPrefab;
    NPCUnit unitClone;

    public GameObject World;

    void Start()
    {
        position = Vector3TOVector2(this.transform.position);
    }

    void Update ()
    {
	    if(_nbCurrentUnit < _nbMaxUnit)
        {
            InvokeRepeating("createUnit", 5.0f, 1.0f);
        }
	}

    private void createUnit()
    {
        unitClone = Instantiate(unitPrefab) as NPCUnit;
        unitClone.updatePosition(nearToSpawner());
        unitClone.setSimpleTarget(this);
        unitClone.gameObject.transform.parent = World.transform;
        _nbCurrentUnit++;

        CancelInvoke("createUnit");
    }

    private Vector2 nearToSpawner()
    {
        Vector3 spawnerPosition = this.position;

        float x = Random.Range(spawnerPosition.x - 1, spawnerPosition.x + 1);
        float y = Random.Range(spawnerPosition.z - 1, spawnerPosition.z + 1);

        return new Vector2(x, y);
    }
}
