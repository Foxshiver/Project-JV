using UnityEngine;
using System.Collections;

public class Unit : MonoBehaviour
{
    public float _masse;
    public float _maxSpeed;
    public float _maxSteeringForce;
    public Vector2 _position;
    public Vector2 _direction;
    public Vector2 _velocity;
    public Vector2 _steering;

    public string _name;
    protected ArrayList _behaviors;

    public GameObject _simpleTarget;
    public Unit _targetUnit;

    public Unit()
    {
        _behaviors = new ArrayList();

        _behaviors.Add(new SeekBehavior(this));
        _behaviors.Add(new FleeBehavior(this));
        _behaviors.Add(new PursuitBehavior(this));
        _behaviors.Add(new EvasionBehavior(this));

        Debug.Log("Unit constructor called");
    }

    void Start()
    {
        _position = new Vector2(this.transform.position[0], this.transform.position[2]);
        Debug.Log(_name + " POSITION = " + _position);
    }

    protected void updatePosition(Vector2 position)
    {
        this.transform.position = new Vector3(position[0], this.transform.position[1], position[1]);
    }

    protected Vector2 Vector3TOVector2(Vector3 position)
    {
        return new Vector2(position[0], position[2]);
    }
}
