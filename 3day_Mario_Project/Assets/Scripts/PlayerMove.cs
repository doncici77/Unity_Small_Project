using System;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    Rigidbody2D rb;
    public float jumpForce = 5.0f;
    public float moveSpeed = 5.0f;
    public float killJump = 5.0f;
    bool isJump = false;
    bool canMove = true;
    Vector3 dir;

    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
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
            }
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
    }

    void Attack()
    {
        // ���� ĳ��Ʈ ���� ��ũ��Ʈ(y�ӵ��� ���߳�����)
        if (rb.linearVelocityY < 0)
        {
            RaycastHit2D attck_ray = Physics2D.Raycast(rb.position, Vector2.down, 0.2f, LayerMask.GetMask("Enemy"));
            Debug.Log(attck_ray);
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

        if (collision.gameObject.tag == "Enemy")
        {
            Dead();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "DeadZone")
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
