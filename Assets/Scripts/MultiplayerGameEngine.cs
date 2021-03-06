﻿using UnityEngine;
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
    Player player1Clone;
    Player player2Clone;

    // SPAWNERS
    public NestSpawner foxSpawner1;
    public NestSpawner foxSpawner2;
    public NestSpawner snakeSpawner1;
    public NestSpawner snakeSpawner2;
    public NestSpawner chickenSpawner1;
    public NestSpawner chickenSpawner2;

    // FARM
    public Farm player1QG;
    public Farm player2QG;

    // OBJECTS LISTS
    private Object[] _famrsList;
    private Object[] _spawnersList;
    private Object[] _fieldsList;

    private Object[] _neutralsUnitsList;

    // CANVAS GESTION
    public Canvas player1_Canvas;
    public Canvas player2_Canvas;

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
        initScene();

        _substate = Substate.WaitForStart;

        PauseCanvas.enabled = false;

        PersistentData.nbPlayer = 2;
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

    void initScene()
    {
        // Instanciate 2 players
        player1Clone = Instantiate(playerPrefab) as Player;
        player1Clone.init(player1QG, player2QG, 1, 2, 1);
        player1_Canvas.worldCamera = player1Clone.GetComponentInChildren<Camera>();
        player1_Canvas.worldCamera.rect = new Rect(0.0f, 0.0f, 0.5f, 1.0f);
        player1_Canvas.GetComponentInChildren<GestionUserInterface>()._player = player1Clone;

        player2Clone = Instantiate(playerPrefab) as Player;
        player2Clone.init(player2QG, player1QG, 2, 1, 2);
        player2_Canvas.worldCamera = player2Clone.GetComponentInChildren<Camera>();
        player2_Canvas.worldCamera.rect = new Rect(0.5f, 0.0f, 1.0f, 1.0f);
        player2_Canvas.GetComponentInChildren<GestionUserInterface>()._player = player2Clone;

        // Instanciate 3 neutral fox
        foxSpawner1.createUnit();
        foxSpawner1.createUnit();
        foxSpawner1.createUnit();

        foxSpawner2.createUnit();
        foxSpawner2.createUnit();
        foxSpawner2.createUnit();

        // Instanciate 3 neutral chicken
        chickenSpawner1.createUnit();
        chickenSpawner1.createUnit();
        chickenSpawner1.createUnit();

        chickenSpawner2.createUnit();
        chickenSpawner2.createUnit();
        chickenSpawner2.createUnit();

        // Instanciate 3 neutral snake
        snakeSpawner1.createUnit();
        snakeSpawner1.createUnit();
        snakeSpawner1.createUnit();

        snakeSpawner2.createUnit();
        snakeSpawner2.createUnit();
        snakeSpawner2.createUnit();

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
                {
                    PersistentData.winner = 2;
                    return Substate.WaitForEnd;
                }

                if(player2QG._healPoint <= 0.0f)
                {
                    PersistentData.winner = 1;
                    return Substate.WaitForEnd;
                }

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

                if(Input.GetButtonDown("JoystickB"))
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

                player1Clone.update();
                player2Clone.update();

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
