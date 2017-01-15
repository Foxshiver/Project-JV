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
    }

    void initUnits()
    {
        // Instanciate 1 player
        playerClone = Instantiate(playerPrefab) as PlayerUnit;
        //playerClone.updatePosition(new Vector2(22.0f, -16.0f));

        // Instanciate 2 neutral fox
        foxSpawner.createUnit();
        foxSpawner.createUnit();
        foxSpawner.createUnit();
        //foxClone.updatePosition(new Vector2(7.0f, -2.0f));

        // Instanciate 2 neutral chicken
        chickenSpawner.createUnit();
        chickenSpawner.createUnit();
        chickenSpawner.createUnit();
        //chickenClone.updatePosition(new Vector2(3.5f, 14.0f));

        // Instanciate 2 neutral snake
        snakeSpawner.createUnit();
        snakeSpawner.createUnit();
        snakeSpawner.createUnit();
        //snakeClone.updatePosition(new Vector2(15.0f, -8.0f));

        _neutralsUnitsList = FindObjectsOfType(typeof(NPCUnit)) as NPCUnit[];
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
                
                foreach(NPCUnit unit in _neutralsUnitsList)
                    unit.update();

                _neutralsUnitsList = FindObjectsOfType(typeof(NPCUnit)) as NPCUnit[];
                playerClone.update();

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
