﻿using UnityEngine;
using System.Collections;

public class SnakeUnit : NPCUnit
{
    // Constructor
    public SnakeUnit()
    {
        _name = "Snake";
        _faction = 0;
        _fieldOfView = 3.0f;
        _healPoint = 10.0f;

        Debug.Log("SnakeUnit constructor called");
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