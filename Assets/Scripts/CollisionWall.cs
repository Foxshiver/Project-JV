using UnityEngine;
using System.Collections;

public class CollisionWall : MonoBehaviour {

	Collider _WallCollider;

	Vector2 _posEnter;

	// Use this for initialization
	void Start () {
		_WallCollider = GetComponent<BoxCollider> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider other) {
		_posEnter = new Vector2(other.gameObject.transform.position.x,other.gameObject.transform.position.z);

		Debug.Log ("Enter in " + other.gameObject.name);
	}

	void OnTriggerStay(Collider other) {
		other.gameObject.transform.position = new Vector3( _posEnter.x,0.0f,_posEnter.y);

		Debug.Log ("Stay in " + other.gameObject.name);
	}

	void OnTriggerExit(Collider other) {

		Debug.Log ("Exit of " + other.gameObject.name);
	}

}
