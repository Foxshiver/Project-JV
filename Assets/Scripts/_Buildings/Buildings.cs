using UnityEngine;
using System.Collections;

public class Buildings : MonoBehaviour
{
    public Vector2 position;
    public string name;

    public int _nbCurrentUnit = 0;

    public void start()
    {
        position = Vector3TOVector2(this.transform.position);
    }

    protected Vector2 Vector3TOVector2(Vector3 position)
    {
        return new Vector2(position.x, position.z);
    }
}
