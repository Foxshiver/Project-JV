using UnityEngine;
using System.Collections;

public class MoveNavMeshAgent : MonoBehaviour {

    NavMeshAgent agent;
    public NavMeshAgent target;
	// Use this for initialization
	void Start ()
    {
        agent = GetComponent<NavMeshAgent>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        var x = Input.GetAxis("Horizontal") * Time.deltaTime * 150.0f;
        var z = Input.GetAxis("Vertical") * Time.deltaTime * 5.0f;
        //agent.SetDestination(target.nextPosition);
        agent.Move(new Vector2(3 * Time.deltaTime, 0));
	}
}
