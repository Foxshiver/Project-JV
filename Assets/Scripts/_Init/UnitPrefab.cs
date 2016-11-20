using UnityEngine;
using System.Collections;

public class UnitPrefab : MonoBehaviour
{
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
    public GameObject foxSpawner;
    public GameObject chickenSpawner;
    public GameObject snakeSpawner;

    // Use this for initialization
    void Start()
    {
        // Instanciate 1 player
        playerClone = Instantiate(playerPrefab) as PlayerUnit;

        // Instanciate 2 neutral fox
        foxClone = Instantiate(foxPrefab) as FoxUnit;
        foxClone.setSimpleTarget(foxSpawner);
        foxClone.updatePosition(new Vector2(7.0f,-2.0f));
        foxClone = Instantiate(foxPrefab) as FoxUnit;
        foxClone.setSimpleTarget(foxSpawner);

        // Instanciate 2 neutral chicken
        chickenClone = Instantiate(chickenPrefab) as ChickenUnit;
        chickenClone.setSimpleTarget(chickenSpawner);
        chickenClone.updatePosition(new Vector2(3.5f,14.0f));
        chickenClone = Instantiate(chickenPrefab) as ChickenUnit;
        chickenClone.setSimpleTarget(chickenSpawner);

        // Instanciate 2 neutral snake
        snakeClone = Instantiate(snakePrefab) as SnakeUnit;
        snakeClone.setSimpleTarget(snakeSpawner);
        snakeClone.updatePosition(new Vector2(15.0f,-8.0f));
        snakeClone = Instantiate(snakePrefab) as SnakeUnit;
        snakeClone.setSimpleTarget(snakeSpawner);
    }

    void Update()
    {
        if (Input.GetKeyDown("f"))
        {
            foxClone = Instantiate(foxPrefab) as FoxUnit;
            foxClone.setSimpleTarget(foxSpawner);

            foxClone.setFaction(2);
        }

        if (Input.GetKeyDown("h"))
        {
            chickenClone = Instantiate(chickenPrefab) as ChickenUnit;
            chickenClone.setSimpleTarget(chickenSpawner);

            chickenClone.setFaction(2);
        }

        if (Input.GetKeyDown("g"))
        {
            snakeClone = Instantiate(snakePrefab) as SnakeUnit;
            snakeClone.setSimpleTarget(snakeSpawner);

            snakeClone.setFaction(2);
        }
    }
}
