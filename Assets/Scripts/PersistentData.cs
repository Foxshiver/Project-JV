using UnityEngine;
using System.Collections;

public class PersistentData : MonoBehaviour
{
    public static float TimeAlive;
    public static int nbOfWaves;
    public static int nbPlayer;
    public static int winner;

    void Awake()
    {
        DontDestroyOnLoad(this);
    }
}
