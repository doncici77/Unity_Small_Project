using System;
using System.Collections;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    Rigidbody2D rb;
    public float jumpForce = 5.0f; // 점프 높이
    public float moveSpeed = 5.0f; // 움직임 속도
    public float killJump = 5.0f; // 죽을때 튀어오르는 높이
    public float nukbackForce = 5.0f; // 죽을때 튀어오르는 높이
    bool isJump = false; // 점프 가능 여부
    bool canMove = true; // 움직임 가능 여부
    public bool goGoal = false;
    bool isPowerUp = false;
    Vector3 goalPos;
    Vector3 dir;
    Animator animator;

    Vector3 respawnPos;
    SpriteRenderer spriteRenderer;
    Collider2D dieCollide;
    Color respawnColor;

    public static bool bigState = false; // 빅 모드
    public static RaycastHit2D head_ray; // 머리 위로 충돌 체크
    public static RaycastHit2D attck_ray;
    RaycastHit2D bottom_ray;
    RaycastHit2D wall_ray;
    RaycastHit2D block_ray;

    public AudioClip deadSound;
    public AudioClip coinSound;
    public AudioClip jumpSound;
    public AudioClip poweUpSound;
    AudioSource playerSound;

    public Sprite stage1B;
    SpriteRenderer backgroundSprite;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        respawnColor = spriteRenderer.color;
        respawnPos = transform.position;

        rb = gameObject.GetComponent<Rigidbody2D>();
        animator = gameObject.GetComponent<Animator>();

        GameObject goal = GameObject.FindGameObjectWithTag("Goal");
        goalPos = goal.transform.position;

        GameObject backG = GameObject.FindGameObjectWithTag("BackGround");
        backgroundSprite = backG.GetComponent<SpriteRenderer>();

        playerSound = gameObject.GetComponent<AudioSource>();
    }

    void Update()
    {
        if (canMove)
        {
            Move();
            Jump();
            Attack();
            DestoryBlock();
        }
        CheckJump();

        if (bigState)
        {
            if(isPowerUp)
            {
                gameObject.transform.localScale = new Vector3(transform.localScale.x, 1);
                playerSound.clip = poweUpSound;
                playerSound.Play();
                isPowerUp = false;
            }
        }

        if(goGoal && isJump) 
        {
            transform.position = Vector3.MoveTowards(transform.position, goalPos, moveSpeed * Time.deltaTime);
            animator.SetBool("isDown", false);
            animator.SetBool("isUp", false);
            animator.SetBool("isMove", true);

            if (MathF.Abs(transform.position.x - goalPos.x) < 0.2)
            {
                gameObject.SetActive(false);
                GameUIManager.goalState = true;
            }
        }
    }

    private void DestoryBlock()
    {
        if(!isJump)
        {
            Vector2 rayStartPos = rb.position + Vector2.up * 1.0f;
            head_ray = Physics2D.Raycast(rayStartPos, Vector2.up, 0.3f, LayerMask.GetMask("Block"));

            // 디버그용 Ray 그리기 (씬 뷰에서 확인 가능)
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
                playerSound.clip = jumpSound;
                playerSound.Play();
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

    void CheckJump()
    {
        bottom_ray = Physics2D.Raycast(rb.position, Vector2.down, 0.2f, LayerMask.GetMask("Bottom"));
        wall_ray = Physics2D.Raycast(rb.position, Vector2.down, 0.2f, LayerMask.GetMask("Wall"));
        block_ray = Physics2D.Raycast(rb.position, Vector2.down, 0.2f, LayerMask.GetMask("Block"));

        Debug.Log(isJump);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (bottom_ray.collider != null)
        {
            isJump = true;
        }
        else if (wall_ray.collider != null)
        {
            isJump = true;
        }
        else if (block_ray.collider != null)
        {
            isJump = true;
        }

        if (collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "DeadZone")
        {
            if(bigState && collision.gameObject.tag == "Enemy")
            {
                gameObject.transform.localScale = new Vector3(transform.localScale.x, 0.5f);
                bigState = false;

                Vector2 nukback = (transform.position - collision.transform.position);
                rb.AddForce(nukback * nukbackForce, ForceMode2D.Impulse);
            }
            if(bigState && collision.gameObject.tag == "DeadZone")
            {
                gameObject.transform.localScale = new Vector3(transform.localScale.x, 0.5f);
                bigState = false;

                rb.AddForce(Vector2.up * nukbackForce * 2, ForceMode2D.Impulse);
            }
            else
            {
                Dead();
            }
        }

        if(collision.gameObject.tag == "ItemBig")
        {
            bigState = true;
            isPowerUp = true;
            Destroy(collision.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Coin")
        {
            Destroy(collision.gameObject);
            GameUIManager.coin++;
            playerSound.clip = coinSound;
            playerSound.Play();
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

        playerSound.clip = deadSound;
        playerSound.Play();

        rb.AddForce(Vector2.up * killJump, ForceMode2D.Impulse); // 죽었을때 위로 튐

        dieCollide = gameObject.GetComponent<Collider2D>(); // 죽어서 충돌 제거
        dieCollide.enabled = false;

        spriteRenderer = gameObject.GetComponent<SpriteRenderer>(); // 죽고 반투명 빨간색
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
        backgroundSprite.sprite = stage1B;
    }
}
