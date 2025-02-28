using System;
using System.Collections;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    Rigidbody2D rb;
    public float jumpForce = 5.0f; // ���� ����
    public float moveSpeed = 5.0f; // ������ �ӵ�
    public float killJump = 5.0f; // ������ Ƣ������� ����
    public float nukbackForce = 5.0f; // ������ Ƣ������� ����
    bool isJump = false; // ���� ���� ����
    bool canMove = true; // ������ ���� ����
    bool goGoal = false;
    Vector3 goalPos;
    Vector3 dir;
    Animator animator;

    Vector3 respawnPos;
    SpriteRenderer spriteRenderer;
    Collider2D dieCollide;
    Color respawnColor;

    public static bool bigState = false; // �� ���
    public static RaycastHit2D head_ray; // �Ӹ� ���� �浹 üũ
    public static RaycastHit2D attck_ray;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        respawnColor = spriteRenderer.color;
        respawnPos = transform.position;

        rb = gameObject.GetComponent<Rigidbody2D>();
        animator = gameObject.GetComponent<Animator>();

        GameObject goal = GameObject.FindGameObjectWithTag("Goal");
        goalPos = goal.transform.position;
        Debug.Log(goalPos);
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

        if(goGoal && isJump) 
        {
            transform.position = Vector3.MoveTowards(transform.position, goalPos, moveSpeed * Time.deltaTime);

            if(MathF.Abs(transform.position.x - goalPos.x) < 0.2)
            {
                gameObject.SetActive(false);
                GameUIManager.goalState = true;
            }
        }
    }

    private void JumpAttack()
    {
        if(!isJump)
        {
            Vector2 rayStartPos = rb.position + Vector2.up * 1.0f;
            head_ray = Physics2D.Raycast(rayStartPos, Vector2.up, 0.3f, LayerMask.GetMask("Block"));

            // ����׿� Ray �׸��� (�� �信�� Ȯ�� ����)
            Debug.DrawRay(rayStartPos, Vector2.up * 0.3f, Color.red, 0.1f);
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
                animator.SetBool("isUp", true);
            }
        }

        // ���ϸ��̼�
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

        // �ִϸ��̼�
        animator.SetBool("isMove", x != 0f);
    }

    void Attack()
    {
        // ���� ĳ��Ʈ ���� ��ũ��Ʈ(y�ӵ��� ���߳�����)
        if (rb.linearVelocityY < 0)
        {
            attck_ray = Physics2D.Raycast(rb.position, Vector2.down, 0.2f, LayerMask.GetMask("Enemy"));
            Debug.DrawRay(transform.position, Vector2.down * 0.2f, Color.yellow, 0.1f);

            if (attck_ray.collider != null)
            {
                rb.AddForce(Vector2.up * killJump, ForceMode2D.Impulse);

                EnemyMove enemy = attck_ray.collider.gameObject.GetComponent<EnemyMove>();
                enemy.Dead();
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer == 6 || collision.gameObject.layer == 9 
            || collision.gameObject.layer == 7)
        {
            isJump = true;
        }

        if (collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "DeadZone")
        {
            if(bigState)
            {
                gameObject.transform.localScale = new Vector3(transform.localScale.x, 0.5f);
                bigState = false;

                Vector2 nukback = (transform.position - collision.transform.position);
                rb.AddForce(nukback * nukbackForce, ForceMode2D.Impulse);
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Coin")
        {
            Destroy(collision.gameObject);
            GameUIManager.coin++;
        }

        if (collision.gameObject.tag == "Finish")
        {
            canMove = false;
            goGoal = true;
        }
    }

    void Dead()
    {
        canMove = false;

        rb.AddForce(Vector2.up * killJump, ForceMode2D.Impulse); // �׾����� ���� Ʀ

        dieCollide = gameObject.GetComponent<Collider2D>(); // �׾ �浹 ����
        dieCollide.enabled = false;

        spriteRenderer = gameObject.GetComponent<SpriteRenderer>(); // �װ� ������ ������
        Color dieColor = new Color(1f, 0f, 0f, 0.4f);
        spriteRenderer.color = dieColor;

        StartCoroutine("ReSpawn");
    }

    IEnumerator ReSpawn()
    {
        yield return new WaitForSeconds(2);
        spriteRenderer.color = respawnColor;
        transform.position = respawnPos;
        dieCollide.enabled = true;
        canMove = true;
    }
}
