using System;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    Rigidbody2D rb;
    public float jumpForce = 5.0f;
    public float moveSpeed = 5.0f;
    Vector3 dir;

    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Move();
        Jump();
    }

    private void Jump()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("มกวม");
            rb.AddForce(Vector3.up * jumpForce, ForceMode2D.Impulse);
        }
    }

    private void Move()
    {
        float x = Input.GetAxis("Horizontal");
        dir = new Vector2(x, 0);
        transform.position += dir * moveSpeed * Time.deltaTime;
    }
}
