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
        if (collision.gameObject.tag == "Player" && // 출동 된게 플레이어고 
            (collision.gameObject.GetComponent<Transform>().position.y < transform.position.y) && // 플레이어가 아래에 있고
            PlayerMove.bigState == true && // 빅 모드이고
            PlayerMove.head_ray.collider != null) // 출동 레이의 영역안에 있으면
        {
            blockSound.Play();
            spriteRenderer.enabled = false;
            boxCollider.enabled = false;
        }
    }
}
