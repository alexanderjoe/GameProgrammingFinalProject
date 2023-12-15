using UnityEngine;

public class SkeletonMovement : MonoBehaviour
{
    public Transform player; 
    public float moveSpeed = 5f; 

    void Update()
    {
        if (player != null)
        {
            Vector3 direction = player.position - transform.position;

            direction.Normalize();

            transform.Translate(direction * moveSpeed * Time.deltaTime);
        }
    }
}