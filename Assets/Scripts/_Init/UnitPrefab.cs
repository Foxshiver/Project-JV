using UnityEngine;
using System.Collections;

public class UnitPrefab : MonoBehaviour
{
    // Attributes
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
        playerClone = Instantiate(playerPrefab) as PlayerUnit;

        foxClone = Instantiate(foxPrefab) as FoxUnit;
        foxClone._simpleTarget = foxSpawner;
        foxClone.transform.position = new Vector3(7.0f,0.5f,-2.0f);
        foxClone = Instantiate(foxPrefab) as FoxUnit;
        foxClone._simpleTarget = foxSpawner;

        chickenClone = Instantiate(chickenPrefab) as ChickenUnit;
        chickenClone._simpleTarget = chickenSpawner;
        chickenClone.transform.position = new Vector3(3.5f, 0.5f, 14.0f);
        chickenClone = Instantiate(chickenPrefab) as ChickenUnit;
        chickenClone._simpleTarget = chickenSpawner;

        snakeClone = Instantiate(snakePrefab) as SnakeUnit;
        snakeClone._simpleTarget = snakeSpawner;
        snakeClone.transform.position = new Vector3(15.0f, 0.5f, -8.0f);
        snakeClone = Instantiate(snakePrefab) as SnakeUnit;
        snakeClone._simpleTarget = snakeSpawner;
    }

    void Update()
    {
        if (Input.GetKeyDown("f"))
        {
            foxClone = Instantiate(foxPrefab) as FoxUnit;
            foxClone.changeTarget(playerClone);
            foxClone._simpleTarget = foxSpawner;
        }

        if (Input.GetKeyDown("h"))
        {
            chickenClone = Instantiate(chickenPrefab) as ChickenUnit;
            chickenClone.changeTarget(playerClone);
            chickenClone._simpleTarget = chickenSpawner;
        }

        if (Input.GetKeyDown("g"))
        {
            snakeClone = Instantiate(snakePrefab) as SnakeUnit;
            snakeClone.changeTarget(playerClone);
            snakeClone._simpleTarget = snakeSpawner;
        }
    }
}
