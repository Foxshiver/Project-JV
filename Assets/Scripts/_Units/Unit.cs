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

<<<<<<< HEAD
    public GameObject _simpleTarget;
	public Unit _targetUnit;

	public int _money;
=======
    public GameObject _simpleTarget = null;
    public Unit _targetUnit = null;
>>>>>>> 58fa32dba1df0f1a89aead6f3ec1b4c47d74d6f6

	public enum State
	{
		    WaitFree,
		    SeekLeader,
		    WaitWork,
		    Fight
	}

	public State _stateUnit;

    public Unit()
    {
        _behaviors = new ArrayList();

        _behaviors.Add(new SeekBehavior(this));
        _behaviors.Add(new FleeBehavior(this));
        _behaviors.Add(new PursuitBehavior(this));
        _behaviors.Add(new EvasionBehavior(this));
		_behaviors.Add(new LeaderBehavior(this));
		_behaviors.Add (new WaitBehavior (this));

        Debug.Log("Unit constructor called");
    }

    void Start()
    {
		_currentPosition = new Vector2(this.transform.position.x, this.transform.position.z);
        //Debug.Log(_name + " POSITION = " + _currentPosition);

		_stateUnit = Unit.State.WaitFree;
    }

    public string getName()
    {
        return _name;
    }

    public void changeTarget(Unit newTarget)
    {
        this._targetUnit = newTarget;
    }

    protected void updatePosition(Vector2 position)
    {
		this.transform.position = new Vector3(position.x, this.transform.position.y, position.y);
    }

    protected Vector2 Vector3TOVector2(Vector3 position)
    {
        return new Vector2(position.x, position.z);
    }
}
