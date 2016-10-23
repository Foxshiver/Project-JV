using UnityEngine;
using System.Collections;

public class LeaderBehavior : GeneralBehavior {

    // Attributes
    public float _radius;

    // Constructor
    public LeaderBehavior(Unit unit) : base(unit)
    {
        Debug.Log("LeaderBehavior constructor called");
    }

    // Functions
    public float getRadius() {
        return _radius;
    }
}
