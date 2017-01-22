using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Field : Buildings
{
    [HideInInspector] public int _nbMaxUnit = 1;
    public int _nbMaxCoin = 1;
    
    public Coin coinPrefab;
    public List<Coin> coinsList;
    private Coin coinToDestroy = null;

    public void update()
    {
        foreach(Coin c in coinsList)
        {
            c.update();
            if(c.toDestroy)
                coinToDestroy = c;
        }

        if(coinToDestroy != null)
        {
            coinsList.Remove(coinToDestroy);
            Destroy(coinToDestroy.gameObject);
            coinToDestroy = null;
        }
    }

    public void createCoin()
    {
        if(coinsList.Count >= _nbMaxCoin)
            return;

        Coin coin = Instantiate(coinPrefab) as Coin;
        coin.start(nearToField(), this);
        coin.name = "Coin n°" + coinsList.Count;

        coinsList.Add(coin);
    }

    private Vector2 nearToField()
    {
        float x = Random.Range(this.position.x - 2, this.position.x + 2);
        float y = Random.Range(this.position.y - 2, this.position.y + 2);

        return new Vector2(x, y);
    }
}
