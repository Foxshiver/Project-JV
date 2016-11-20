using UnityEngine;
using System.Collections;

public class Farm : Buildings
{
    protected float _healPoint;
    protected int _faction;
	
	void Update ()
    {
	    if(_healPoint < 0.0f)
        {
            getPlayer(this._faction).lose();
        }
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
