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
    public Spawner foxSpawner;
    public Spawner chickenSpawner;
    public Spawner snakeSpawner;

    // ENEMY FARM
    public Farm enemyFarm;

    // 
    public GameObject World;

    private float timeAttack;
    private int nbFoxAttack;
    private int nbSnakeAttack;
    private int nbChickenAttack;
    private float timeBeforeNextAttack;

    // Use this for initialization
    void Start()
    {
        timeAttack = Time.time;
        timeBeforeNextAttack = 30.0f;

        // Instanciate 1 player
        playerClone = Instantiate(playerPrefab) as PlayerUnit;
        playerClone.updatePosition(new Vector2(17.0f, -33.0f));
        playerClone.gameObject.transform.parent = World.gameObject.transform;

        // Instanciate 2 neutral fox
        foxClone = Instantiate(foxPrefab) as FoxUnit;
        foxClone.setSimpleTarget(foxSpawner);
        foxClone.gameObject.transform.parent = World.gameObject.transform;
        foxSpawner._nbCurrentUnit++;
        foxClone.updatePosition(new Vector2(7.0f,-2.0f));
        foxClone = Instantiate(foxPrefab) as FoxUnit;
        foxClone.setSimpleTarget(foxSpawner);
        foxClone.gameObject.transform.parent = World.gameObject.transform;
        foxSpawner._nbCurrentUnit++;

        // Instanciate 2 neutral chicken
        chickenClone = Instantiate(chickenPrefab) as ChickenUnit;
        chickenClone.setSimpleTarget(chickenSpawner);
        chickenClone.gameObject.transform.parent = World.gameObject.transform;
        chickenSpawner._nbCurrentUnit++;
        chickenClone.updatePosition(new Vector2(3.5f,14.0f));
        chickenClone = Instantiate(chickenPrefab) as ChickenUnit;
        chickenClone.setSimpleTarget(chickenSpawner);
        chickenClone.gameObject.transform.parent = World.gameObject.transform;
        chickenSpawner._nbCurrentUnit++;

        // Instanciate 2 neutral snake
        snakeClone = Instantiate(snakePrefab) as SnakeUnit;
        snakeClone.setSimpleTarget(snakeSpawner);
        snakeClone.gameObject.transform.parent = World.gameObject.transform;
        snakeSpawner._nbCurrentUnit++;
        snakeClone.updatePosition(new Vector2(15.0f,-8.0f));
        snakeClone = Instantiate(snakePrefab) as SnakeUnit;
        snakeClone.setSimpleTarget(snakeSpawner);
        snakeClone.gameObject.transform.parent = World.gameObject.transform;
        snakeSpawner._nbCurrentUnit++;
    }

    void Update()
    {
        if ((Time.time - timeAttack) > timeBeforeNextAttack)
        {
            nbFoxAttack = (int)Random.Range(1.0f, 4.0f);
            nbSnakeAttack = (int)Random.Range(1.0f, 4.0f);
            nbChickenAttack = (int)Random.Range(1.0f, 4.0f);

            timeBeforeNextAttack = Random.Range(25.0f, 50.0f);

            timeAttack = Time.time;

            for(int f=0; f < nbFoxAttack;f++)
            {
                foxClone = Instantiate(foxPrefab) as FoxUnit;
                foxClone.setSimpleTarget(enemyFarm);

                foxClone.setFaction(2);
            }

            for (int c = 0; c < nbChickenAttack; c++)
            {
                chickenClone = Instantiate(chickenPrefab) as ChickenUnit;
                chickenClone.setSimpleTarget(enemyFarm);

                chickenClone.setFaction(2);
            }

            for (int s = 0; s < nbSnakeAttack; s++)
            {
                snakeClone = Instantiate(snakePrefab) as SnakeUnit;
                snakeClone.setSimpleTarget(enemyFarm);

                snakeClone.setFaction(2);
            }

        }

        //if (Input.GetKeyDown("f"))
        //{
        //    foxClone = Instantiate(foxPrefab) as FoxUnit;
        //    foxClone.setSimpleTarget(enemyFarm);

        //    foxClone.setFaction(2);
        //}

        //if (Input.GetKeyDown("h"))
        //{
        //    chickenClone = Instantiate(chickenPrefab) as ChickenUnit;
        //    chickenClone.setSimpleTarget(enemyFarm);

        //    chickenClone.setFaction(2);
        //}

        //if (Input.GetKeyDown("g"))
        //{
        //    snakeClone = Instantiate(snakePrefab) as SnakeUnit;
        //    snakeClone.setSimpleTarget(enemyFarm);

        //    snakeClone.setFaction(2);
        //}
    }
}
