using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementScript : MonoBehaviour
{
    float moveSpeed = 5.0f;
    Rigidbody2D rb;
    float xDirection = 0.0f;
    float yDirection = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        yDirection = Input.GetAxisRaw("Vertical");
        xDirection = Input.GetAxisRaw("Horizontal");
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(xDirection * moveSpeed, yDirection * moveSpeed);


    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        
    }
}
