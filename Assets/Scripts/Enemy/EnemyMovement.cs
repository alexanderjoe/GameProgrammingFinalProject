using UnityEngine;

public class SkeletonMovement : MonoBehaviour
{
    public GameObject player; 
    public float moveSpeed = 5f;
    public int chaseDistance = 10;
    public bool isChasing = false;

    void Update()
    {

        if (player != null)
        {
            float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);

            if (distanceToPlayer <= chaseDistance)
            {
                isChasing = true;
            }
            else
            {
                isChasing = false;
            }

            if (isChasing)
            {
                Vector3 direction = (player.transform.position - transform.position).normalized;

                transform.Translate(direction * moveSpeed * Time.deltaTime);
            }
        }
    }
}