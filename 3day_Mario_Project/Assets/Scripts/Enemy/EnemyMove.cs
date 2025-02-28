using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.InputSystem.Processors;

public class EnemyMove : MonoBehaviour
{
    public float moveSpeed = 5.0f;
    Vector3 target;
    int randX;
    int randRange;
    bool canMove = true;
    public float killJump = 5.0f;
    public DropCheck dropCheck;
    AudioSource audioSource;

    void Start()
    {
        dropCheck = GetComponentInChildren<DropCheck>();
        audioSource = GetComponentInChildren<AudioSource>();
        MoveSetting();
    }

    void Update()
    {
        if (canMove)
        {
            transform.position = Vector3.MoveTowards(transform.position, target, moveSpeed * Time.deltaTime);
            if (Mathf.Abs(target.x - transform.position.x) < 0.2)
            {
                MoveSetting();
            }
            Check();
        }
    }

    void MoveSetting()
    {
        randX = Random.Range(0, 2);
        randRange = Random.Range(1, 4);
        if (randX == 0)
        {
            target = new Vector3(transform.position.x - randRange, transform.position.y);
            gameObject.transform.localScale = new Vector3(1, transform.localScale.y);
        }
        if (randX == 1)
        {
            target = new Vector3(transform.position.x + randRange, transform.position.y);
            gameObject.transform.localScale = new Vector3(-1, transform.localScale.y);
        }
    }

    void Check()
    {
        if (dropCheck.canDrop)
        {
            if (gameObject.transform.localScale.x == -1)
            {
                randRange = Random.Range(2, 4);
                target = new Vector3(transform.position.x - randRange, transform.position.y);
                gameObject.transform.localScale = new Vector3(1, transform.localScale.y);
                dropCheck.canDrop = false;
            }
            else if (gameObject.transform.localScale.x == 1)
            {
                randRange = Random.Range(2, 4);
                target = new Vector3(transform.position.x + randRange, transform.position.y);
                gameObject.transform.localScale = new Vector3(-1, transform.localScale.y);
                dropCheck.canDrop = false;
            }
        }
    }

    public void Dead()
    {
        canMove = false;

        Collider2D dieCollide = gameObject.GetComponent<Collider2D>(); // 죽어서 충돌 제거
        dieCollide.enabled = false;

        SpriteRenderer dieRanderer = gameObject.GetComponent<SpriteRenderer>(); // 죽고 반투명 빨간색
        Color dieColor = new Color(1f, 0f, 0f, 0.4f);
        dieRanderer.color = dieColor;

        audioSource.Play();

        StartCoroutine("ActiveFalse"); // 2초뒤에 비활성화
    }

    IEnumerator ActiveFalse()
    {
        yield return new WaitForSeconds(2);
        gameObject.SetActive(false);
    }
}
