using UnityEngine;

public class EventBlock : MonoBehaviour
{
    public GameObject bigPrefab;
    public Transform createItemPos;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player" && PlayerMove.head_ray.collider != null)
        {
            Instantiate(bigPrefab, createItemPos, bigPrefab);
        }
    }
}
