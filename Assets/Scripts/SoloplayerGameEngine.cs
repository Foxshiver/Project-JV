using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class SoloplayerGameEngine : MonoBehaviour {

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

    // FARM
    public Farm allyQG;

    // OBJECTS LISTS
    private Object[] _famrsList;
    private Object[] _spawnersList;
    private Object[] _evilSpawnersList;
    private Object[] _fieldsList;

    private Object[] _neutralsUnitsList;

    // CANVAS GESTION
    public Canvas playerCanvas;

    // ATTACK GESTION
    private float timeAttack;
    private float timeBeforeNextAttack;

    // PAUSE GESTION
    public Canvas PauseCanvas;

    // MUSIC GESTION
    public AudioSource SourceMusic;

    // TIME AND SCORE GESTION
    private float startTime;
    private int nbWaves;

    void Start()
    {
        startTime = Time.time;
        nbWaves = 0;

        loadScene();
        initScene();

        _substate = Substate.WaitForStart;

        timeAttack = Time.time;
        timeBeforeNextAttack = 10.0f;

        PauseCanvas.enabled = false;

        PersistentData.nbPlayer = 1;
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

    void initScene()
    {
        // Instanciate 1 player
        playerClone = Instantiate(playerPrefab) as Player;
        playerClone.init(allyQG, 1, 2, 1);
        playerCanvas.GetComponentInChildren<GestionUserInterface>()._player = playerClone;

        foreach (NestSpawner spawn in _spawnersList)
        {
            spawn.createUnit();
            spawn.createUnit();
            spawn.createUnit();
        }

        _neutralsUnitsList = FindObjectsOfType(typeof(Unit)) as Unit[];
    }

    void addEnnemy()
    {
        if ((Time.time - timeAttack) > timeBeforeNextAttack)
        {
            if (nbWaves >= 10)
                endGame();
            nbWaves++;

            timeBeforeNextAttack = Random.Range(25.0f, 50.0f);
            timeAttack = Time.time;

            foreach (EvilNestSpawner spawn in _evilSpawnersList)
                spawn.createUnit(allyQG);
        }
    }

    void endGame()
    {
        PersistentData.TimeAlive = Time.time - startTime;
        PersistentData.nbOfWaves = nbWaves;

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

                if (Input.GetButtonDown("JoystickB"))
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
