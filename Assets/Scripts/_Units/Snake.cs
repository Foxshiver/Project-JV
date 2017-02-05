using UnityEngine;
using System.Collections;

public class Snake : Unit {

    public Snake()
    {
        _name = "Snake";
        _faction = 0;
        _fieldOfView = 10.0f;
        _healPoint = 10.0f;
    }

    public float getDamage(string name)
    {
        switch (name)
        {
            case "Fox":
                return 5.0f;
            case "Chicken":
                return 1.0f;
            case "Snake":
                return 2.0f;
            default:
                Debug.Log("ERROR DAMAGE - WRONG NAME");
                return 0.0f;
        }
    }
}