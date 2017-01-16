using UnityEngine;
using System.Collections;

public class Farm : Buildings
{
    protected float _healPoint;
    protected int _faction;
	
	public void update ()
    {
	    if(_healPoint < 0.0f)
        {
            getPlayer(this._faction).lose();
        }
	}

    public void createUnit(NPCUnit unitPrefab, NPCUnit unitClone)
    {
        unitClone = Instantiate(unitPrefab) as NPCUnit;
        unitClone.init(this, nearToSpawner());
        unitClone.setFaction(2);
    }

    private Vector2 nearToSpawner()
    {
        float x = Random.Range(position.x - 2, position.x + 2);
        float y = Random.Range(position.y - 2, position.y + 2);

        return new Vector2(x, y);
    }

    private PlayerUnit getPlayer(int faction)
    {
        PlayerUnit[] listOfPlayerUnit = GameObject.FindObjectsOfType<PlayerUnit>();

        foreach(PlayerUnit playerUnit in listOfPlayerUnit)
        {
            if(playerUnit.getFaction() == faction)
                return playerUnit;
        }

        return null;
    }
}
