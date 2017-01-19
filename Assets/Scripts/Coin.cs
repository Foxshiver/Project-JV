using UnityEngine;
using System.Collections;

public class Coin : MonoBehaviour {

    public Vector2 position;
    private Field field;
    private PlayerUnit player;

    public bool toDestroy = false;

    public void start(Vector2 position, Field field)
    {
        this.field = field;
        this.position = position;
        this.transform.position = new Vector3(this.position.x, this.transform.position.y, this.position.y);

        player = FindObjectOfType(typeof(PlayerUnit)) as PlayerUnit;
    }

    public void update()
    {
        this.position = Vector3TOVector2(this.transform.position);

        float distance = (player._currentPosition - this.position).magnitude;
        if(distance < 1.0f)
        {
            player._money++;
            toDestroy = true;
        }
    }

    protected Vector2 Vector3TOVector2(Vector3 position)
    {
        return new Vector2(position.x, position.z);
    }
}
