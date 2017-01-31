using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameEngine : MonoBehaviour {

    enum Substate
    {
        WaitForStart,
        Game,
        WaitForEnd,
        Pause
    };

    private Substate _substate;

    // PLAYER
    public PlayerUnit playerPrefab;
    PlayerUnit playerClone;

    // UNITS
    public FoxUnit foxPrefab;
    FoxUnit foxClone;

    public ChickenUnit chickenPrefab;
    ChickenUnit chickenClone;

    public SnakeUnit snakePrefab;
    SnakeUnit snakeClone;

    // SPAWNERS
    public Spawner foxSpawner;
    public Spawner chickenSpawner;
    public Spawner snakeSpawner;

    // ENEMY FARM --- DEBUG
    public Farm enemyQG;
    private PositionToHold positionEnemy;

    // OBJECTS LISTS
    private Object[] _famrsList;
    private Object[] _spawnersList;
    private Object[] _fieldsList;

    private Object[] _neutralsUnitsList;
    private Object[] _enemyUnitsList;
    private Object[] _allyUnitsList;

    // ATTACK GESTION
    private float timeAttack;
    private int nbFoxAttack;
    private int nbSnakeAttack;
    private int nbChickenAttack;
    private float timeBeforeNextAttack;

    // PAUSE GESTION
    private bool pause = false;
    public Canvas CanvPause;

    // MUSIC GESTION
    public AudioSource SourceMusic;

    void Start()
    {
        loadScene();
        initUnits();

        _substate = Substate.WaitForStart;

        timeAttack = Time.time;
        timeBeforeNextAttack = 30.0f;

        CanvPause.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("JoystickStart"))
        {
            if (!pause)
            {
                CanvPause.enabled = true;
                SourceMusic.Pause();
            }
            else
            {
                CanvPause.enabled = false;
                SourceMusic.Play();
            }


            pause = !pause;
        }

        if(!pause)
        {
            _substate = checkChangeSubstate();
            executeSubstate();
        }
    }

    void loadScene()
    {
        _famrsList = FindObjectsOfType(typeof(Farm)) as Farm[];
        _spawnersList = FindObjectsOfType(typeof(Spawner)) as Spawner[];
        _fieldsList = FindObjectsOfType(typeof(Field)) as Field[];

        foreach(Spawner spawner in _spawnersList)
            spawner.start();

        foreach(Field field in _fieldsList)
            field.start();

        enemyQG.start();
        positionEnemy = this.gameObject.AddComponent<PositionToHold>();
        positionEnemy.start();
        positionEnemy.init(2);
        positionEnemy.position = enemyQG.position;
    }

    void initUnits()
    {
        // Instanciate 1 player
        playerClone = Instantiate(playerPrefab) as PlayerUnit;
        //playerClone.updatePosition(new Vector2(22.0f, -16.0f));

        // Instanciate 3 neutral fox
        foxSpawner.createUnit();
        foxSpawner.createUnit();
        foxSpawner.createUnit();

        // Instanciate 3 neutral chicken
        chickenSpawner.createUnit();
        chickenSpawner.createUnit();
        chickenSpawner.createUnit();

        // Instanciate 3 neutral snake
        snakeSpawner.createUnit();
        snakeSpawner.createUnit();
        snakeSpawner.createUnit();

        _neutralsUnitsList = FindObjectsOfType(typeof(NPCUnit)) as NPCUnit[];
    }

    void addEnnemy()
    {
        if ((Time.time - timeAttack) > timeBeforeNextAttack)
        {
            nbFoxAttack = (int)Random.Range(1.0f, 4.0f);
            nbSnakeAttack = (int)Random.Range(1.0f, 4.0f);
            nbChickenAttack = (int)Random.Range(1.0f, 4.0f);

            timeBeforeNextAttack = Random.Range(25.0f, 50.0f);

            timeAttack = Time.time;

            Debug.Log("ATTACK !!   nbFox : " + nbFoxAttack + " - nbchick : " + nbChickenAttack + "  - nbSnak : " + nbSnakeAttack);

            for (int f = 0; f < nbFoxAttack; f++)
            {
                positionEnemy.createUnit(foxPrefab, foxClone);
            }

            for (int c = 0; c < nbChickenAttack; c++)
            {
                positionEnemy.createUnit(chickenPrefab, chickenClone);
            }

            for (int s = 0; s < nbSnakeAttack; s++)
            {
                positionEnemy.createUnit(snakePrefab, snakeClone);
            }

        }

        //if (Input.GetKeyDown("f"))
        //    positionEnemy.createUnit(foxPrefab, foxClone);

        //if (Input.GetKeyDown("h"))
        //    positionEnemy.createUnit(chickenPrefab, chickenClone);

        //if (Input.GetKeyDown("g"))
        //    positionEnemy.createUnit(snakePrefab, snakeClone);
    }

    void endGame()
    {

    }

    Substate checkChangeSubstate()
    {
        switch(_substate)
        {
            case Substate.WaitForStart:
                //return Substate.Game;
                break;

            case Substate.Game:
               //return Substate.WaitForEnd;

                if(Input.GetButtonDown("Start"))
                {
                    //PauseCanvas.enabled = true;
                    return Substate.Pause;
                }
                break;

            case Substate.WaitForEnd:
                endGame();
                break;

            case Substate.Pause:
                if(Input.GetButtonDown("Start"))
                {
                    //PauseCanvas.enabled = false;
                    return Substate.Game;
                }

                if(Input.GetButtonDown("Exit"))
                    Application.Quit();
                break;
        }

        return _substate;
    }

    void executeSubstate()
    {
        switch(_substate)
        {
            case Substate.WaitForStart:

                foreach(Spawner spawner in _spawnersList)
                {
                    spawner.update();
                    
                    //if(spawner._isCreatingUnit)
                    //{
                    //    _neutralsUnitsList.SetValue(spawner.newUnit, _neutralsUnitsList.Length-1);
                    //    spawner._isCreatingUnit = false;
                    //}
                }

                foreach(Field field in _fieldsList)
                {
                    field.update();
                }

                positionEnemy.update();

                _neutralsUnitsList = FindObjectsOfType(typeof(NPCUnit)) as NPCUnit[];
                foreach(NPCUnit unit in _neutralsUnitsList)
                    unit.update();
                playerClone.update();

                addEnnemy();

                break;

            case Substate.Game:
                break;

            case Substate.WaitForEnd:
                break;

            case Substate.Pause:
                break;
        }
    }
}
