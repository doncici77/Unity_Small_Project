using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    public float moveSpeed = 5.0f;
    Vector3 target;
    int randX;
    int randRange;

    void Start()
    {
        MoveSetting();
    }

    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, target, moveSpeed * Time.deltaTime);
        Debug.Log(target);
        if(Mathf.Abs(target.x - transform.position.x) < 0.2)
        {
            MoveSetting();
        }

        if(DropCheck.canDrop)
        {
            MoveSetting();
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
}
