using UnityEngine;

public class NormalBlock : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player" && 
            PlayerMove.bigState == true && 
            PlayerMove.head_ray.collider != null)
        {
            Destroy(gameObject);
        }
    }
}
