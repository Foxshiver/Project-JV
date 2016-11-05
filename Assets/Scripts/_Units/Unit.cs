using UnityEngine;
using System.Collections;

public class Unit : MonoBehaviour
{
    public float _masse;
    public float _maxSpeed;
    public float _maxSteeringForce;

    public Vector2 _currentPosition;
    public Vector2 _behindPosition;

    public Vector2 _direction;
    public Vector2 _velocity;
    public Vector2 _steering;

    protected string _name;
    protected ArrayList _behaviors;

	public int _money;

    public GameObject _simpleTarget = null;
    public Unit _targetUnit = null;

	public enum State
	{
		Wait,
        Pursuit,
        Evade
    }

	public State _stateUnit;

    public Unit()
    {
        _behaviors = new ArrayList();

        _behaviors.Add(new SeekBehavior(this));         // [0] >>> Seek
        _behaviors.Add(new FleeBehavior(this));         // [1] >>> Flee
        _behaviors.Add(new PursuitBehavior(this));      // [2] >>> Pursuit
        _behaviors.Add(new EvasionBehavior(this));      // [3] >>> Evasion
        _behaviors.Add(new LeaderBehavior(this));       // [4] >>> Leader
        _behaviors.Add(new WaitBehavior(this));         // [5] >>> Wait

        Debug.Log("Unit constructor called");
    }

    void Start()
    {
		_currentPosition = new Vector2(this.transform.position.x, this.transform.position.z);
        //Debug.Log(_name + " POSITION = " + _currentPosition);
    }

    public string getName()
    {
        return _name;
    }

    public void changeTarget(Unit newTarget)
    {
        this._targetUnit = newTarget;
    }

    public void updatePosition(Vector2 position)
    {
		this.transform.position = new Vector3(position.x, this.transform.position.y, position.y);
    }

    protected Vector2 Vector3TOVector2(Vector3 position)
    {
        return new Vector2(position.x, position.z);
    }
}
