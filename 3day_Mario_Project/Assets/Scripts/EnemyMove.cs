using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.Processors;

public class EnemyMove : MonoBehaviour
{
    public float moveSpeed = 5.0f;
    Vector3 target;
    int randX;
    int randRange;
    public static bool dead = false;
    bool canMove = true;
    public float killJump = 5.0f;

    void Start()
    {
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
        Dead();
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
        if (DropCheck.canDrop)
        {
            if (gameObject.transform.localScale.x == -1)
            {
                randRange = Random.Range(2, 4);
                target = new Vector3(transform.position.x - randRange, transform.position.y);
                gameObject.transform.localScale = new Vector3(1, transform.localScale.y);
                DropCheck.canDrop = false;
            }
            else if (gameObject.transform.localScale.x == 1)
            {
                randRange = Random.Range(2, 4);
                target = new Vector3(transform.position.x + randRange, transform.position.y);
                gameObject.transform.localScale = new Vector3(-1, transform.localScale.y);
                DropCheck.canDrop = false;
            }
        }
    }

    void Dead()
    {
        if (dead)
        {
            canMove = false;

            Collider2D dieCollide = gameObject.GetComponent<Collider2D>(); // Á×¾î¼­ Ãæµ¹ Á¦°Å
            dieCollide.enabled = false;

            SpriteRenderer dieRanderer = gameObject.GetComponent<SpriteRenderer>(); // Á×°í ¹ÝÅõ¸í »¡°£»ö
            Color dieColor = new Color(1f, 0f, 0f, 0.4f);
            dieRanderer.color = dieColor;

            StartCoroutine("ActiveFalse");
        }
    }

    IEnumerator ActiveFalse()
    {
        yield return new WaitForSeconds(2);
        gameObject.SetActive(false);
    }
}
