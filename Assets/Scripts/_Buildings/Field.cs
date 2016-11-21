using UnityEngine;
using System.Collections;

public class Field : Buildings
{
    public int _nbMaxUnit = 1;

    void Start()
    {
        position = Vector3TOVector2(this.transform.position);
    }
}
