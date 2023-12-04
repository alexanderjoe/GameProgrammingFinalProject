using UnityEngine;

public class TestPlayerScript : MonoBehaviour
{
    [SerializeField] float moveSpeed = 5.0f;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        var xDirection = Input.GetAxis("Horizontal");
        var yDirection = Input.GetAxis("Vertical");

        rb.velocity = new Vector2(xDirection * moveSpeed, yDirection * moveSpeed);
    }
}