using UnityEngine;
using System.Collections;

public class FoxUnit : NPCUnit
{
    // Constructor
    public FoxUnit()
    {
        _name = "Fox";
        _faction = 0;
        _fieldOfView = 3.0f;
        _healPoint = 10.0f;

        Debug.Log("FoxUnit constructor called");
    }

    public float getDamage(string name)
    {
        switch (name)
        {
            case "Fox":
                return 2.0f;
            case "Chicken":
                return 5.0f;
            case "Snake":
                return 1.0f;
            default:
                Debug.Log("ERROR DAMAGE - WRONG NAME");
                return 0.0f;
        }
    }
}