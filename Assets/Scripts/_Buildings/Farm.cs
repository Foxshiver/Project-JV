using UnityEngine;
using System.Collections;

public class Farm : FixedEntity
{
    protected int _faction;

    public void update()
    {}

    private Player getPlayer(int faction)
    {
        Player[] listOfPlayerUnit = GameObject.FindObjectsOfType<Player>();

        foreach(Player player in listOfPlayerUnit)
        {
            if(player._faction == faction)
                return player;
        }

        return null;
    }
}
