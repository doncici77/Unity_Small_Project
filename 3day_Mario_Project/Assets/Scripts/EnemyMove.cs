using UnityEngine;
using UnityEngine.InputSystem.Processors;

public class EnemyMove : MonoBehaviour
{
    public float moveSpeed = 5.0f;
    Vector3 target;
    int randX;
    int randRange;
    public static bool dead = false;

    void Start()
    {
        MoveSetting();
    }

    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, target, moveSpeed * Time.deltaTime);

        MoveSetting();
        Check();
        Dead();

    }

    void MoveSetting()
    {
        if (Mathf.Abs(target.x - transform.position.x) < 0.2)
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
        if(dead)
        {
            gameObject.SetActive(false);
        }
    }
}
