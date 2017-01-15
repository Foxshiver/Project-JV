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

    [HideInInspector]
    public float sizeRadius;
    [HideInInspector]
    public float timeBeforeChangePos;

    protected string _name;
    protected string _unitType;
    protected ArrayList _behaviors;

	public int _money;
    // Faction:
    //      > 0 = neutral
    //      > 1 = ally
    //      > 2 = enemy
    protected int _faction;
    protected float _fieldOfVision;

    protected float _healPoint;
    protected float _damagePoint;

    public Buildings _simpleTarget = null;
    public Unit _unitTarget = null;

	public enum State
	{
        Seek,
        Flee,
        Pursuit,
        Evade,
        Wait,
        Work,
        Defend,
        Fight        
    }

    protected State _stateUnit;

    public Unit()
    {
        _behaviors = new ArrayList();

        //_behaviors.Add(new SeekBehavior(this));         // [0] >>> Seek
        //_behaviors.Add(new FleeBehavior(this));         // [1] >>> Flee
        //_behaviors.Add(new PursuitBehavior(this));      // [2] >>> Pursuit
        //_behaviors.Add(new EvasionBehavior(this));      // [3] >>> Evasion
        //_behaviors.Add(new WaitBehavior(this));         // [4] >>> Wait        
        //_behaviors.Add(new LeaderBehavior(this));       // [5] >>> Leader

        Debug.Log("Unit constructor called");
    }

    void Start()
    {
		_currentPosition = new Vector2(this.transform.position.x, this.transform.position.z);
        //Debug.Log(_name + " POSITION = " + _currentPosition);
    }

    public void updatePosition(Vector2 position)
    {
		this.transform.position = new Vector3(position.x, this.transform.position.y, position.y);
    }

    protected Vector2 Vector3TOVector2(Vector3 position)
    {
        return new Vector2(position.x, position.z);
    }

    // Setter and Getter
    public string getName()
    { return _name; }
    public void setName(string newName)
    { _name = newName; }

    public string getUnitType()
    { return _unitType; }
    public void setUnitType(string newUnitType)
    { _unitType = newUnitType; }

    public int getFaction()
    { return _faction; }
    public void setFaction(int newFaction)
    { _faction = newFaction; }

    public float getHealPoint()
    { return _healPoint; }
    public void setHealPoint(float newHealPoint)
    { _healPoint = newHealPoint; }

    public float getDamagePoint()
    { return _damagePoint; }
    public void setDamagePoint(float newDamagePoint)
    { _damagePoint = newDamagePoint; }

    public State getState()
    { return this._stateUnit; }
    public void setState(State newSate)
    { this._stateUnit = newSate; }

    public Unit getUnitTarget()
    { return this._unitTarget; }
    public void setUnitTarget(Unit newUnitTarget)
    { this._unitTarget = newUnitTarget; }

    public Buildings getSimpleTarget()
    { return this._simpleTarget; }
    public void setSimpleTarget(Buildings newSimpleTarget)
    { this._simpleTarget = newSimpleTarget; }
}
