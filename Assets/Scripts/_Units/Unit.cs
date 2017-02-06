using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Unit : MovableEntity
{

    [HideInInspector]
    public float sizeRadius;


    public float _damagePoint;

    public FixedEntity _simpleTarget = null;
    public MovableEntity _unitTarget = null;

    //
    public string _name;
    public Player general;

    RecruitmentPattern recruitmentPattern = null;
    WavePattern wavePattern = null;
    public string currentState;

    public Scrollbar healthBar;

    public Animator _animator;

    public void init(FixedEntity spawner, Vector2 pos, RecruitmentPattern pattern)
    {
        this._simpleTarget = spawner;
        this._currentPosition = pos;
        this.updatePosition(this._currentPosition);

        _animator = GetComponent<Animator>();

        sizeRadius = 2.0f;
        timeBeforeChangePos = Random.Range(3.0f, 6.0f);

        recruitmentPattern = pattern;
    }

    public void init(FixedEntity spawner, Vector2 pos, WavePattern pattern)
    {
        this._simpleTarget = spawner;
        this._currentPosition = pos;
        this.updatePosition(this._currentPosition);
        this._faction = 2;

        this._maxSpeed = 2.0f;
        this._maxSteeringForce = 2.0f;

        _animator = GetComponent<Animator>();

        sizeRadius = 2.0f;
        timeBeforeChangePos = Random.Range(3.0f, 6.0f);

        wavePattern = pattern;
    }

    public void update()
    {
        Vector2 prevPosition = this._currentPosition;

        if (this._healPoint <= 0.0f)
        {
            Destroy(this.gameObject);
        }
        else
        {
            if (recruitmentPattern != null)
            {
                recruitmentPattern.updateState();
                currentState = recruitmentPattern.currentState.ToString();
            }
            else
            {
                wavePattern.updateState();
                currentState = wavePattern.currentState.ToString();
            }

            ChangeSizeOfHealthBar(this._healPoint);

            Vector2 vector = this._currentPosition - prevPosition;
            float angle = AngleBetweenVector2(vector, new Vector2(0.0f, 0.0f));
            transform.localEulerAngles = new Vector3(0.0f, -angle - 90, 0.0f);
        }
    }

    public void triggeringUpdate()
    {
        if (recruitmentPattern != null)
            recruitmentPattern.triggeringUpdate();
    }

    public void ChangeSizeOfHealthBar(float healthPoint)
    {
        healthBar.size = healthPoint / 10.0f;
        if (healthPoint != 10.0f)
            healthBar.size -= 0.1f;
    }

    /////////////////////
    // SETTER - GETTER //
    /////////////////////

    public string getName()
    { return _name; }
    public void setName(string newName)
    { _name = newName; }

    public Player getGeneral()
    { return general; }
    public void setGeneral(Player newGeneral)
    {
        general = newGeneral;
        setUnitTarget(general);
        setFaction(general._faction);
    }

    public int getFaction()
    { return _faction; }
    public void setFaction(int newFaction)
    { _faction = newFaction; }

    public float getHealPoint()
    { return _healPoint; }
    public void setHealPoint(float newHealPoint)
    { _healPoint = newHealPoint; }

    public MovableEntity getUnitTarget()
    { return this._unitTarget; }
    public void setUnitTarget(MovableEntity newUnitTarget)
    { this._unitTarget = newUnitTarget; }

    public FixedEntity getSimpleTarget()
    { return this._simpleTarget; }
    public void setSimpleTarget(FixedEntity newSimpleTarget)
    { this._simpleTarget = newSimpleTarget; }
}
