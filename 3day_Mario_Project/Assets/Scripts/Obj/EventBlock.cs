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

    public AudioClip bigSound;
    public AudioClip coinSound;
    AudioSource blockSound;

    private void Start()
    {
        blockSound = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if(collision.gameObject.tag == "Player" && 
            PlayerMove.head_ray.collider != null && 
            canSpawn == true)
        {
            if(item == Items.Big)
            {
                blockSound.clip = bigSound;
                Instantiate(bigPrefab, createItemPos.position, Quaternion.identity);
                canSpawn = false;
                SpriteRenderer block = gameObject.GetComponent<SpriteRenderer>();
                block.sprite = stopBlock;
                blockSound.Play();
            }
            else if(item == Items.Coin)
            {
                blockSound.clip = coinSound;
                if (coinStack < 4)
                {
                    GameUIManager.coin++;
                    StartCoroutine("SeeCoin");
                    coinStack++;
                    blockSound.Play();
                }
                else if(coinStack == 4)
                {
                    GameUIManager.coin++;
                    StartCoroutine("SeeCoin");
                    coinStack++;
                    blockSound.Play();
                    SpriteRenderer block = gameObject.GetComponent<SpriteRenderer>();
                    block.sprite = stopBlock;
                }
            }
        }
    }

    IEnumerator SeeCoin()
    {
        GameObject coin = Instantiate(coinPrefab, createItemPos.position, Quaternion.identity);
        yield return new WaitForSeconds(0.3f);
        Destroy(coin);
    }
}
