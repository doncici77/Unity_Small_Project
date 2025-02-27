using System;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    Rigidbody2D rb;
    public float jumpForce = 5.0f; // 점프 높이
    public float moveSpeed = 5.0f; // 움직임 속도
    public float killJump = 5.0f; // 죽을때 튀어오르는 높이
    bool isJump = false; // 점프 가능 여부
    bool canMove = true; // 움직임 가능 여부
    Vector3 dir;
    Animator animator;

    public static bool bigState = false;
    public static RaycastHit2D head_ray;

    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        animator = gameObject.GetComponent<Animator>();
    }

    void Update()
    {
        if (canMove)
        {
            Move();
            Jump();
            Attack();
            JumpAttack();
        }

        if(bigState)
        {
            gameObject.transform.localScale = new Vector3(transform.localScale.x, 1);
        }
    }

    private void JumpAttack()
    {
        if(!isJump)
        {
            head_ray = Physics2D.Raycast(rb.position, Vector2.up, 1.5f, LayerMask.GetMask("NormalBlock"));
            // 디버그용 Ray 그리기 (씬 뷰에서 확인 가능)
            Debug.DrawRay(rb.position, Vector2.up * 1.3f, Color.red, 0.1f);
        }
    }

    private void Jump()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            if(isJump)
            {
                rb.AddForce(Vector3.up * jumpForce, ForceMode2D.Impulse);
                isJump = false;
                Debug.Log("점프");
                animator.SetBool("isUp", true);
            }
        }

        // 에니메이션
        if(rb.linearVelocityY < 0)
        {
            animator.SetBool("isDown", true);
            animator.SetBool("isUp", false);
        }
        else if (rb.linearVelocityY == 0)
        {
            animator.SetBool("isDown", false);
            animator.SetBool("isUp", false);
        }
    }

    private void Move()
    {
        float x = Input.GetAxis("Horizontal");
        dir = new Vector2(x, 0);
        if(x > 0)
        {
            gameObject.transform.localScale = new Vector3(1, transform.localScale.y);
        }
        if (x < 0)
        {
            gameObject.transform.localScale = new Vector3(-1, transform.localScale.y);
        }
        transform.position += dir * moveSpeed * Time.deltaTime;

        // 애니메이션
        animator.SetBool("isMove", x != 0f);
    }

    void Attack()
    {
        // 레이 캐스트 공격 스크립트(y속도가 들쭉날쭉함)
        if (rb.linearVelocityY < 0)
        {
            RaycastHit2D attck_ray = Physics2D.Raycast(rb.position, Vector2.down, 0.2f, LayerMask.GetMask("Enemy"));

            if (attck_ray.collider != null)
            {
                EnemyMove.dead = true;
                rb.AddForce(Vector2.up * killJump, ForceMode2D.Impulse);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer == 6)
        {
            isJump = true;
        }

        if (collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "DeadZone")
        {
            if(bigState)
            {

            }
            else
            {
                Dead();
            }
        }

        if(collision.gameObject.tag == "ItemBig")
        {
            bigState = true;
            Destroy(collision.gameObject);
        }
    }

    void Dead()
    {
        canMove = false;

        rb.AddForce(Vector2.up * killJump, ForceMode2D.Impulse); // 죽었을때 위로 튐

        Collider2D dieCollide = gameObject.GetComponent<Collider2D>(); // 죽어서 충돌 제거
        dieCollide.enabled = false;

        SpriteRenderer dieRanderer = gameObject.GetComponent<SpriteRenderer>(); // 죽고 반투명 빨간색
        Color dieColor = new Color(1f, 0f, 0f, 0.4f);
        dieRanderer.color = dieColor;
    }
}
