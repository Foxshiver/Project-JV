﻿using UnityEngine;
using System.Collections;

public class Coin : MonoBehaviour {

    public Vector2 position;
    private Field field;
    private Player[] player;

    public AudioClip CoinSound;

    public bool toDestroy = false;

    public void start(Vector2 position, Field field)
    {
        this.field = field;
        this.position = position;
        this.transform.position = new Vector3(this.position.x, this.transform.position.y, this.position.y);

        player = FindObjectsOfType(typeof(Player)) as Player[];
    }

    public void update()
    {
        this.position = Vector3TOVector2(this.transform.position);

        for(int i=0; i<player.Length; i++)
        {
            float distance = (player[i]._currentPosition - this.position).magnitude;
            if(distance < 1.0f)
            {
                player[i]._money++;
                AudioSource.PlayClipAtPoint(CoinSound, this.transform.position,0.01f);
                toDestroy = true;
            }
        }
    }

    protected Vector2 Vector3TOVector2(Vector3 position)
    {
        return new Vector2(position.x, position.z);
    }
}
