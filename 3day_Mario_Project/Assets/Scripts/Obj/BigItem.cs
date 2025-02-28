using UnityEngine;

public class BigItem : MonoBehaviour
{
    public float moveSpeed = 1.0f;
    Vector3 dir;

    private void Start()
    {
        dir = Vector2.left;
    }

    void Update()
    {
        transform.position += dir * moveSpeed * Time.deltaTime;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer == 7)
        {
            if(dir == Vector3.left)
            {
                dir = Vector2.right;
            }
            else
            {
                dir = Vector2.left;
            }
        }
        else if(collision.gameObject.tag == "DeadZone")
        {
            Destroy(gameObject);
        }
    }
}
