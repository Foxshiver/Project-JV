using UnityEngine;
using System.Collections;

public class BehindPoint : MonoBehaviour {

    public Transform Leader;
    Transform Behind_Sphere;
    public int LEADER_BEHIND_DIST = 3;

    private Vector3 tv;
    private Vector3 behind;

    // Use this for initialization
    void Start()
    {
        Behind_Sphere = GetComponent<Transform>();

        tv = Leader.GetComponent<Rigidbody>().velocity * -1;
    }

    // Update is called once per frame
    void Update()
    {

        tv = tv.normalized * LEADER_BEHIND_DIST;
        behind = Leader.position + tv;

        Behind_Sphere.position = behind;

    }

    Vector3 followLeader(Transform Leader)
    {
        Vector3 tv = Leader.GetComponent<Rigidbody>().velocity * -1;
        Vector3 force = new Vector3();

        tv = tv.normalized * LEADER_BEHIND_DIST;
        behind = Leader.position + tv;

        force = force + behind;

        return force;
    }
}
