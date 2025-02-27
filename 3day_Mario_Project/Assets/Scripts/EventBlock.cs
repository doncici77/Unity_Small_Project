using System;
using System.Collections;
using UnityEngine;

public class EventBlock : MonoBehaviour
{
    public enum Items
    {
        Big,
        Coin
    }

    public GameObject bigPrefab;
    public GameObject coinPrefab;
    public Transform createItemPos;
    public Sprite stopBlock;
    public Items item;
    int coinStack = 0;
    bool canSpawn = true;

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if(collision.gameObject.tag == "Player" && 
            PlayerMove.head_ray.collider != null && 
            canSpawn == true)
        {
            if(item == Items.Big)
            {
                Instantiate(bigPrefab, createItemPos, bigPrefab);
                canSpawn = false;
                SpriteRenderer block = gameObject.GetComponent<SpriteRenderer>();
                block.sprite = stopBlock;
            }
            else if(item == Items.Coin)
            {
                if(coinStack <= 5)
                {
                    GameUIManager.coin++;
                    StartCoroutine("SeeCoin");
                    coinStack++;
                }
            }
        }
    }

    IEnumerator SeeCoin()
    {
        GameObject coin = Instantiate(coinPrefab, createItemPos, coinPrefab);
        yield return new WaitForSeconds(0.3f);
        Destroy(coin);
    }
}
