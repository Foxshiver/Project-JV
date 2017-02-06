using UnityEngine;
using System.Collections;

public class PersistentData : MonoBehaviour
{
    public static float TimeAlive;
    public static int nbOfWaves;

    void Awake()
    {
        DontDestroyOnLoad(this);
    }
}
