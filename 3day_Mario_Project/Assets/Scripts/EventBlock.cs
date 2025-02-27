using System;
using UnityEngine;

public class EventBlock : MonoBehaviour
{
    public GameObject bigPrefab;
    public Transform createItemPos;
    public Sprite stopBlock;
    bool canSpawn = true;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("조우건1");
        if(collision.gameObject.tag == "Player" && 
            PlayerMove.head_ray.collider != null && 
            canSpawn == true)
        {
            Debug.Log("조우건2");
            Instantiate(bigPrefab, createItemPos, bigPrefab);
            canSpawn = false;
            SpriteRenderer block = gameObject.GetComponent<SpriteRenderer>();
            block.sprite = stopBlock;
        }
    }
}
