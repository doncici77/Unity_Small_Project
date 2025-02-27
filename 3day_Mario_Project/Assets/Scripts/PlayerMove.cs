using System;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    Rigidbody2D rb;
    public float jumpForce = 5.0f; // ���� ����
    public float moveSpeed = 5.0f; // ������ �ӵ�
    public float killJump = 5.0f; // ������ Ƣ������� ����
    bool isJump = false; // ���� ���� ����
    bool canMove = true; // ������ ���� ����
    Vector3 dir;
    Animator animator;

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
        }
    }

    private void Jump()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            if(isJump)
            {
                Debug.Log("����");
                rb.AddForce(Vector3.up * jumpForce, ForceMode2D.Impulse);
                isJump = false;
                animator.SetBool("isUp", true);
            }
        }

        // ���ϸ��̼�
        if(rb.linearVelocityY < 0)
        {
            animator.SetBool("isDown", true);
            animator.SetBool("isUp", false);
        }
        else
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

        Debug.Log(rb.linearVelocityX);
        // �ִϸ��̼�
        animator.SetBool("isMove", x != 0f);
    }

    void Attack()
    {
        // ���� ĳ��Ʈ ���� ��ũ��Ʈ(y�ӵ��� ���߳�����)
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
            Dead();
        }
    }

    void Dead()
    {
        canMove = false;

        rb.AddForce(Vector2.up * killJump, ForceMode2D.Impulse); // �׾����� ���� Ʀ

        Collider2D dieCollide = gameObject.GetComponent<Collider2D>(); // �׾ �浹 ����
        dieCollide.enabled = false;

        SpriteRenderer dieRanderer = gameObject.GetComponent<SpriteRenderer>(); // �װ� ������ ������
        Color dieColor = new Color(1f, 0f, 0f, 0.4f);
        dieRanderer.color = dieColor;
    }
}
