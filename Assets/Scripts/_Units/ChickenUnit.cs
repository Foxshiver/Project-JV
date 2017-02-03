using UnityEngine;
using System.Collections;

public class ChickenUnit : NPCUnit
{
    // Constructor
    public ChickenUnit()
    {
        _name = "Chicken";
        _faction = 0;
        _fieldOfView = 3.0f;
        _healPoint = 10.0f;

    }

    public float getDamage(string name)
    {
        switch (name)
        {
            case "Fox":
                return 1.0f;
            case "Chicken":
                return 2.0f;
            case "Snake":
                return 5.0f;
            default:
                Debug.Log("ERROR DAMAGE - WRONG NAME");
                return 0.0f;
        }
    }
}