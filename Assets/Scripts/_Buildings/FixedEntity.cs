using UnityEngine;
using System.Collections;

public class FixedEntity : MonoBehaviour
{
    public Vector2 position;

    public float defendingArea = 8.0f;
    public Unit nearestEnemy = null;

    public float _healPoint;

    public int _nbCurrentUnit = 0;

    public void start()
    {
        _healPoint = 100.0f;
        position = Vector3TOVector2(this.transform.position);
    }

    protected Vector2 Vector3TOVector2(Vector3 position)
    {
        return new Vector2(position.x, position.z);
    }

    public float getHealPoint()
    { return _healPoint; }
    public void setHealPoint(float newHealPoint)
    { _healPoint = newHealPoint; }
}
