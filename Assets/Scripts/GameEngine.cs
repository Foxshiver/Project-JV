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

    // OBJECTS LISTS
    private Object[] _famrsList;
    private Object[] _spawnersList;
    private Object[] _fieldsList;

    private Object[] _neutralsUnitsList;
    private Object[] _enemyUnitsList;
    private Object[] _allyUnitsList;

    void Start()
    {
        loadScene();
        initUnits();

        _substate = Substate.WaitForStart;
    }

    // Update is called once per frame
    void Update()
    {
        _substate = checkChangeSubstate();
        executeSubstate();        
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
        if (Input.GetKeyDown("f"))
            enemyQG.createUnit(foxPrefab, foxClone);

        if (Input.GetKeyDown("h"))
            enemyQG.createUnit(chickenPrefab, chickenClone);

        if (Input.GetKeyDown("g"))
            enemyQG.createUnit(snakePrefab, snakeClone);
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
