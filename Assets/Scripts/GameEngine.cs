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
    public Player playerPrefab;
    Player playerClone;

    // SPAWNERS
    public NestSpawner foxSpawner;
    public NestSpawner chickenSpawner;
    public NestSpawner snakeSpawner;

    public EvilNestSpawner evilFoxSpawner;
    public EvilNestSpawner evilChickenSpawner;
    public EvilNestSpawner evilSnakeSpawner;

    // FARM
    public Farm allyQG;

    // OBJECTS LISTS
    private Object[] _famrsList;
    private Object[] _spawnersList;
    private Object[] _evilSpawnersList;
    private Object[] _fieldsList;

    private Object[] _neutralsUnitsList;
    private Object[] _enemyUnitsList;
    private Object[] _allyUnitsList;

    // ATTACK GESTION
    private float timeAttack;
    private float timeBeforeNextAttack;
    private int nbFoxAttack;
    private int nbSnakeAttack;
    private int nbChickenAttack;

    // PAUSE GESTION
    private bool pause = false;
    public Canvas PauseCanvas;

    // MUSIC GESTION
    public AudioSource SourceMusic;

    void Start()
    {
        loadScene();
        initUnits();

        _substate = Substate.WaitForStart;

        timeAttack = Time.time;
        timeBeforeNextAttack = 10.0f;

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
        _evilSpawnersList = FindObjectsOfType(typeof(EvilNestSpawner)) as EvilNestSpawner[];
        _fieldsList = FindObjectsOfType(typeof(Field)) as Field[];

        foreach(NestSpawner spawner in _spawnersList)
            spawner.start();

        foreach(EvilNestSpawner evilSpawner in _evilSpawnersList)
            evilSpawner.start();

        foreach(Field field in _fieldsList)
            field.start();

        allyQG.start();
    }

    void initUnits()
    {
        // Instanciate 1 player
        playerClone = Instantiate(playerPrefab) as Player;

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

    void addEnnemy()
    {
        if ((Time.time - timeAttack) > timeBeforeNextAttack)
        {
            timeBeforeNextAttack = Random.Range(25.0f, 50.0f);
            timeAttack = Time.time;
            
            evilFoxSpawner.createUnit(allyQG);
            evilChickenSpawner.createUnit(allyQG);
            evilSnakeSpawner.createUnit(allyQG);
        }
    }

    void endGame()
    {
        Debug.Log("You lose");
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

                if(allyQG._healPoint <= 0.0f)
                    return Substate.WaitForEnd;

                break;

            case Substate.Game:
                break;

            case Substate.WaitForEnd:
                endGame();
                break;

            case Substate.Pause:
                if (Input.GetButtonDown("JoystickStart"))
                {
                    PauseCanvas.enabled = false;
                    SourceMusic.Play();
                    return Substate.WaitForStart;
                }

                if (Input.GetButtonDown("CallBack")) // Exit with button 'X'
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

                foreach(EvilNestSpawner evilSpawner in _evilSpawnersList)
                    evilSpawner.update();

                foreach(Field field in _fieldsList)
                    field.update();

                _neutralsUnitsList = FindObjectsOfType(typeof(Unit)) as Unit[];
                foreach(Unit unit in _neutralsUnitsList)
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
