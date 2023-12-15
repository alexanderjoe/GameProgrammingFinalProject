using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OingoAttackScript : MonoBehaviour
{
    private Rigidbody2D _rb;
    private GameLogicScript _gls;
    public GameObject oingoAttack;
    GameObject player;
    GameObject boss;
    float time = 0;
    float sampleTimer = 0.0f;
    float waitTime = 0.2f;
    float lastXSample = 0.0f;
    float lastYSample = 0.0f;


    float locX;
    float locY;

    public float xDirection = 0.0f;
    public float slidingFactor = 0.999f;
    public bool isGrounded = false;
    public bool isSliding = false;
    bool isMoving = false;

    const float MoveSpeed = 5.0f;

    // Start is called before the first frame update
    void Start()
    {
        _gls = GameObject.Find("GameLogic").GetComponent<GameLogicScript>();
        _rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");
        boss = GameObject.FindGameObjectWithTag("Boss");
        locX = Random.Range(-19f, 20f);
        locY = Random.Range(-6f, 7f);

        //inital launch towards player
        if (player.transform.position.x < boss.transform.position.x)
        {
            xDirection = -0.5f;
        }
        else
        {
            xDirection = 0.5f;
        }
    }

    // Update is called once per frame
    void Update()
    {

        if (xDirection > 0 && transform.localScale.x < 0)
        {
            FlipOingo();
        }
        else if (xDirection < 0 && transform.localScale.x > 0)
        {
            FlipOingo();
        }

        time += Time.deltaTime;
        sampleTimer += Time.deltaTime;

        if (sampleTimer > 0.1f)
        {
            isMoving = false;
            if (gameObject.transform.position.x != lastXSample || gameObject.transform.position.y != lastYSample)
            {
                isMoving = true;
            }

            //sample x coord
            lastXSample = gameObject.transform.position.x;
            lastYSample = gameObject.transform.position.y;
            sampleTimer = 0.0f;
        }


        // Subtracting two is more accurate over time than resetting to zero.
        if (time > waitTime)
        {
            // Remove the recorded 2 seconds.
            //time -= waitTime;
            if (!isMoving)
            {
                //Debug.Log(_rb.velocity);
                //Debug.Log("invert velocity");
                xDirection *= -1;
                isMoving = true;
                //Debug.Log(_rb.velocity);
            }
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
           //do nothing now
        }

    }


    void FlipOingo()
    {
        var tempTransform = transform;
        var gobScale = tempTransform.localScale;
        gobScale.x *= -1;
        tempTransform.localScale = gobScale;
    }
}
