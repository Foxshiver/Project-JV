using UnityEngine;
using System.Collections;

public class UnitPrefab : MonoBehaviour
{
    // Attributes
    // PLAYER
    public PlayerUnit playerPrefab;
    PlayerUnit playerClone;

    public FoxUnit foxPrefab;
    FoxUnit foxClone;

    public ChickenUnit chickenPrefab;
    ChickenUnit chickenClone;

    public SnakeUnit snakePrefab;
    SnakeUnit snakeClone;

    // Use this for initialization
    void Start()
    {
        playerClone = Instantiate(playerPrefab) as PlayerUnit;
    }

    void Update()
    {
        if (Input.GetKeyDown("f"))
        {
            foxClone = Instantiate(foxPrefab) as FoxUnit;
            foxClone.changeTarget(playerClone);
        }
    }
}
