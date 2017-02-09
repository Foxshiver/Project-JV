using UnityEngine;
using System.Collections;

public class MovableEntity : MonoBehaviour {

    //
    public float _masse;
    public float _maxSpeed;
    public float _maxSteeringForce;

    public Vector2 _steering;
    public Vector2 _velocity;
    public Vector2 _direction;

    [HideInInspector] public float timeBeforeChangePos;

    //
    public Vector2 _currentPosition;

    // Faction:
    //      > 0 = neutral
    //      > 1 = ally
    //      > 2 = enemy
    public int _faction;
    public int _enemyFaction;

    public float _fieldOfView;
    public float _healPoint;
    public Animator _animatorEntity;

    
    void Start()
    {
        _currentPosition = new Vector2(this.transform.position.x, this.transform.position.z);
        _animatorEntity = gameObject.GetComponent<Animator>();
    }

    public void updatePosition(Vector2 position)
    { this.transform.position = new Vector3(position.x, this.transform.position.y, position.y); }

    protected Vector2 Vector3TOVector2(Vector3 position)
    { return new Vector2(position.x, position.z); }

    protected float AngleBetweenVector2(Vector2 vec1, Vector2 vec2)
    {
        Vector2 diference = vec2 - vec1;
        float sign = (vec2.y < vec1.y) ? -1.0f : 1.0f;
        return Vector2.Angle(Vector2.right, diference) * sign;
    }
}
