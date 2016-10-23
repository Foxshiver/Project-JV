using UnityEngine;
using System.Collections;

public class InitProg {

    // Attributes
    public FoxUnit unit_1;
    public FoxUnit unit_2;
    public FoxUnit unit_3;
    public FoxUnit unit_4;

    // Use this for initialization
    void Start () {
        unit_1 = new FoxUnit();
        unit_2 = new FoxUnit();
        unit_3 = new FoxUnit();
        unit_4 = new FoxUnit();
    }
}
