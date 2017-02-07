using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class MultiplayerGameEngine : MonoBehaviour {

    enum Substate
    {
        WaitForStart,
        Game,
        WaitForEnd,
        Pause
    };

    private Substate _substate;

    // PLAYER
    public Player playerPrefab;
    Player playerClone;

    // SPAWNERS
    public NestSpawner foxSpawner;
    public NestSpawner chickenSpawner;
    public NestSpawner snakeSpawner;

    // FARM
    public Farm player1QG;
    public Farm player2QG;

    // OBJECTS LISTS
    private Object[] _famrsList;
    private Object[] _spawnersList;
    private Object[] _fieldsList;

    private Object[] _neutralsUnitsList;

    // PAUSE GESTION
    public Canvas PauseCanvas;

    // MUSIC GESTION
    public AudioSource SourceMusic;

    // TIME AND SCORE GESTION
    private float startTime;

    void Start()
    {
        startTime = Time.time;

        loadScene();
        initUnits();

        _substate = Substate.WaitForStart;

        PauseCanvas.enabled = false;
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
        _spawnersList = FindObjectsOfType(typeof(NestSpawner)) as NestSpawner[];
        _fieldsList = FindObjectsOfType(typeof(Field)) as Field[];

        foreach(NestSpawner spawner in _spawnersList)
            spawner.start();

        foreach(Field field in _fieldsList)
            field.start();

        player1QG.start();
        player2QG.start();
    }

    void initUnits()
    {
        // Instanciate 1 player
        playerClone = Instantiate(playerPrefab) as Player;
        //playerClone._currentPosition = player1QG.position;
        //playerClone = Instantiate(playerPrefab) as Player;
        //playerClone._currentPosition = player2QG.position;
        //playerClone._faction = 2;

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

        _neutralsUnitsList = FindObjectsOfType(typeof(Unit)) as Unit[];
    }

    void endGame()
    {
        PersistentData.TimeAlive = Time.time - startTime;

        SceneManager.LoadScene("End Scene");
    }

    Substate checkChangeSubstate()
    {
        switch(_substate)
        {
            case Substate.WaitForStart:
                if(Input.GetButtonDown("JoystickStart"))
                {
                    PauseCanvas.enabled = true;
                    SourceMusic.Pause();
                    return Substate.Pause;
                }

                if(player1QG._healPoint <= 0.0f)
                    return Substate.WaitForEnd;

                if(player2QG._healPoint <= 0.0f)
                    return Substate.WaitForEnd;

                break;

            case Substate.Game:
                break;

            case Substate.WaitForEnd:
                endGame();
                break;

            case Substate.Pause:
                if(Input.GetButtonDown("JoystickStart"))
                {
                    PauseCanvas.enabled = false;
                    SourceMusic.Play();
                    return Substate.WaitForStart;
                }

                if(Input.GetButtonDown("CallBack")) // Exit with button 'X'
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
                foreach(NestSpawner spawner in _spawnersList)
                    spawner.update();

                foreach(Field field in _fieldsList)
                    field.update();

                _neutralsUnitsList = FindObjectsOfType(typeof(Unit)) as Unit[];
                foreach(Unit unit in _neutralsUnitsList)
                    unit.update();

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
