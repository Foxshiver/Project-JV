using UnityEngine;
using System.Collections;

public class Coin : MonoBehaviour {

    public Vector2 position;
    public Rigidbody rgc;

    public void start()
    {
        position = Vector3TOVector2(this.transform.position);
    }

    protected Vector2 Vector3TOVector2(Vector3 position)
    {
        return new Vector2(position.x, position.z);
    }

    void OnColliderEnter(Collider col)
    {
        if(col.gameObject.tag == "Player")
        {
            Destroy(this.gameObject);  
        }
    }

}
