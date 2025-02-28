using UnityEngine;

public class NormalBlock : MonoBehaviour
{
    AudioSource blockSound;
    SpriteRenderer spriteRenderer;
    BoxCollider2D boxCollider;

    private void Start()
    {
        blockSound = GetComponent<AudioSource>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player" && 
            PlayerMove.bigState == true && 
            PlayerMove.head_ray.collider != null)
        {
            blockSound.Play();
            spriteRenderer.enabled = false;
            boxCollider.enabled = false;
        }
    }
}
